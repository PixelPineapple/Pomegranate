//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.11.02
// 内容　：全部のシーンを管理するクラス
// 最後の更新 : 2017.11.03
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Utility.ParticleRelated
{
   static  class ColorUtil
    {
        /// <summary>
        /// 色からHSVに
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static Vector3 ColorToHSV(Color color)
        {
            Vector3 c = color.ToVector3();
            float v = Math.Max(c.X, Math.Max(c.Y, c.Z));
            float chroma = v - Math.Min(c.X, Math.Min(c.Y, c.Z));

            if (chroma == 0f)
                return new Vector3(0, 0, v);

            float s = chroma / v;

            if (c.X >= c.Y && c.Y >= c.Z)
            {
                float h = (c.Y - c.Z) / chroma;
                if (h < 0)
                    h += 6;
                return new Vector3(h, s, v);
            }
            else if (c.Y >= c.Z && c.Y >= c.X)
                return new Vector3((c.Z - c.X) / chroma + 2, s, v);
            else
                return new Vector3((c.X - c.Y) / chroma + 4, s, v);
        }

        /// <summary>
        /// HSVから色に
        /// </summary>
        /// <param name="hsv"></param>
        /// <returns></returns>
        public static Color HSVToColor(Vector3 hsv)
        {
            return HSVToColor(hsv.X, hsv.Y, hsv.Z);
        }

        /// <summary>
        /// HSVから色に
        /// </summary>
        /// <param name="h"></param>
        /// <param name="s"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Color HSVToColor(float h, float s, float v)
        {
            if (h == 0 && s == 0)
                return new Color(v, v, v);

            float c = s * v;
            float x = c * (1 - Math.Abs(h % 2 - 1));
            float m = v - c;

            if (h < 1) return new Color(c + m, x + m, m);
            else if (h < 2) return new Color(x + m, c + m, m);
            else if (h < 3) return new Color(m, c + m, x + m);
            else if (h < 4) return new Color(m, x + m, c + m);
            else if (h < 5) return new Color(x + m, m, c + m);
            else return new Color(c + m, m, x + m);
        }
    }
}
