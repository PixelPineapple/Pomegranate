//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.22
// 内容　：モションを管理
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility
{
    class Motion
    {
        private Range range; // 範囲
        private Timer timer; // モーション時間
        public int motionNumber; // モーション番号

        // 表示位置を番号で管理
        private Dictionary<int, Rectangle> rectangles = new Dictionary<int, Rectangle>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Motion()
        {
            Initialize(new Range(0, 0), new Timer());
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">タイマー</param>
        public Motion (Range range, Timer timer)
        {
            Initialize(range, timer);
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="range">範囲</param>
        /// <param name="timer">タイマー</param>
        public void Initialize (Range range, Timer timer)
        {
            this.range = range;
            this.timer = timer;
            motionNumber = range.First();
        }

        /// <summary>
        /// アニメーション画像を追加
        /// </summary>
        /// <param name="index"></param>
        /// <param name="rect"></param>
        public void Add(int index, Rectangle rect)
        {
            rectangles.Add(index, rect);
        }

        /// <summary>
        /// モションを更新
        /// </summary>
        private void MotionUpdate()
        {
            motionNumber += 1;
            if (range.IsOutOfRange(motionNumber))
            {
                motionNumber = range.First();
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update (GameTime gameTime)
        {
            if (range.IsOutOfRange())
            {
                return;
            }
            timer.Update();
            if (timer.IsTime())
            {
                timer.Initialize();
                MotionUpdate();
            }
        }

        /// <summary>
        /// 描画したい画像の順番
        /// </summary>
        /// <returns></returns>
        public Rectangle DrawingRange()
        {
            return rectangles[motionNumber];
        }
    }
}
