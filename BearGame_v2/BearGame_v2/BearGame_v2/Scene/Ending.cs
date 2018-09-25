//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：ゲームシーン　-　エンディング
// 最後の更新 : 2017.10.17
//-------------------------------------------------------

using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Scene
{
    class Ending : IScene
    {
        private InputState input;
        private bool endFlag;
        private GameManager gameManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="gameDevice">ゲームデバイス</param>
        /// <param name="gameManager">ゲームマネージャー</param>
        public Ending(GameDevice gameDevice, GameManager gameManager)
        {
            input = gameDevice.GetInputState();
            endFlag = false;
            this.gameManager = gameManager;
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
            if (input.GetKeyTrigger(Microsoft.Xna.Framework.Input.Keys.Space))
            {
                endFlag = true;
            }
        }

        /// <summary>
        /// シーンが終わったか？
        /// </summary>
        /// <returns>終了フラグ</returns>
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
            return SceneType.Title;
        }

        /// <summary>
        /// シャットダウン
        /// </summary>
        public void Shutdown() { }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="gameTime"></param>
        public void Draw(Renderer renderer, GameTime gameTime)
        {
            renderer.Begin();

            // 勝つの画像を見せる
            if (gameManager.isPlayerWin == GameState.WIN)
            {
                // Draw WIN
                renderer.DrawTexture("NicoWin", Vector2.Zero);
            }
            // 負けるの画像を見せる
            else if (gameManager.isPlayerWin == GameState.LOSE)
            {
                // Draw LOSE
                renderer.DrawTexture("KumaWin", Vector2.Zero);
            }

            renderer.End();
        }
    }
}
