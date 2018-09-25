//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.11.02
// 内容　：背景
// 最後の更新 : 2017.11.03
//-------------------------------------------------------

using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Actor
{
    class Scenery
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Scenery() { }

        /// <summary>
        /// 雲を描く
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="initialPosition">元の座標位置</param>
        /// <param name="movement">移動</param>
        public void DrawClouds(Renderer renderer, Vector2 initialPosition, float movement)
        {
            renderer.DrawTexture("Cloud", initialPosition, 1, 0, null, Color.White, Vector2.Zero);
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture("Sky", new Vector2(0, -5.0f));
            renderer.DrawTexture("River", new Vector2(0, 255.0f));
            renderer.DrawTexture("Grass_2", new Vector2(0, 185.0f));
            renderer.DrawTexture("Grass_1", new Vector2(0, 115.0f));
            DrawClouds(renderer, new Vector2 (270, 10), 10);
        }
    }
}
