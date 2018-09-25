//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.15
// 内容　：時間を計るクラス
// 最後の更新 : 2017.10.15
//-------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility
{
    
    class Timer
    {
        private float currentTime;//現在の時間
        private float limitTime;//制限時間
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Timer()
        {
            limitTime = 60;//1秒, 60fps
            Initialize();
        }
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="second"></param>
        public Timer(float second)
        {
            limitTime = 60 * second;
            Initialize();
        }

        /// <summary>
        /// 初期化
        /// </summary>
        public void Initialize()
        {
            currentTime = limitTime;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            currentTime -= 1.0f;
            currentTime = Math.Max(currentTime, 0);
        }

        /// <summary>
        /// 時間になったか
        /// </summary>
        /// <returns></returns>
        public bool IsTime()
        {
            return currentTime <= 0.0f;
        }

        /// <summary>
        /// 現在の時間
        /// </summary>
        /// <returns></returns>
        public float Now()
        {
            return currentTime;
        }

        /// <summary>
        /// 最大の時間を切り替える
        /// </summary>
        /// <param name="limitTime">最大の時間</param>
        public void Change(float limitTime)
        {
            this.limitTime = limitTime;
            Initialize();
        }

        /// <summary>
        /// 割合
        /// </summary>
        /// <returns></returns>
        public float Rate()
        {
            return currentTime / limitTime;
        }
    }
}
