using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BearGame_v2.Device;
using BearGame_v2.Scene;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using BearGame_v2.Utility.BloomPostprocess;
using BearGame_v2.Utility.ParticleRelated;

namespace BearGame_v2
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphicsDeviceManager;
        private GameDevice gameDevice;
        private Renderer renderer;
        private SceneManager sceneManager;
        //private BloomComponent bloom;

        public Game1()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //bloom = new BloomComponent(this);
            //Components.Add(bloom);
            //bloom.Settings = new BloomSettings(null, 0.9f, 4f, 1f, 1, 1.5f, 1);
            Window.Title = "Pomegranate";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // ゲームーデバイスの実体生成
            gameDevice = new GameDevice(Content, GraphicsDevice);
            //描画オブジェクトの取得
            renderer = gameDevice.GetRenderer();

            sceneManager = new SceneManager();
            //sceneManager.Add(SceneType.Load, new Loader(gameDevice));
            sceneManager.Add(SceneType.Title, new Title(gameDevice));
            sceneManager.Add(SceneType.GamePlay, new GamePlay(gameDevice));
            sceneManager.Add(SceneType.Ending, new Ending(gameDevice, ((GamePlay)sceneManager.getScene(SceneType.GamePlay)).gameManager));
            sceneManager.Change(SceneType.Title);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            gameDevice.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            gameDevice.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                (Keyboard.GetState().IsKeyDown(Keys.Escape)))
                this.Exit();

            gameDevice.Update(gameTime);
            sceneManager.Update(gameTime);


            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            //bloom.BeginDraw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneManager.Draw(renderer, gameTime);
            
            base.Draw(gameTime);
        }
    }
}
