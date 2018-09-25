using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using BearGame_v2.Device;
using BearGame_v2.Utility;
using FarseerPhysics.Collision.Shapes;

namespace BearGame_v2.Actor
{
    class Kuma : Character
    {
        public enum KumaAnimation
        {
            EarlyState, // Only used as an opening
            Idle,
            Walk,
            Flirt,
            Hurt1,
            Critical
        }

        private KumaAnimation currentAnimation = KumaAnimation.EarlyState;

        public Kuma(Body body) : base (body)
        {
            ((PrefabUserData)body.UserData).HitPoints = 100;
        }

        public override void Initialize()
        {
            motion = new Motion();
            int counter = 0;
            for (int y = 0; y < 4; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    motion.Add(counter, new Rectangle(65 * x, 70 * y, 65, 70));
                    counter++;
                }
            }
            UpdateMotion(KumaAnimation.Flirt);
        }

        public override void Update(GameTime gameTime)
        {
            motion.Update(gameTime);
        }

        public void UpdateMotion(KumaAnimation changeTo)
        {
            if(changeTo == KumaAnimation.Idle && currentAnimation != KumaAnimation.Idle)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(0, 15), new Timer(0.05f));
            }
            if(changeTo == KumaAnimation.Walk && currentAnimation != KumaAnimation.Walk)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(16, 31), new Timer(0.1f));
            }
            if(changeTo == KumaAnimation.Flirt && currentAnimation != KumaAnimation.Flirt)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(32, 47), new Timer(0.1f));
            }
            if(changeTo == KumaAnimation.Hurt1 && currentAnimation != KumaAnimation.Hurt1)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(48, 52), new Timer(0.1f));
            }
            if(changeTo == KumaAnimation.Critical && currentAnimation != KumaAnimation.Critical)
            {
                currentAnimation = changeTo;
                motion.Initialize(new Range(53, 57), new Timer(0.1f));
            }
        }

        public void MoveKuma()
        {
            Body.ApplyForce(new Vector2(-3f, 0));
        }

        public override void Draw(Renderer renderer)
        {
            var prefabUserData = (PrefabUserData)Body.UserData;
            switch (prefabUserData.Status)
            {
                case BodyStatus.Active:
                    renderer.DrawTexture(
                        Name,
                        Position,
                        Constants.Scale,
                        Body.Rotation,
                        motion.DrawingRange(),
                        Color.White,
                        (Origin * Constants.Scale));
                    break;
            }
        }
    }
}
