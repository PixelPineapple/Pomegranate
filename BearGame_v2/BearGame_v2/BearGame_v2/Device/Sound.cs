//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.18
// 内容　：サウンドを管理するクラス　「BGM　と　SE」
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    class Sound
    {
        private ContentManager contentManager;

        private Dictionary<string, Song> bgms;
        private Dictionary<string, SoundEffect> soundEffects;
        private Dictionary<string, SoundEffectInstance> seInstances;
        private List<SoundEffectInstance> sePlayList;

        private String currentBGM;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="content">コンテンツマネージャー</param>
        public Sound(ContentManager content)
        {
            contentManager = content;
            MediaPlayer.IsRepeating = true;
            bgms = new Dictionary<string, Song>();
            soundEffects = new Dictionary<string, SoundEffect>();
            seInstances = new Dictionary<string, SoundEffectInstance>();
            sePlayList = new List<SoundEffectInstance>();

            currentBGM = null;
        }

        /// <summary>
        /// エラーが出るときのメッセージ
        /// </summary>
        /// <param name="name">サウンド名</param>
        /// <returns></returns>
        private string ErrorMessage(string name)
        {
            return "再生する音データのアセット名(" + name + ")がありません。\n"
                +
                "アセット名の確認、Dictionaryに登録されているか確認してください\n";
        }

        #region　BGM関連処理

        /// <summary>
        /// BGMをロード
        /// </summary>
        /// <param name="name">BGM名</param>
        /// <param name="filepath">ファイルパス</param>
        public void LoadBGM(string name, string filepath = "./")
        {
            if (bgms.ContainsKey(name))
            {
                return;
            }
            bgms.Add(name, contentManager.Load<Song>(filepath + name));

        }

        /// <summary>
        /// BGMを止まるか？
        /// </summary>
        /// <returns></returns>
        public bool IsStoppedBGM()
        {
            return (MediaPlayer.State == MediaState.Stopped);
        }

        /// <summary>
        /// BGMはまだプレイ？
        /// </summary>
        /// <returns></returns>
        public bool IsPlayingBGM()
        {
            return (MediaPlayer.State == MediaState.Playing);
        }

        /// <summary>
        /// BGMはポーズされたか？
        /// </summary>
        /// <returns></returns>
        public bool IsPausedBGM()
        {
            return (MediaPlayer.State == MediaState.Paused);
        }

        /// <summary>
        /// BGMを止まらせる
        /// </summary>
        public void StopBGM()
        {
            MediaPlayer.Stop();
            currentBGM = null;
        }

        /// <summary>
        /// BGMをプレイ
        /// </summary>
        /// <param name="name"></param>
        public void PlayBGM(string name)
        {
            Debug.Assert(bgms.ContainsKey(name), ErrorMessage(name));

            if (currentBGM == name)
            {
                return;
            }

            if (IsPlayingBGM())
            {
                StopBGM();
            }

            MediaPlayer.Volume = 0.5f;

            currentBGM = name;

            MediaPlayer.Play(bgms[currentBGM]);
        }

        /// <summary>
        /// BGMはループするか？
        /// </summary>
        /// <param name="loopFlag"></param>
        public void ChangeBGMLoopFlag(bool loopFlag)
        {
            MediaPlayer.IsRepeating = loopFlag;
        }

        #endregion

        #region WAV関連

        /// <summary>
        /// WAVをロード
        /// </summary>
        /// <param name="name">WAV名</param>
        /// <param name="filepath">ファイルパス</param>
        public void LoadSE(string name, string filepath = "./")
        {
            if (soundEffects.ContainsKey(name))
            {
                return;
            }

            soundEffects.Add(name, contentManager.Load<SoundEffect>(filepath + name));
        }

        /// <summary>
        /// SEインスタンスを追加する
        /// </summary>
        /// <param name="name"></param>
        public void CreateSEInstance(string name)
        {
            if (seInstances.ContainsKey(name))
            {
                return;
            }

            Debug.Assert(soundEffects.ContainsKey(name),
                "先に" + name + "の読み込み処理をしてください");

            seInstances.Add(name, soundEffects[name].CreateInstance());
        }

        /// <summary>
        /// SEをプレイ
        /// </summary>
        /// <param name="name"></param>
        public void PlaySE(string name)
        {
            Debug.Assert(soundEffects.ContainsKey(name), ErrorMessage(name));

            soundEffects[name].Play();
        }

        /// <summary>
        /// SEインスタンスをプレイ
        /// </summary>
        /// <param name="name"></param>
        /// <param name="loopFlag"></param>
        public void PlaySEInstance(string name, bool loopFlag = false)
        {
            Debug.Assert(seInstances.ContainsKey(name), ErrorMessage(name));

            var data = seInstances[name];
            data.IsLooped = loopFlag;
            data.Play();
            sePlayList.Add(data);
        }

        /// <summary>
        /// SEを止まらせる
        /// </summary>
        public void StoppedSE()
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing)
                {
                    se.Stop();
                }
            }
        }

        /// <summary>
        /// SEをポーズ
        /// </summary>
        /// <param name="name"></param>
        public void PausedSE(string name)
        {
            foreach (var se in sePlayList)
            {
                if (se.State == SoundState.Playing)
                {
                    se.Stop();
                }
            }
        }

        /// <summary>
        /// 全部ストップされたSEをプレイリストから消す
        /// </summary>
        public void RemoveSE()
        {
            sePlayList.RemoveAll(se => (se.State == SoundState.Stopped));
        }

        #endregion

        /// <summary>
        /// サウンドをアンロード
        /// </summary>
        public void Unload()
        {
            bgms.Clear();
            soundEffects.Clear();
            sePlayList.Clear();
        }
    }
}
