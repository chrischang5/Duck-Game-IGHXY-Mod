using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.IGHXY
{
    [EditorGroup("MyMod|Explosives")]
    [BaggedProperty("isFatal", false)]

    public class SpringGrenade : GrenadeBase
    {
        float _radius = 250f;

        public SpringGrenade(float xval, float yval) : base(xval, yval)
        {
            sprite = new SpriteMap(GetPath("concussiveblast2"), 9, 11);
            base.graphic = sprite;

            _editorName = "Spring Grenade";

            collisionOffset = new Vec2(-4.5f, -5.5f);
            collisionSize = new Vec2(9f, 11f);

            center = new Vec2(4.5f, 5.5f);

            editorTooltip = "#1 Pull pin. #2 Bounce! Sends enemy ducks flying!";
            _bio = "Move back!";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            sprite.frame = HasPin ? 0 : 1;
            base.Update();
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            base.OnSoftImpact(with, from);
        }

        public override void Explode()
        {
            if (base.isServerForObject)
            {
                Push();

                SFX.Play("spring", 0.2f, -0.1f + Rando.Float(0.2f));
                RumbleManager.AddRumbleEvent(position, new RumbleEvent(RumbleIntensity.Heavy, RumbleDuration.Short, RumbleFalloff.Medium));

                Level.Remove(this);
                base.Explode();
            }

        }

        public virtual void Push()
        {

            foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(new Vec2(this.x, this.y), this._radius * 1.2f))
            {
                // x, y values sourced from DuckFu's Moveset.DoQuack() method
                float x = _radius * 0.6f * (float)(4.0 / Math.Sqrt((double)(p.position - position).length / 2.0) * Math.Cos(Rando.Float(0.2f)));
                float y = _radius * 0.6f * (float)(4.0 / Math.Sqrt((double)(p.position - position).length / 2.0) * Math.Sin(Rando.Float(0.2f))) - 0.1f;
                
                // Thank you to DanTheDanMan on QC Discord for this!
                if (p is Duck && !p.isServerForObject && Level.CheckLine<Block>(position, p.position, p) == null && p.active)
                {
                    Send.Message(new NMPush(p as Duck, new Vec2(x, y)), p.connection);
                }

            }
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (!HasPin)
            {
                Explode();
            }
            if (pullOnImpact)
            {
                OnPressAction();
            }
            base.OnSolidImpact(with, from);
        }
    }

}