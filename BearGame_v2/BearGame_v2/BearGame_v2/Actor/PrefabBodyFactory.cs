//-------------------------------------------------------
// 作成者：シスワントレサ
// 作成日：2017.10.16
// 内容　：全体のゲームオブジェクトのファクトリー
// 最後の更新 : 2017.11.02
//-------------------------------------------------------

using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BearGame_v2.Actor
{
    class PrefabBodyFactory
    {
        // ゲームオブジェクトのライブラリーを生成
        private static readonly Dictionary<PrefabType, Func<World, Vector2, Body>> Library =
            new Dictionary<PrefabType, Func<World, Vector2, Body>>();
        
        /// <summary>
        /// ゲームオブジェクトを定義して、ライブラリーの中に置いておく
        /// </summary>
        static PrefabBodyFactory()
        {
            Library.Add(PrefabType.GoldBox, (world, position) => CreateRectangle(world, position, "box_gold", 0.4f, 0.4f, 7));
            Library.Add(PrefabType.GrayBox, (world, position) => CreateRectangle(world, position, "box_grey", 0.4f, 0.4f, 7));
            Library.Add(PrefabType.Ball, (world, position) => CreateCircle(world, position, "Durian", 0.2f, 7));
            // 小さい果物
            Library.Add(PrefabType.Orange, (world, position) => CreateCircleFruits(world, position, "Orange", 0.13f, 10, 1.0f, Color.Orange));
            Library.Add(PrefabType.Momo, (world, position) => CreateCircleFruits(world, position, "Momo", 0.12f, 10, 1.0f, Color.LightPink));
            Library.Add(PrefabType.Strawberry, (world, position) => CreateCircleFruits(world, position, "Strawberry", 0.10f, 10, 0.85f, Color.DarkRed));
            Library.Add(PrefabType.CherryFruit, (world, position) => CreateCircleFruits(world, position, "CherryFruit", 0.14f, 10, 1.1f, Color.Red));
            Library.Add(PrefabType.Apple, (world, position) => CreateCircleFruits(world, position, "Apple", 0.14f, 10, 1.2f, Color.MediumVioletRed));
            Library.Add(PrefabType.Lime, (world, position) => CreateCircleFruits(world, position, "Lime", 0.11f, 10, 0.8f, Color.Lime)); // CHECK IF CAPSULE IS NEEDED
            // 大きい果物
            Library.Add(PrefabType.Durian, (world, position) => CreateCircleFruits(world, position, "Durian", 0.20f, 15, 2.2f, Color.LightGoldenrodYellow));
            Library.Add(PrefabType.Melon, (world, position) => CreateCircleFruits(world, position, "Melon", 0.16f, 15, 1.9f, Color.LightGreen));
            Library.Add(PrefabType.Pineapple, (world, position) => CreateCircleFruits(world, position, "Pineapple", 0.18f, 15, 1.9f, Color.Yellow));
            Library.Add(PrefabType.DragonFruit, (world, position) => CreateCircleFruits(world, position, "DragonFruit", 0.18f, 15, 1.8f, Color.IndianRed));
            // 特別な果物
            Library.Add(PrefabType.Pomegranate, (world, position) => CreateCircleFruits(world, position, "Pomegranate", 0.14f, 25, 1.1f, Color.Gold));
            // プレイヤー
            Library.Add(PrefabType.Nico, (world, position) => CreateCharacter(world, position, "Nico", 0.62f, 0.64f));
            Library.Add(PrefabType.Kuma, (world, position) => CreateCharacter(world, position, "Kuma", 0.65f, 0.67f)); // Kuma's HEIGHT is slightly lowered. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Library.Add(PrefabType.Cherry, (world, position) => CreateCharacter(world, position, "Cherry", 0.48f, 0.58f));
        }

        /// <summary>
        /// ゲームオブジェクトを作る
        /// </summary>
        /// <param name="type">ゲームオブジェクトのタイプ</param>
        /// <param name="world">どちらのワールドに置きたい</param>
        /// <param name="position">位置座標</param>
        /// <returns></returns>
        public static Body CreateBody(PrefabType type, World world, Vector2 position)
        {
            return Library[type](world, position);
        }

        /// <summary>
        /// 丸い形の果物を生成
        /// </summary>
        /// <param name="world">どちらのワールドに置きたい</param>
        /// <param name="position">位置座標</param>
        /// <param name="spriteName">スプライト名</param>
        /// <param name="radius">半径</param>
        /// <param name="damage">果物のダメージ</param>
        /// <param name="impulseModifier">インパルスの強さ</param>
        /// <param name="particleColor">パーティクル色</param>
        /// <returns></returns>
        private static Body CreateCircleFruits(World world, Vector2 position, string spriteName, float radius, int damage, float impulseModifier, Color particleColor)
        {
            Body body = BodyFactory.CreateCircle(world, radius, 1f, position);
            foreach (var x in body.FixtureList)
            {
                x.Restitution = 0.3f; // bounciness
                x.Friction = 1.0f;
            }
            body.UserData = new PrefabUserData
            {
                Origin = new Vector2(radius),
                SpriteName = spriteName,
                Damage = damage,
                ImpulseModifier = impulseModifier,
                ParticleColor = particleColor
            };
            body.AngularDamping = 2.0f;
            body.BodyType = BodyType.Dynamic;
            return body;
        }

        /// <summary>
        /// 四角のオブジェクトを生成
        /// </summary>
        /// <param name="world">どちらのワールドに置きたい</param>
        /// <param name="position">位置座標</param>
        /// <param name="spriteName">スプライト名</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <param name="hitPoints">HP</param>
        /// <returns></returns>
        private static Body CreateRectangle(World world, Vector2 position, string spriteName, float width, float height, int hitPoints)
        {
            Body body = BodyFactory.CreateRectangle(world, width, height, 1f, position);
            body.UserData = new PrefabUserData
            {
                Origin = new Vector2(width / 2f, height / 2f),
                SpriteName = spriteName,
                HitPoints = hitPoints,
                Damage = 30
            };
            body.BodyType = BodyType.Static;
            return body;
        }

        /// <summary>
        /// 丸いゲームオブジェクトを生成
        /// </summary>
        /// <param name="world">どちらのワールドに置きたい</param>
        /// <param name="position">位置座標</param>
        /// <param name="spriteName">スプライト名</param>
        /// <param name="radius">半径</param>
        /// <param name="hitPoints">HP</param>
        /// <returns></returns>
        private static Body CreateCircle(World world, Vector2 position, string spriteName, float radius, int hitPoints)
        {
            Body body = BodyFactory.CreateCircle(world, radius, 1f, position);
            body.UserData = new PrefabUserData
            {
                Origin = new Vector2(radius),
                SpriteName = spriteName,
                Damage = 30,
                HitPoints = hitPoints,
            };
            body.AngularDamping = 2.0f;
            body.BodyType = BodyType.Static;
            return body;
        }

        /// <summary>
        /// ゲームキャラクターを生成
        /// </summary>
        /// <param name="world">どちらのワールドに置きたい</param>
        /// <param name="position">位置座標</param>
        /// <param name="spriteName">スプライト名</param>
        /// <param name="width">幅</param>
        /// <param name="height">高さ</param>
        /// <returns></returns>
        private static Body CreateCharacter(World world, Vector2 position, string spriteName, float width, float height)
        {
            Body body = BodyFactory.CreateRectangle(world, width, height, 5f, position);
            
            body.UserData = new PrefabUserData
            {
                Origin = new Vector2(width / 2f, height / 2f),
                SpriteName = spriteName,
            };
            body.BodyType = BodyType.Dynamic;
            return body;
        }
    }
}
