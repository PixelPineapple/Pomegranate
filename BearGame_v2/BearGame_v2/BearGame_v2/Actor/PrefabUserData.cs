//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.15
// 内容　：ゲームオブジェクトのプロパティ
// 最後の更新 : 2017.11.02
//-------------------------------------------------------

using BearGame_v2.Utility;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Actor
{
    // ゲームオブジェクトの現在の状態
    public enum BodyStatus
    {
        Active, // アクティブ
        ToBeDestroyed, // 次のフレームに破壊される
        Destroyed // 破壊された
    }

    class PrefabUserData
    {
        public string SpriteName { get; set; } // スプライト名
        public Vector2 Origin { get; set; } // 元の座標位置
        public int Damage { get; set; } // ダメージ
        public int HitPoints { get; set; } // ヒットポイント
        public BodyStatus Status { get; set; } // 現在の状態
        public bool IsImmortal { get; set; } // 不滅のゲームオブジェクトか？
        public float ImpulseModifier { get; set; } // どのぐらいインパルスを上げるのか？
        public Color ParticleColor { get; set; } // パーティクルの色
        public bool FallFromTree { get; set; } // 木に落ちるのか？
        public ExplosionAnimation ExplosionAnimation { get; set; } // 爆発のアニメーション
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrefabUserData()
        {
            ExplosionAnimation = new ExplosionAnimation("Smoke"); // 爆発のアニメーションを設定
        }

        /// <summary>
        /// 破壊するメソッド
        /// </summary>
        /// <param name="body">ゲームオブジェクト</param>
        public void Destroy(Body body)
        {
            Status = BodyStatus.Destroyed;
            ExplosionAnimation.Activate(body);
        }
    }
}
