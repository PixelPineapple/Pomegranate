//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.18
// 内容　：キャラクター[Nico]のクラス
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using BearGame_v2.Device;
using BearGame_v2.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Actor
{
    class Nico : Character
    {
        // アニメーションの状態
        public enum NicoAnimation
        {
            EarlyState,
            Idle,
            Lifting,
            Throwing
        }

        // 現在のアニメーション
        private NicoAnimation currentAnimation = NicoAnimation.EarlyState;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="body">Farseerの物理的なボディー</param>
        public Nico(Body body) : base (body)
        {
            // 不滅のキャラクター
            ((PrefabUserData)body.UserData).IsImmortal = true;
        }

        /// <summary>
        /// 初期化する
        /// </summary>
        public override void Initialize()
        {
            // モションクラスを呼び出す
            motion = new Motion();
            // モションカウンター
            int counter = 0;
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    motion.Add(counter, new Rectangle(64 * x, 64 * y, 64, 64));
                    counter++;
                }
            }
            // モションを変更する
            UpdateMotion(NicoAnimation.Lifting);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public override void Update(GameTime gameTime)
        {
            // モションを更新
            motion.Update(gameTime);
        }

        /// <summary>
        /// モション変化を担当
        /// </summary>
        /// <param name="changeTo">次の</param>
        public void UpdateMotion(NicoAnimation changeTo)
        {
            // アイドルアニメーション
            if(changeTo == NicoAnimation.Idle && currentAnimation != changeTo)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(0, 9), new Timer(0.1f));
            }
            // 持ち上げるアニメーション
            if(changeTo == NicoAnimation.Lifting && currentAnimation != changeTo)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(10, 19), new Timer(0.1f));
            }
            // 投げるアニメーション
            if(changeTo == NicoAnimation.Throwing && currentAnimation != changeTo)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(20, 29), new Timer(0.05f));
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public override void Draw(Renderer renderer)
        {
            // ステータスをチェック「アクティブか?」
            var prefabUserData = (PrefabUserData)Body.UserData;
            switch (prefabUserData.Status)
            {
                // 描画する
                case BodyStatus.Active:
                    renderer.DrawTexture(
                        Name,
                        Position,
                        Constants.Scale,
                        Body.Rotation,
                        motion.DrawingRange(),
                        Color.White,
                        (Origin * Constants.Scale));
                    break;
            }
        }
    }
}