//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.16
// 内容　：Farseerライブラリーのワールド
// 最後の更新 : 2017.10.23
//-------------------------------------------------------

using BearGame_v2.Actor;
using BearGame_v2.Utility;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Device
{
    class ThePhysicsWorld
    {
        private World _world;

        /// <summary>
        /// 物理のワールド
        /// </summary>
        public World TheWorld
        {
            get { return _world; }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="graphics">XNAのグラフィックデバイス</param>
        public ThePhysicsWorld(GraphicsDevice graphics)
        {
            _world = new World(new Vector2(0f, 6f));
            Initialize(graphics);
        }
        
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="graphics">グラフィックデバイス</param>
        public void Initialize(GraphicsDevice graphics)
        {
            Constants.Initialize(graphics); // 定数変数を初期化

            float simulatedHeight = Constants.FloorPosition.Y / Constants.Scale;
            float simulatedWidth = (graphics.Viewport.Width / Constants.Scale) * 3;

            // 物理地面を作る
            BodyFactory.CreateEdge(_world, new Vector2(0.0f, simulatedHeight), new Vector2(simulatedWidth, simulatedHeight));

            // 世界で当たり判定を初期化
            _world.ContactManager.BeginContact = BeginContact;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            _world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }

        /// <summary>
        /// 物理の世界の当たり判定を管理する
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        public bool BeginContact(Contact contact)
        {
            Body bodyA = contact.FixtureA.Body;
            Body bodyB = contact.FixtureB.Body;
            
            // 当たるときのスピードを計算
            Manifold worldManifold;
            contact.GetManifold(out worldManifold);
            ManifoldPoint p = worldManifold.Points[0];
            Vector2 vA = bodyA.GetLinearVelocityFromLocalPoint(p.LocalPoint);
            Vector2 vB = bodyB.GetLinearVelocityFromLocalPoint(p.LocalPoint);
            float approachVelocity = Math.Abs(Vector2.Dot(vB - vA, worldManifold.LocalNormal));
            
            // ヒットポイントを下げる
            ProcessContact(contact, bodyA, bodyB, approachVelocity);
            ProcessContact(contact, bodyB, bodyA, approachVelocity);

            return true;
        }

        /// <summary>
        /// コンタクトプロセス
        /// </summary>
        /// <param name="contact">当たる</param>
        /// <param name="body"></param>
        /// <param name="other"></param>
        /// <param name="approachVelocity"></param>
        private void ProcessContact(Contact contact, Body body, Body other, float approachVelocity)
        {
            PrefabUserData userData = body.UserData as PrefabUserData;
            PrefabUserData otherUserData = other.UserData as PrefabUserData;

            // ゲームオブジェクトはデータを持ってば、ヒットポイントを下がる
            if (( userData != null ))
            {
                if (!userData.IsImmortal && body.BodyType != BodyType.Static)　// 不滅のキャラクターでわない
                {
                    var approachingSpeed = (int)Math.Round(approachVelocity); // オブジェクトのスピード
                    if (approachingSpeed >= 5)
                    {
                        userData.HitPoints -= otherUserData.Damage;　// ヘルスポイントを下がる
                    }
                    if (userData.HitPoints <= 0) // ゲームオブジェクトがなくなった。
                    {
                        contact.Enabled = false;
                        body.IsSensor = true;
                        // mark this status as ToBeDestroyed
                        userData.Status = BodyStatus.ToBeDestroyed;
                    }
                }
                else if (body.BodyType == BodyType.Static)　// 不滅のキャラクター
                {
                    body.BodyType = BodyType.Dynamic;
                    userData.IsImmortal = true;
                    userData.FallFromTree = true;
                }
            }
        }
    }
}
