//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：キャラクター[Cherry]のクラス
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using BearGame_v2.Utility;

namespace BearGame_v2.Actor
{
    class Cherry : Character
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="body">Farseerの物理的なボディー</param>
        public Cherry(Body body) : base (body)
        {
            ((PrefabUserData)body.UserData).IsImmortal = true;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public override void Initialize()
        {
            motion = new Motion();
            int counter = 0;
            for (int y = 0; y < 1; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    motion.Add(counter, new Rectangle(48 * x, 58 * y, 48, 58));
                    counter++;
                }
            }
            motion.Initialize(new Range(0, 9), new Timer(0.1f));
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
