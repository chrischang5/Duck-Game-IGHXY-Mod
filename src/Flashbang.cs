using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

// Credit to https://steamcommunity.com/sharedfiles/filedetails/?id=775942759 for helping me createtgrenade

namespace DuckGame.IGHXY
{
    [EditorGroup("MyMod|Explosives")]
    [BaggedProperty("isFatal", false)]
    public class Flashbang : GrenadeBase
    {
        public Flashbang(float xval, float yval) : base(xval, yval)
        {
            this.sprite = new SpriteMap(GetPath("flashbang"), 7, 7); // Create flashbang asset
            base.graphic = sprite;

            _editorName = "Flashbang";

            collisionOffset = new Vec2(-3.5f, -3.5f);
            collisionSize = new Vec2(7f, 7f);
            Timer = 10000f; // basically an huge-ass timer for the grenade

            center = new Vec2(3.5f, 3.5f);

            //bouncy = 100000f; // Controls bounce. Cool!
            //friction = 100000f; // no friction?

            editorTooltip = "#1 Pull pin. #2 Throw grenade. Order of operations is important here.";
            _bio = "To cook grenade, pull pin and hold until feelings of terror run down your spine. Serves as many ducks as you can fit into a 3 meter radius.";
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

                if (with as Duck != null) // detect if you hit a duck
                {
                    Timer = 0; // Explode instantly if hit on duck
                }
            }
            base.OnSoftImpact(with, from);
        }

        public override void Explode()
        {
            QuickFlash();
            Flash();
            SFX.Play(GetPath("sounds/flashGrenadeExplode.wav"));
            Level.Remove(this);
            base.Explode();
        }

        public virtual void Flash()
        {
            Level.Add(new Flash(position.x, position.y));
        }

        public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
        {
            if (!HasPin) // look into this.owner and Disarm. See Duck class for more details
            {
                SFX.Play(GetPath("sounds/flashbang_csgo.wav"));
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


