//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.29
// 内容　：ゲームカメラ
// 最後の更新 : 2017.10.31
//-------------------------------------------------------

using BearGame_v2.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    // カメラの現在の状態
    public enum CameraCondition
    {
        PlayerFound, // プレイヤーを見つける
        Moveable, // 動かせる
        Stop // 止まる
    }

    class Camera
    {
        private float _offsetX; // ｘ座標のオフセット
        private Body _trackingBody; // どちらのゲームオブジェクトを見せる
        public bool MouseControlled { get; private set; } // マウスで動かせるのか？
        public Matrix TransformationMatrix { get; private set; } // 変換マトリックス
        public float CenterPointTarget { get; set; } // ターゲットの中心
        public Vector2 cursorPosition { get; private set; } // マウスの位置

        private MouseState mouse; // マウス

        public CameraCondition cameraCondition { get; set; } // カメラの現在の状態

        public static readonly Camera Current = new Camera();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        private Camera () {
            MouseControlled = false;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update (GameTime gameTime)
        {
            // プレイヤーを見つけられるとカメラは動かせない時
            if (cameraCondition == CameraCondition.PlayerFound || cameraCondition == CameraCondition.Stop)
            {
                if (_trackingBody != null)　// カメラはゲームオブジェクトを追いかけ
                {
                    // カメラを大切なゲームオブジェクトの座標を取って、
                    if (_trackingBody.Position.X * Constants.Scale != Constants.HalfScreenWidth + _offsetX)
                    {
                        _offsetX = Clamp(_trackingBody.Position.X * Constants.Scale - Constants.HalfScreenWidth);
                    }
                }
                // カメラを動かせる。
                TransformationMatrix = Matrix.CreateTranslation(-_offsetX, 0f, 0f);
            }
            else
            {
                // カメラをマウスで自由に動かせる。
                mouse = Mouse.GetState();
                if (mouse.X > (Constants.ScreenWidth - 30.0f) && _offsetX <= 1060f)
                {
                    _offsetX += 10f;
                }
                else if (mouse.X < 30.0f && _offsetX >= 10f)
                {
                    _offsetX -= 10f;
                }
                TransformationMatrix = Matrix.CreateTranslation(-_offsetX, 0f, 0f);
            }
        }

        /// <summary>
        /// カメラの動き範囲
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private float Clamp (float value)
        {
            return MathHelper.Clamp(value, 0, CenterPointTarget - Constants.HalfScreenWidth);
        }

        /// <summary>
        /// 大切なゲームオブジェクトを見せる
        /// </summary>
        /// <param name="body">ゲームオブジェクト</param>
        public void StartTracking (Body body)
        {
            _trackingBody = body;
        }

        /// <summary>
        /// さっき追いかけられたゲームオブジェクトを解放
        /// </summary>
        public void StopTracking()
        {
            _trackingBody = null;
        }

        /// <summary>
        /// 現在の追いかけられたゲームオブジェクト
        /// </summary>
        /// <returns>ゲームオブジェクト</returns>
        public Body GetTrackingBody()
        {
            return _trackingBody;
        }

        /// <summary>
        /// スクリーンの座標をワールドの座標に変換
        /// </summary>
        /// <param name="mousePosition">マウス座標位置</param>
        /// <returns>カメラのワールド座標位置</returns>
        public Vector2 ScreenToSimulation(Vector2 mousePosition)
        {
            return Vector2.Transform(mousePosition, Matrix.Invert(TransformationMatrix)) / Constants.Scale;
        }

        /// <summary>
        /// trueだったら、カメラを主人公の位置座標に戻る、自由に動かせる
        /// </summary>
        /// <param name="value"></param>
        public void CameraControlled (bool value)
        {
            MouseControlled = value;
            if (value == true)
            {
                TransformationMatrix = Matrix.CreateTranslation(0, 0, 0);
            }
        }
    }
}
