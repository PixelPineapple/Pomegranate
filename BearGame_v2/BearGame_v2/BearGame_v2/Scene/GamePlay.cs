//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：ゲームシーン　-　ゲームプレイ
// 最後の更新 : 2017.11.08
//-------------------------------------------------------

using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using BearGame_v2.Actor;
using BearGame_v2.Utility;
using BearGame_v2.Utility.ParticleRelated;

namespace BearGame_v2.Scene
{
    class GamePlay : IScene
    {
        public GameManager gameManager { get; private set; }

        private Scenery scenery;    // 背景画像
        private bool endFlag;   // 終了フラグ
        private List<Body> _bodies = new List<Body>();  // ゲームオブジェクトのボヂィー
        private const float Scale = 100f; // FarseerPhysicsのユニットから、XNAのユニットに
        private ThePhysicsWorld _world; // FarseerPhysicsの物理の世界
        private Nico _nico; // 主人公
        private Cherry _cherry; // ゴール
        private MouseInput mouse; // マウス入力
        private int nowTurn; // 今のターンの数
        private Sound sound; // サウンド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice"></param>
        public GamePlay(GameDevice gameDevice)
        {
            endFlag = false;
            _world = gameDevice.GetPhysicsWorld();
            mouse = gameDevice.GetMouseInput();
            gameManager = new GameManager(_world, mouse); // ゲームマネジャー
            scenery = new Scenery();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            gameManager.Initialize();
            endFlag = false;
            // 主人公
            Body nico = PrefabBodyFactory.CreateBody(PrefabType.Nico, _world.TheWorld, new Vector2(1.3f, 3.88f));
            _nico = new Nico(nico);
            _nico.Initialize();
            // ゴール / 主人公が守らなければならないもの
            Body cherry = PrefabBodyFactory.CreateBody(PrefabType.Cherry, _world.TheWorld, new Vector2(2.5f, 3.88f));
            _cherry = new Cherry(cherry);
            _cherry.Initialize();

            // かめらのターゲット
            Camera.Current.CenterPointTarget = 1520f;

            // 最初のターン
            nowTurn = 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime)
        {
            // 主人公に果物の玉をあげる。
            if (gameManager.Turn % 2 != 0 && nowTurn != gameManager.Turn)
            {
                _bodies.Add(gameManager.FruitBullet.Body);
                nowTurn = gameManager.Turn;
            }

            // 主人公のモションを更新
            if(!gameManager.FruitBullet.Launched)
                _nico.UpdateMotion(Nico.NicoAnimation.Lifting);
            else
            {
                _nico.UpdateMotion(Nico.NicoAnimation.Idle);
            }

            gameManager.Update(gameTime);
            Camera.Current.Update(gameTime); // カメラの更新
            if(nowTurn % 2 != 0)
            {
                // プレイヤーのターンにカメラを動かせる
                Camera.Current.cameraCondition = CameraCondition.Moveable;
            }

            // プレイヤーを更新
            _nico.Update(gameTime);
            _cherry.Update(gameTime);

            // プレイヤーが勝負
            if(gameManager.isPlayerWin != GameState.TBD)
            {
                // 全部の不滅のキャラクターを削除する
                _nico.Body.Dispose();
                _cherry.Body.Dispose();
                // 不滅のキャラクター以外も削除します
                foreach(Body x in _bodies)
                {
                    x.Dispose();
                }
                _bodies.Clear();
                endFlag = true;
            }
            // 削除するべき物は全部削除します
            _bodies.RemoveAll(x => ((PrefabUserData)x.UserData).Status == BodyStatus.ToBeDestroyed);
        }

        /// <summary>
        /// シーンが終わったか？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return endFlag;
        }

        /// <summary>
        /// 次のシーン
        /// </summary>
        /// <returns>エンディングシーン</returns>
        public SceneType Next()
        {
            return SceneType.Ending;
        }

        public void Shutdown()
        {

        }
        
        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="gameTime">ゲーム時間</param>
        public void Draw(Renderer renderer, GameTime gameTime)
        {
            renderer.CameraBegin();

            scenery.Draw(renderer);

            renderer.DrawTexture("floor_long", Constants.FloorPosition);

            foreach (var body in _bodies)
            {
                var prefabUserData = (PrefabUserData)body.UserData;
                if (gameManager.particleManager.IsActive)
                {
                    gameManager.particleManager.Draw(renderer);
                }
                switch (prefabUserData.Status)
                {
                    case BodyStatus.Active:
                        renderer.DrawTexture(
                            prefabUserData.SpriteName,
                            body.Position,
                            Constants.Scale,
                            body.Rotation,
                            null,
                            Color.White,
                            (prefabUserData.Origin * Constants.Scale));
                        break;
                    case BodyStatus.Destroyed:
                        prefabUserData.ExplosionAnimation.Draw(renderer, gameTime);
                        break;
                }
            }

            _nico.Draw(renderer);

            _cherry.Draw(renderer);

            gameManager.Draw(renderer);

            renderer.DrawTexture("Foreground", Vector2.Zero, Color.White);
            renderer.End();
        }
    }
}
