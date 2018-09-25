//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.26
// 内容　：定数の変数
// 最後の更新 : 2017.11.09
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility
{
    public static class Constants
    {
        public const float Scale = 100f;

        public static Color HalfTransparent { get; private set; }
        public static float HalfScreenWidth { get; private set; }
        public static float ScreenWidth { get; private set; }
        public static float ScreenHeight { get; private set; }
        public static Vector2 FloorPosition { get; private set; }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="graphics"></param>
        public static void Initialize(GraphicsDevice graphics)
        {
            HalfScreenWidth = graphics.Viewport.Width / 2f;
            ScreenWidth = graphics.Viewport.Width;
            ScreenHeight = graphics.Viewport.Height;
            FloorPosition = new Vector2(0, graphics.Viewport.Height * 0.875f);
            HalfTransparent = new Color(128, 128, 128, 128);
        }
    }
}
