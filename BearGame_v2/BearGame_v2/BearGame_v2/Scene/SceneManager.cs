//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.17
// 内容　：全部のシーンを管理するクラス
// 最後の更新 : 2017.10.17
//-------------------------------------------------------

using BearGame_v2.Device;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Scene
{
    class SceneManager
    {
        // シーンのディクショナリー
        private Dictionary<SceneType, IScene> scenes = new Dictionary<SceneType, IScene>();
        private IScene currentScene = null;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneManager()
        {}

        /// <summary>
        /// シーンをディクショナリーに追加
        /// </summary>
        /// <param name="name">シーンの名前</param>
        /// <param name="scene">シーンのインタフェース</param>
        public void Add(SceneType name, IScene scene)
        {
            if(scenes.ContainsKey(name))
            {
                return;
            }
            scenes.Add(name, scene);
        }

        /// <summary>
        /// シーンを切り替える
        /// </summary>
        /// <param name="name">次のシーン</param>
        public void Change(SceneType name)
        {
            if(currentScene != null)
            {
                currentScene.Shutdown();
            }
            currentScene = scenes[name];
            currentScene.Initialize();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime">ゲーム時間</param>
        public void Update(GameTime gameTime)
        {
            if(currentScene == null)
            {
                return;
            }
            currentScene.Update(gameTime);
            if (currentScene.IsEnd())
            {
                Change(currentScene.Next());
            }
        }

        /// <summary>
        /// 描画する
        /// </summary>
        /// <param name="renderer">レンダラー</param>
        /// <param name="gameTime">ゲーム時間</param>
        public void Draw(Renderer renderer, GameTime gameTime)
        {
            if(currentScene == null)
            {
                return;
            }
            currentScene.Draw(renderer, gameTime);
        }

        /// <summary>
        /// シーンを取り出す
        /// </summary>
        /// <param name="scene"></param>
        /// <returns></returns>
        public IScene getScene(SceneType scene)
        {
            return scenes[scene];
        }
    }
}
