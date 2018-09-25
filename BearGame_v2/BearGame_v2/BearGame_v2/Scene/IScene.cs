//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：シーンのインタフェース
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
    interface IScene
    {
        /// <summary>
        /// 初期化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        void Update(GameTime gameTime);

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="gameTime">ゲーム時間</param>
        void Draw(Renderer renderer, GameTime gameTime);

        /// <summary>
        /// シャットダウン
        /// </summary>
        void Shutdown();

        /// <summary>
        /// シーンが終わったか？
        /// </summary>
        /// <returns>次のシーン</returns>
        bool IsEnd();

        /// <summary>
        /// 次のシーンに移動
        /// </summary>
        /// <returns>次のシーン</returns>
        SceneType Next();
    }
}
