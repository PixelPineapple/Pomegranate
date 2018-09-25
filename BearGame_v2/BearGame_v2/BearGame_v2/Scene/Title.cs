//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：タイトルシーン
// 最後の更新 : 2017.10.19
//-------------------------------------------------------

using BearGame_v2.Device;
using BearGame_v2.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Scene
{
    class Title : IScene
    {
        private InputState input;
        private bool endFlag;
        private Timer stpTime;
        private float counter = 0;
        private bool upDown = true;
        private Sound sound;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice">ゲームデバイス</param>
        public Title(GameDevice gameDevice)
        {
            input = gameDevice.GetInputState();
            endFlag = false;
            stpTime = new Timer(2);
            stpTime.Initialize();
            sound = gameDevice.GetSound();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            endFlag = false;
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime)
        {
            stpTime.Update();
            if (stpTime.IsTime()) { stpTime.Initialize(); }
            if (input.GetKeyTrigger(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                sound.PlaySE("Archery arrow release");
                endFlag = true;
            }
        }

        /// <summary>
        /// シーンが終わりましたか？
        /// </summary>
        /// <returns></returns>
        public bool IsEnd()
        {
            return endFlag;
        }

        /// <summary>
        /// 次のシーンに切り替える
        /// </summary>
        /// <returns></returns>
        public SceneType Next()
        {
            return SceneType.GamePlay;
        }

        public void Shutdown()
        {

        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="gameTime">ゲームタイム</param>
        public void Draw(Renderer renderer, GameTime gameTime)
        {
            renderer.Begin();
            renderer.DrawTexture("Title_BG", Vector2.Zero);
            renderer.DrawTexture("Judul", new Vector2 (100, 50.0f));
            if (stpTime.Now() % 12 == 0)
            {
                if (upDown)
                {
                    counter += 0.1f;
                }
                else
                {
                    counter -= 0.1f;
                }
                if (counter >= 1)
                {
                    upDown = false;
                }
                else if (counter <= 0)
                {
                    upDown = true;
                }
            }
            renderer.DrawTexture("SpaceToPlay_TEXT", new Vector2(200.0f, 300.0f), counter);
            renderer.End();
        }
    }
}
