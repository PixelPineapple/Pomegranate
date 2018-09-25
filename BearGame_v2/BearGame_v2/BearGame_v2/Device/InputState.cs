//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：キーボード入力を管理するクラス
// 最後の更新 : 2017.10.29
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    class InputState
    {
        private KeyboardState currentKey; // 現在のキー
        private KeyboardState previousKey; // 1フレーム前のキー
        private float degree;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public InputState() { }

        /// <summary>
        /// キーを押されたか？
        /// </summary>
        /// <param name="key">どちらのキー</param>
        /// <returns></returns>
        public bool CheckDownKey(Keys key)
        {
            if (currentKey.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 撃つ度を設定
        /// </summary>
        public void UpdateDegree()
        {
            if (degree > 90) { degree = 90; }
            else if (degree < 0) { degree = 0; }

            if (CheckDownKey(Keys.Z))
            {
                degree += 1;
            }
            if (CheckDownKey(Keys.X))
            {
                degree -= 1;
            }
        }

        /// <summary>
        /// キーの更新
        /// </summary>
        /// <param name="keyState"></param>
        private void UpdateKey(KeyboardState keyState)
        {
            previousKey = currentKey; // 前回のフレームに押されたキー
            currentKey = keyState; // 現在のフレームに押されたキー
        }

        /// <summary>
        /// 別のキーを押されたか？
        /// </summary>
        /// <param name="key">どちらのキー</param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key)
        {
            bool current = currentKey.IsKeyDown(key);
            bool previous = previousKey.IsKeyDown(key);

            return current && !previous;
        }

        /// <summary>
        /// IsKeyDown(Keys key)を呼ぶメソッド
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetKeyTrigger(Keys key)
        {
            return IsKeyDown(key);
        }

        /// <summary>
        /// 現在のキーをまだ押されたか？
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetKeyState(Keys key)
        {
            return currentKey.IsKeyDown(key);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            var keyState = Keyboard.GetState();
            UpdateKey(keyState); // キーの更新
            UpdateDegree();
        }
    }
}
