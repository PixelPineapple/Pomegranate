//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.18
// 内容　：カウンターダウンを表示する
// 最後の更新 : 2017.10.18
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
    class TimerUI
    {
        private Timer timer;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="timer">タイマー</param>
        public TimerUI(Timer timer)
        {
            this.timer = timer;
            timer.Initialize();
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public void Draw(Renderer renderer)
        {
            string gameTime = Convert.ToString(timer.Now() / 60.0f);
            int digit = 5;

            if(gameTime.Length > digit)
            {
                renderer.DrawNumber("Number", new Vector2(60, 330), gameTime, digit);
            }
            else
            {
                renderer.DrawNumber("Number",
                    new Vector2(60, 330),
                    gameTime,
                    gameTime.Length);
            }
        }
    }
}
