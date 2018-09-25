//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.16
// 内容　：ゲーム状態を管理するクラス
// 最後の更新 : 2017.11.13
//-------------------------------------------------------

using BearGame_v2.Actor;
using BearGame_v2.Scene;
using BearGame_v2.Utility;
using BearGame_v2.Utility.ParticleRelated;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    /// <summary>
    /// ゲームステータス
    /// </summary>
    public enum GameState {
        WIN,    //  勝勝つ
        LOSE,   //  負ける
        TBD     //  まだ決まっていません
    }

    //　風の強さを担当
    //　プレイヤーに果物をあげる
    //　ターンを担当する
    //　クマの動き
    //　勝つ負けるを管理
    class GameManager
    {
        public int Turn { get; private set; }   // 今のターン
        public List<PrefabType> Fruits { get; private set; }    // 果物の玉のリスト
        public FruitBullet FruitBullet { get; private set; }    // 果物の玉
        private Random rand;    // ランダム
        private MouseInput mouse;   // マウスインプット
        private ThePhysicsWorld world;  // 物理のワールド
        private Kuma _kuma; // 敵のクマ
        public Timer PlayerTimer
        {
            get; private set;
        }   // 果物を投げるの制限時間
        public Timer KumaTimer
        {
            get; private set;
        }   // クマの制限時間
        public ParticleManager particleManager { get; private set; }    // パーティクルマネージャー

        public GameState isPlayerWin{ get; private set; }   // 主人公の勝つか？
        private TimerUI timerUI;    // タイマー

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="_world">物理ワールド</param>
        /// <param name="mouse">マウスインプット</param>
        public GameManager(ThePhysicsWorld _world, MouseInput mouse)
        {
            Fruits = new List<PrefabType>()
            {
                // Small Fruits 小さい果物
                PrefabType.Momo,                    //PrefabBodyFactory.CreateBody(PrefabType.Momo, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.Orange,                  //PrefabBodyFactory.CreateBody(PrefabType.Orange, _world.TheWorld, new Vector2(1.3f, 3.0f)), 
                PrefabType.Strawberry,             //PrefabBodyFactory.CreateBody(PrefabType.Strawberry, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.Lime,                      //PrefabBodyFactory.CreateBody(PrefabType.Lime, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.CherryFruit,             //PrefabBodyFactory.CreateBody(PrefabType.CherryFruit, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.Apple,                     //PrefabBodyFactory.CreateBody(PrefabType.Apple, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                // Big Fruits 大きい果物
                PrefabType.Durian,                    //PrefabBodyFactory.CreateBody(PrefabType.Durian, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.Melon,                     //PrefabBodyFactory.CreateBody(PrefabType.Melon, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.Pineapple,                //PrefabBodyFactory.CreateBody(PrefabType.Pineapple, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                PrefabType.DragonFruit,             //PrefabBodyFactory.CreateBody( PrefabType.DragonFruit, _world.TheWorld, new Vector2(1.3f, 3.0f)),
                // Special Fruits 特別な果物
                PrefabType.Pomegranate            //PrefabBodyFactory.CreateBody(PrefabType.Pomegranate, _world.TheWorld, new Vector2(1.3f, 3.0f))
            };

            rand = new Random();    // ランダム

            this.mouse = mouse; // マウス

            world = _world; // 物理のワールド

            isPlayerWin = GameState.TBD;    // ゲームステート
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            isPlayerWin = GameState.TBD;　// ゲームステート
            Body kuma = PrefabBodyFactory.CreateBody(PrefabType.Kuma, world.TheWorld, new Vector2(15.3f, 3.88f));   // クマの物理ボディー
            _kuma = new Kuma(kuma);
            _kuma.Initialize();

            Turn = 0;　// ターンを零に設定
            Reload(GetSmallFruits());   // プレイヤーを果物の玉に上げる。
            PlayerTimer = new Timer(20);    // プレイヤーの制限時間
            timerUI = new TimerUI(PlayerTimer); // タイマーのUI

            KumaTimer = new Timer(3);   // クマの動きの制限時間
            ChangeTurn();   // ターンをチェンジ
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime)
        {
            // プレイやーはまだ‘勝負しない
            if (isPlayerWin == GameState.TBD)
            {
                // プレイやーが勝つ
                if (((PrefabUserData)_kuma.Body.UserData).Status == BodyStatus.ToBeDestroyed)
                {
                    isPlayerWin = GameState.WIN;
                }
                // プレイやーが負ける
                if (_kuma.Position.X <= 3.3f)
                {
                    isPlayerWin = GameState.LOSE;
                }
                if (Turn % 2 != 0) // プレイヤーのターン
                {
                    // カメラのトラキングがプレイやーに
                    if (Camera.Current.GetTrackingBody() == null)
                    {
                        Camera.Current.StartTracking(FruitBullet.Body);
                        Camera.Current.cameraCondition = CameraCondition.PlayerFound;
                    }
                    // プレイヤーの制限時間が無くなった。
                    if (PlayerTimer.IsTime())
                    {
                        ((PrefabUserData)FruitBullet.Body.UserData).Destroy(FruitBullet.Body);
                        FruitBullet.Body.Dispose();
                        ChangeTurn();
                    }
                    // 果物の玉が投げられたか？
                    if (!FruitBullet.Launched) 
                    {
                        PlayerTimer.Update(); // プレイヤーのタイマーを更新
                        FruitBullet.HandleInput(gameTime, mouse.GetMouseCondition());
                    }
                    else // 果物の玉を殴られたら、
                    {
                        Camera.Current.cameraCondition = CameraCondition.Stop;
                        // パーティクルを管理
                        if (!particleManager.IsActive) { particleManager.SwitchOn(); }
                        else
                        {
                            var random = new Random();
                            var velocity = new Vector2(((float)random.Next(-1, 2)), ((float)random.Next(-5, -1)));
                            particleManager.Add(new Particle("Particle", FruitBullet.Body.Position, velocity, ((PrefabUserData)FruitBullet.Body.UserData).ParticleColor));
                            particleManager.Update();
                        }
                        // 果物の玉を消す
                        if (FruitBullet.Body.LinearVelocity == Vector2.Zero || FruitBullet.Position.X >= 20.0f || FruitBullet.Position.X <= -5.0f)
                        {
                            particleManager.SwitchOff();
                            ((PrefabUserData)FruitBullet.Body.UserData).Status = BodyStatus.ToBeDestroyed;
                            ((PrefabUserData)FruitBullet.Body.UserData).Destroy(FruitBullet.Body);
                            FruitBullet.Body.Dispose();
                            ChangeTurn();
                            Console.WriteLine(((PrefabUserData)_kuma.Body.UserData).HitPoints);
                        }
                    }
                }
                else // クマのターン
                {
                    // カメラのトラキングがクマに
                    if (Camera.Current.GetTrackingBody() == null)
                    {
                        Camera.Current.StartTracking(_kuma.Body);
                        Camera.Current.cameraCondition = CameraCondition.PlayerFound;
                    }
                    if (((PrefabUserData)_kuma.Body.UserData).HitPoints == 0)
                    {
                        isPlayerWin = GameState.WIN;
                    }
                    KumaTimer.Update();
                    if (!KumaTimer.IsTime())
                    {
                        // クマを動かせる。
                        _kuma.UpdateMotion(Kuma.KumaAnimation.Walk);
                        _kuma.MoveKuma();
                    }
                    else
                    {
                        // クマのターンを終わらせる、プレイヤーを新しい果物の玉をあげる
                        _kuma.UpdateMotion(Kuma.KumaAnimation.Idle);
                        Reload(GetSmallFruits());
                        ChangeTurn();
                    }
                }
                _kuma.Update(gameTime);
            }
        }

        /// <summary>
        /// ターンをチェンジ「クマとプレイヤー」
        /// </summary>
        public void ChangeTurn()
        {
            Camera.Current.StopTracking();
            Turn++;
            if(Turn % 2 != 0)
            {
                PlayerTimer.Initialize();
            }
            else
            {
                KumaTimer.Initialize();
            }
        }

        /// <summary>
        /// 小さい果物をあげる
        /// </summary>
        /// <returns>小さい果物</returns>
        public Body GetSmallFruits()
        {
            Body fruit = PrefabBodyFactory.CreateBody(Fruits[rand.Next(0, 10)], world.TheWorld, new Vector2(1.3f, 3.0f));
            return fruit;
        }

        /// <summary>
        /// 大きい果物をあげる
        /// </summary>
        /// <returns>大きい果物</returns>
        public Body GetBigFruits()
        {
            Body fruit = PrefabBodyFactory.CreateBody(Fruits[rand.Next(6, 10)], world.TheWorld, new Vector2(1.3f, 3.0f));
            return fruit;
        }

        /// <summary>
        /// 特別な果物をあげる
        /// </summary>
        /// <returns>特別な果物</returns>
        public Body GetSpecialFruits()
        {
            Body fruit = PrefabBodyFactory.CreateBody(Fruits[10], world.TheWorld, new Vector2(1.3f, 3.0f));
            return fruit;
        }

        /// <summary>
        /// 果物の玉をあげる
        /// </summary>
        /// <param name="bodies"></param>
        /// <returns></returns>
        public FruitBullet Reload(Body bodies)
        {
            FruitBullet = new FruitBullet(bodies, mouse);
            particleManager = new ParticleManager();
            return FruitBullet;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw (Renderer renderer)
        {
            _kuma.Draw(renderer);
            if (FruitBullet.IsBeingDragged)
            {
                PrefabUserData userData = ((PrefabUserData)FruitBullet.Body.UserData);
                renderer.DrawTexture(
                    userData.SpriteName,
                    FruitBullet.DragPosition,
                    Constants.Scale,
                    0,
                    null,
                    Constants.HalfTransparent,
                    (userData.Origin * Constants.Scale));
            }
            timerUI.Draw(renderer);
        }
    }
}
