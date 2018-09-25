//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.18
// 内容　：果物の弾
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using BearGame_v2.Device;
using BearGame_v2.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Actor
{
    class FruitBullet
    {
        private MouseInput mouse;   // マウス入力
        private const float MouseRadius = 0.1f; // マウス半径
        private const float MaxDragRadius = 0.5f; // 最遠ドラッグ半径
        private static readonly Vector2 CenterOffset = new Vector2(0, 0.025f);  // 中心オフセット
        private Vector2 _mouseOffsetFromCenter; // 中心からのマウスオフセット
        public Vector2 DragPosition { get; private set; } // ドラッグ位置座標
        public bool IsBeingDragged { get; private set; } // ドラッグ中ですか？
        public Body Body { get; private set; } // Farseerの物理的なボディー
        public bool Launched { get; private set; } // 打ち上げましたか？
        public Vector2 Position // 位置座標
        {
            get { return Body.Position; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="body">Farseerの物理的なボディー</param>
        /// <param name="mouse">マウス入力</param>
        public FruitBullet(Body body, MouseInput mouse)
        {
            Body = body;
            this.mouse = mouse;
            ((PrefabUserData)Body.UserData).IsImmortal = true; // 不滅のキャラクター
            Launched = false;
        }

        /// <summary>
        /// 入力管理
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="mouseCondition">現在のマウス状況</param>
        /// <returns></returns>
        public bool HandleInput(GameTime gameTime, MouseCondition mouseCondition)
        {
            // マウス座標を取る。
            Vector2 mousePositionSim = Camera.Current.ScreenToSimulation(mouse.newCoor);
            
            // 左クリックを管理
            if (mouseCondition == MouseCondition.Clicked)
            {
                if ((Position - mousePositionSim).Length() < MouseRadius && !IsBeingDragged)
                {
                    IsBeingDragged = true; // ドラッグ状態を開始
                    _mouseOffsetFromCenter = Position - mousePositionSim; // 中心に比べるとクリック座標
                    DragPosition = Position;
                    return true;
                }
            }

            // ドラッグ状態
            if (mouseCondition == MouseCondition.Dragged && IsBeingDragged)
            {
                Vector2 pullPosition = mousePositionSim + _mouseOffsetFromCenter;　// 引く座標
                Vector2 dragVector = Position - pullPosition; // ドラッグのベクトル

                // 最遠のドラッグ座標より遠ければ
                if (dragVector.Length() < MaxDragRadius)
                {
                    DragPosition = pullPosition;
                }
                else
                {
                    dragVector.Normalize();
                    DragPosition = Position - Vector2.Multiply(dragVector, MaxDragRadius);
                }
                return true;
            }

            // 解放状態
            if (mouseCondition == MouseCondition.Released && IsBeingDragged)
            {
                IsBeingDragged = false;　// ドラッグ値はfalseに
                Vector2 dragVector = (Body.Position - DragPosition) * ((PrefabUserData)Body.UserData).ImpulseModifier; // 果物のボディーに物理的な力をあげる
                // インパルスをあげるけど、投げられた果物のアーチが見えるように、中心からちょっとずれる
                Vector2 centerWithOffset = Body.WorldCenter + CenterOffset;
                Body.ApplyLinearImpulse(dragVector, centerWithOffset);
                // 果物が投げられた
                Launched = true;
                return true;
            }
            return false;
        }
    }
}
