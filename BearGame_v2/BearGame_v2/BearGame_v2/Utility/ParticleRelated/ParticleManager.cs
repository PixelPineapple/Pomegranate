//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.11.02
// 内容　：パーティクルを管理するクラス
// 最後の更新 : 2017.11.03
//-------------------------------------------------------

using BearGame_v2.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility.ParticleRelated
{
    class ParticleManager
    {
        private List<Particle> particles = new List<Particle>();

        // 表示するか？
        public bool IsActive
        {
            get; private set;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ParticleManager()
        {
            IsActive = false;
        }

        /// <summary>
        /// リストを追加
        /// </summary>
        /// <param name="particle"></param>
        public void Add(Particle particle)
        {
            particles.Add(particle);
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void Update()
        {
            if (IsActive)
            {
                particles.ForEach(particle => particle.Update());
            }
            particles.RemoveAll(particle => particle.IsDead());
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer"></param>
        public void Draw(Renderer renderer)
        {
            particles.ForEach(particle => particle.Draw(renderer));
        }

        /// <summary>
        /// パーティクルマネジャーをオンにする
        /// </summary>
        public void SwitchOn() { IsActive = true; }

        /// <summary>
        /// パーティクルマネジャーをオフにする
        /// </summary>
        public void SwitchOff()
        {
            IsActive = false;
            particles.Clear();
        }
    }
}
