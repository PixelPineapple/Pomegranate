//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：爆発のアニメーション
// 最後の更新 : 2017.10.18
//-------------------------------------------------------

using BearGame_v2.Actor;
using BearGame_v2.Device;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility
{
    class ExplosionAnimation
    {
        private bool _isActive;
        private Vector2 _position;
        private Vector2 _offset;

        private float _transitionValue;

        private TimeSpan _timeToLive = TimeSpan.FromSeconds(0.4);

        private readonly string _spritename;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="spritename"></param>
        public ExplosionAnimation(string spritename)
        {
            _spritename = spritename;
        }

        /// <summary>
        /// ボディーを活性化する
        /// </summary>
        /// <param name="body"></param>
        public void Activate(Body body)
        {
            _position = body.Position * Constants.Scale;
            _offset = new Vector2(30, 30);

            _transitionValue = 0f;
            _isActive = true;
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="elapsedGameTime">経過時間</param>
        public void Draw(Renderer renderer, GameTime elapsedGameTime)
        {
            if (_isActive)
            {
                UpdateTransition(elapsedGameTime);

                int colorChannel = 255 - (int)(_transitionValue * 255);
                int alphaChannel = 160 - (int)(_transitionValue * 160);
                Color color = new Color(colorChannel, colorChannel, colorChannel, alphaChannel);
                
                renderer.DrawTexture(_spritename, _position - _offset, color);
               
            }
        }

        /// <summary>
        /// 移動する更新
        /// </summary>
        /// <param name="elapsedGameTime">経過時間</param>
        private void UpdateTransition(GameTime elapsedGameTime)
        {
            // 移動するつもり範囲
            float transitionDelta = (float)(elapsedGameTime.ElapsedGameTime.TotalMilliseconds /
                _timeToLive.TotalMilliseconds);
            // 座標位置を更新
            _transitionValue += transitionDelta;
            // 移動するの範囲の最後に到着すれば
            if(_transitionValue >= 1)
            {
                _transitionValue = 1f;
                _isActive = false;
            }
        }
    }
}
