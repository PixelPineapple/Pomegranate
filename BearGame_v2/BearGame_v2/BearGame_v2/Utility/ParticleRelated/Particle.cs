//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.11.02
// 内容　：パーティクル
// 最後の更新 : 2017.11.03
//-------------------------------------------------------

using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility.ParticleRelated
{
    class Particle
    {
        private string name;
        private Vector2 initialPosition;
        private Vector2 position;
        private Vector2 velocity;
        private Color color;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name">ネーム</param>
        /// <param name="position">パーティクルのポジション</param>
        /// <param name="velocity">速度</param>
        /// <param name="color">色</param>
        public Particle(string name, Vector2 position, Vector2 velocity, Color color)
        {
            this.name = name;
            initialPosition = position * Constants.Scale;
            this.position = position * Constants.Scale;
            this.velocity = velocity;
            this.color = color;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            position += velocity;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        public void Draw(Renderer renderer)
        {
            renderer.DrawTexture(name, position, color);
        }

        /// <summary>
        /// パーティクルが元の位置から遠すぎると無くなる
        /// </summary>
        /// <returns></returns>
        public bool IsDead()
        {
            return Vector2.Distance(position, initialPosition) >= 30.0f;
        }

    }
}
