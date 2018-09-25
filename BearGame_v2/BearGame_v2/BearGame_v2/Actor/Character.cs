//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：キャラクターの抽象クラス
// 最後の更新 : 2017.11.02
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
    abstract class Character
    {
        /// <summary>
        /// Farseerの物理的なボディー
        /// </summary>
        public Body Body { get; private set; }

        /// <summary>
        /// キャラクターのモション
        /// </summary>
        public Motion motion;

        /// <summary>
        /// 位置座標
        /// </summary>
        public Vector2 Position
        {
            get { return Body.Position; }　// Farseerのボディーの位置座標をリターン
        }

        /// <summary>
        /// ボディーのネーム
        /// </summary>
        public string Name
        {
            get { return ((PrefabUserData)Body.UserData).SpriteName; }
        }

        /// <summary>
        /// 元の位置座標
        /// </summary>
        public Vector2 Origin
        {
            get { return ((PrefabUserData)Body.UserData).Origin; }
        }

        /// <summary>
        /// キャラクターのボディー
        /// </summary>
        /// <param name="body">Farseerの物理的なボディー</param>
        public Character(Body body)
        {
            Body = body;
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public abstract void Initialize();

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー管理</param>
        public abstract void Draw(Renderer renderer);
    }
}
