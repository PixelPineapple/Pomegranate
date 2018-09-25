//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.27
// 内容　：マウス入力を管理するクラス
// 最後の更新 : 2017.10.27
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    // マウスの状況
    public enum MouseCondition
    {
        Clicked,      //　クリック
        Dragged,    //　ドラッグ
        Released    //　解放
    }

    class MouseInput
    {
        private MouseState oldState;　// 前回のフレームの状態
        private MouseState newState;    // 現在の状態
        public Vector2 newCoor { get; private set; }    // 新しいの座標位置
        public Vector2 clickedCoor { get; private set; }    // クリック座標位置

        public MouseCondition Condition { get; private set; }   // マウスの状況

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MouseInput()
        {
            Condition = MouseCondition.Released;　// マウスを解放状況に
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime)
        {
            GetMouseCondition(); // マウス状態を取り出す
        }

        /// <summary>
        /// マウス状態を取り出す
        /// </summary>
        /// <returns></returns>
        public MouseCondition GetMouseCondition()
        {
            newState = Mouse.GetState(); // 現在のマウスの状態
            // マウスがクリックされた
            if (newState.LeftButton == ButtonState.Pressed && oldState.LeftButton == ButtonState.Released)
            {
                clickedCoor = new Vector2(newState.X, newState.Y);
                Condition = MouseCondition.Clicked;
            }
            // クリックしたままでドラッグされた
            if(oldState.LeftButton == ButtonState.Pressed && Vector2.Distance(clickedCoor, newCoor) >= 5)
            {
                Condition = MouseCondition.Dragged;
            }
            // 押されたマウスボタンを解放
            if(newState.LeftButton == ButtonState.Released && oldState.LeftButton == ButtonState.Released)
            {
                Condition = MouseCondition.Released;
            }
            newCoor = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
            oldState = newState;
            return Condition;
        }
    }
}
