using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.src
{
    [EditorGroup("MyMod|Explosives")]
    [BaggedProperty("isFatal", false)]
    public class ConcussiveBlast : GrenadeBase
    {
        float _radius = 50f;
        public ConcussiveBlast(float xval, float yval) : base(xval, yval)
        {
            this.sprite = new SpriteMap(GetPath("concussiveblast2"), 9, 11);
            base.graphic = sprite;

            _editorName = "Concussive Blast";

            collisionOffset = new Vec2(-4.5f, -5.5f);
            collisionSize = new Vec2(9f, 11f);

            center = new Vec2(4.5f, 5.5f);

            editorTooltip = "#1 Pull pin. #2 Throw grenade. Order of operations is important here.";
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
            Push();
            //code from grenade
            SFX.Play("spring", 0.2f, -0.1f + Rando.Float(0.2f)); // why is there 
			RumbleManager.AddRumbleEvent(this.position, new RumbleEvent(RumbleIntensity.Heavy, RumbleDuration.Short, RumbleFalloff.Medium));
            
            Level.Remove(this);
            base.Explode();
        }

        // Physics portion of Push() taken from DuckFu's Moveset.DoQuack() method
        public virtual void Push()
        {

            foreach (PhysicsObject p in Level.CheckCircleAll<PhysicsObject>(new Vec2(this.x,this.y), _radius * 1.2f))
            {
                if (p.active)
                {
                    if (base.isServerForObject && !(p is Duck) && (!(p is Holdable) || ((p as Holdable).duck == null && (p as Holdable).equippedDuck == null)))
                    {
                        Fondle(p);
                    }
                    if(Level.CheckLine<Block>(this.position, p.position, p) == null)
                    {
                        float num = (float)Math.Atan2((double)p.y - (double)position.y, (double)p.x - (double)position.x);
                        p.hSpeed += _radius * 0.6f * (float)(4.0 / Math.Sqrt((double)(p.position - this.position).length / 2.0) * Math.Cos(num));
                        p.vSpeed += _radius * 0.6f * (float)(4.0 / Math.Sqrt((double)(p.position - this.position).length / 2.0) * Math.Sin(num));

                        p.vSpeed -= 0.1f;
                    }

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