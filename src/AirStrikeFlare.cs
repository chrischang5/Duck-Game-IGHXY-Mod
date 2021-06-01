using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace DuckGame.IGHXY
{
    public class AirStrikeFlare : GrenadeBase
    {
        public AirStrikeFlare(float xval, float yval) : base(xval, yval)
        {
            this.sprite = new SpriteMap(GetPath("ninjasmoke"), 7, 10); // Create airstrike asset
            base.graphic = sprite;

            _editorName = "Airstrike Flare";

            collisionOffset = new Vec2(-3.5f, -5f);
            collisionSize = new Vec2(7f, 10f);
            Timer = 1.2f;

            center = new Vec2(3.5f, 5f);

            editorTooltip = "#1 Pull pin. #2 Throw grenade. Order of operations is important here.";
            _bio = "Here comes the party!";
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
            if (!HasPin)
            {
                // Have it stop on boxes and other props
            }
            base.OnSoftImpact(with, from);
        }

        public override void Explode()
        {
            // Signal the airstrike
            Signal(); 
            // Play some airstrike sound from WW2
            Level.Remove(this);
            base.Explode();
        }

        public virtual void Signal()
        {
            // somehow signal in an airstrike ???
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (!HasPin)
            {
                Explode();
                Level.Remove(this);
            }
            if (pullOnImpact)
            {
                OnPressAction();
            }
            base.OnSolidImpact(with, from);
        }

    }
}
