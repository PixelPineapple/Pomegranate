//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.15
// 内容　：使ったデバイスのまとめのクラス
// 最後の更新 : 2017.11.13
//-------------------------------------------------------

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    class GameDevice
    {
        private Renderer renderer;  // レンダラー
        private InputState input;   // インプットステータス
        private MouseInput mouse;   // マウスインプット
        private ThePhysicsWorld _world; // 物理のワールド
        private Sound sound;    // サウンド

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="contentManager">コンテンツマネージャー</param>
        /// <param name="graphics">グラフィック</param>
        public GameDevice(ContentManager contentManager, GraphicsDevice graphics)
        {
            renderer = new Renderer(contentManager, graphics);
            input = new InputState();
            _world = new ThePhysicsWorld(graphics);
            mouse = new MouseInput();
            sound = new Sound(contentManager);
        }

        public void Initialize() { }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲームタイム</param>
        public void Update(GameTime gameTime)
        {
            input.Update();
            _world.Update(gameTime);
            mouse.Update(gameTime);
        }

        /// <summary>
        /// テクスチャをロード
        /// </summary>
        public void LoadContent()
        {
            // 描画
            renderer.LoadTexture("floor_long");
            renderer.LoadTexture("box_red");
            renderer.LoadTexture("box_gold");
            renderer.LoadTexture("box_grey");
            renderer.LoadTexture("ball");
            //// Small Fruits
            renderer.LoadTexture("Orange");
            renderer.LoadTexture("Momo");
            renderer.LoadTexture("Strawberry");
            renderer.LoadTexture("Apple");
            renderer.LoadTexture("CherryFruit");
            renderer.LoadTexture("Lime");
            //// Big Fruits
            renderer.LoadTexture("Durian");
            renderer.LoadTexture("Melon");
            renderer.LoadTexture("Pineapple");
            renderer.LoadTexture("DragonFruit");
            //// Special Fruits
            renderer.LoadTexture("Pomegranate");
            //// Character
            renderer.LoadTexture("Nico");
            renderer.LoadTexture("Kuma");
            renderer.LoadTexture("Cherry");
            //// Utility
            renderer.LoadTexture("Number");
            renderer.LoadTexture("Particle");
            renderer.LoadTexture("Particle_Star");
            renderer.LoadTexture("Smoke");
            renderer.LoadTexture("NicoWin");
            renderer.LoadTexture("KumaWin");
            renderer.LoadTexture("Judul");
            renderer.LoadTexture("SpaceToPlay_TEXT");
            renderer.LoadTexture("Title_BG");
            //// Scenery
            renderer.LoadTexture("Sky");
            renderer.LoadTexture("Grass_1");
            renderer.LoadTexture("Grass_2");
            renderer.LoadTexture("Tree");
            renderer.LoadTexture("Tree_Big");
            renderer.LoadTexture("River");
            renderer.LoadTexture("Mountain");
            renderer.LoadTexture("Small_Mountain");
            renderer.LoadTexture("Cloud");
            renderer.LoadTexture("Foreground");

            sound.LoadSE("Archery arrow release");

        }

        /// <summary>
        /// テクスチャをアンロード
        /// </summary>
        public void UnloadContent()
        {
            renderer.Unload();
        }

        /// <summary>
        /// レンダラークラスを取る
        /// </summary>
        /// <returns></returns>
        public Renderer GetRenderer()
        {
            return renderer;
        }

        /// <summary>
        /// インプットクラスを取る
        /// </summary>
        /// <returns></returns>
        public InputState GetInputState()
        {
            return input;
        }

        /// <summary>
        /// サウンドクラスを取る
        /// </summary>
        /// <returns></returns>
        public Sound GetSound()
        {
            return sound;
        }

        /// <summary>
        /// マウスインプットクラスを取る
        /// </summary>
        /// <returns></returns>
        public MouseInput GetMouseInput()
        {
            return mouse;
        }
        
        /// <summary>
        /// 物理のワールドクラスを取る
        /// </summary>
        /// <returns></returns>
        public ThePhysicsWorld GetPhysicsWorld()
        {
            return _world;
        }
    }
}
