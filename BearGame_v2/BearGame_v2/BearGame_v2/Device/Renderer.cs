//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.18
// 内容　：初期化されたゲームオブジェクトを描画する
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    class Renderer
    {
        private ContentManager contentManager;
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;

        // 複数画像管理
        private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content"></param>
        /// <param name="graphics"></param>
        public Renderer(ContentManager content, GraphicsDevice graphics)
        {
            contentManager = content;
            graphicsDevice = graphics;
            spriteBatch = new SpriteBatch(graphicsDevice);
        }

        /// <summary>
        /// ロードテクスチャ
        /// </summary>
        /// <param name="name">テクスチャ名</param>
        /// <param name="filepath"></param>
        public void LoadTexture(string name, string filepath = "./")
        {
            if (textures.ContainsKey(name))
            {
#if DEBUG // DEBUGモードの時のみ有効
                System.Console.WriteLine("この" + name + "はKeyで、すでに登録されています");
#endif
                // 処理終了
                return;
            }
            textures.Add(name, contentManager.Load<Texture2D>(filepath + name));
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }

        /// <summary>
        /// 画像の登録
        /// </summary>
        /// <param name="name"></param>
        /// <param name="texture"></param>
        public void LoadTexture(string name, Texture2D texture)
        {
            if (textures.ContainsKey(name))
            {
#if DEBUG //DEBUGモードの時のみ有効
                System.Console.WriteLine("この" + name + "はKeyで、すでに登録されています");
#endif
                //処理終了
                return;
            }
            textures.Add(name, texture);
        }

        public void Unload()
        {
            textures.Clear();
        }

        public void Begin()
        {
            spriteBatch.Begin();
        }

        public void Begin(SpriteSortMode spriteSort, BlendState blendS)
        {
            spriteBatch.Begin(spriteSort, blendS);
        }

        /// <summary>
        /// スプライトバッチを呼び出す
        /// </summary>
        /// <returns></returns>
        public SpriteBatch GetSpriteBatch()
        {
            return spriteBatch;
        }

        /// <summary>
        /// カメラを初期化
        /// </summary>
        public void CameraBegin()
        {
            spriteBatch.Begin(0, null, null, null, null, null, Camera.Current.TransformationMatrix);
        }

        public void End()
        {
            spriteBatch.End();
        }

        /// <summary>
        /// 画像の描画
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="alpha">透明値（透明：0.0f, 不透明：1.0f）</param>
        public void DrawTexture(string name, Vector2 position, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            spriteBatch.Draw(textures[name], position, Color.White * alpha);
        }

        public void DrawTexture(string name, Vector2 position, Color color)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            spriteBatch.Draw(textures[name], position, color);
        }

        /// <summary>
        /// 画像の描画（指定範囲）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="rect">画像の切り出し範囲</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string name, Vector2 position, Rectangle rect, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            spriteBatch.Draw(
                textures[name],  //画像
                position,        //位置
                rect,            //矩形の指定範囲（左上の座標x, y, 幅、高さ）
                Color.White * alpha);
        }

        /// <summary>
        /// （拡大縮小対応版）画像の描画
        /// </summary>
        /// <param name="name">アセット名ｎ</param>
        /// <param name="position">位置</param>
        /// <param name="scale">拡大縮小値</param>
        /// <param name="alpha">透明値</param>
        public void DrawTexture(string name, Vector2 position, Vector2 scale, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            spriteBatch.Draw(
                textures[name],  //画像
                position,        //位置
                null,            //切り取り範囲
                Color.White * alpha, //透過
                0.0f,            //回転
                Vector2.Zero,    //回転軸の位置
                scale,           //拡大縮小
                SpriteEffects.None,//表示反転効果
                0.0f             //スプライト表示深度
                );
        }

        public void DrawTexture(string name, Vector2 position, float scale, float rotation, Rectangle? source, Color color, Vector2 origin)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            spriteBatch.Draw(
                textures[name],
                position * scale,
                source,
                color,
                rotation,
                origin,
                1f,
                SpriteEffects.None,
                0f);
        }

        /// <summary>
        /// 数字の描画（整数版、簡易）
        /// </summary>
        /// <param name="name">アセット名</param>
        /// <param name="position">位置</param>
        /// <param name="number">描画したい数字</param>
        /// <param name="alpha">透明値</param>
        public void DrawNumber(string name, Vector2 position, int number, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            number = Math.Max(number, 0);

            foreach (var n in number.ToString())
            {
                spriteBatch.Draw(
                    textures[name],
                    position,
                    new Rectangle((n - '0') * 7, 0, 7, 10),
                    Color.White);
                //1桁ずらす
                position.X += 7;
            }

        }

        public void DrawNumber(string name, Vector2 position, string number, int digit, float alpha = 1.0f)
        {
            Debug.Assert(
                textures.ContainsKey(name),
                "アセット名が間違えていませんか？\n" +
                "大文字小文字間違っていませんか？\n" +
                "LoadTextureメソッドで読み込んでいますか？\n" +
                "プログラムを確認してください\n");

            for (int i = 0; i < digit; i++)
            {
                if (number[i] == '.')
                {
                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle(10 * 7, 0, 7, 10),
                        Color.White);
                }
                else
                {
                    //1文字分の数値文字を取得
                    char n = number[i];

                    spriteBatch.Draw(
                        textures[name],
                        position,
                        new Rectangle((n - '0') * 7, 0, 7, 10),
                        Color.White);
                }

                position.X += 7;
            }
        }
    }
}

