using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.IGHXY
{
    [EditorGroup("MyMod|Explosives")]
    [BaggedProperty("isFatal", false)]

    // Goal: Have pin be an impact-only hit
    // have no pin be a timer
    public class NinjaSmoke : GrenadeBase
    {
        bool startedPinCount = false;
        public NinjaSmoke(float xval, float yval) : base(xval, yval)
        {
            this.sprite = new SpriteMap(GetPath("ninjasmoke"), 7, 10);
            base.graphic = sprite;

            _editorName = "Ninja Smoke";

            collisionOffset = new Vec2(-3.5f, -5f);
            collisionSize = new Vec2(7f, 10f);
            Timer = 10000000f; // huge number for pin behaviour

            center = new Vec2(3.5f, 5f);

            editorTooltip = "#1 Pull pin. #2 Throw grenade. Order of operations is important here.";
            _bio = "Smoke's Down!";
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update()
        {
            sprite.frame = HasPin ? 0 : 1;

            // if smoke does not have pin, update timer value (But only do it once)
            if (!HasPin && !startedPinCount)
            {
                startedPinCount = true;
                Timer = 1.2f;
            }
            base.Update();
        }

        public override void OnSoftImpact(MaterialThing with, ImpactedFrom from)
        {
            // Explode instantly if hit anything and has a pin
            if (!HasPin)
            {
                Explode();
            }
            base.OnSoftImpact(with, from);
        }

        public override void Explode()
        {
            Smoke();
            SFX.Play(GetPath("sounds/flashGrenadeExplode.wav"));
            SFX.Play(GetPath("sounds/flashbang_csgo.wav"));
            Level.Remove(this);
            base.Explode();
        }

        public virtual void Smoke()
        {
            for (int i = 0; i < 300; ++i)
            {
                MusketSmoke musketSmoke = new MusketSmoke(this.x - 16f + Rando.Float(32f) + this.offDir * 10f, this.y - 16f + Rando.Float(32f));
                musketSmoke.depth = (Depth)((float)(.9f + (float)i * (1f / 1000f)));
                Level.Add((Thing)musketSmoke);
            }
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {

            if (!HasPin)
            {
                // if smoke has pin, explode on solid impact
                Explode();
            }
            if (pullOnImpact)
            {
                OnPressAction();
            }
            base.OnSolidImpact(with, from);
        }

        public override void OnPressAction()
        {
            if (HasPin)
            {
                // if the pin exists, do not update timer
                HasPin = false;
            }

        }

    }

}
