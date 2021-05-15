using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

namespace MyMod.src
{
	[EditorGroup("MyMod|Explosives")]
	public class Flashbang : Grenade
    {
		public Flashbang(float xval, float yval)
		: base(xval, yval)
		{
			this.ammo = 1;
			this._ammoType = new ATShrapnel(); // Maybe new explosion type? No idea bro
			this._ammoType.penetration = 0.4f;
			this._type = "grenade";
			this._sprite = new SpriteMap(GetPath("flashbang"), 16, 16); // Create flashbang asset
			base.graphic = _sprite;
			this.center = new Vec2(7f, 8f);
			this.collisionOffset = new Vec2(-4f, -5f);
			this.collisionSize = new Vec2(8f, 10f);
			base.bouncy = 0.4f; // Controls bounce. Cool!
			friction = 0.05f;
			_fireRumble = RumbleIntensity.Kick;
			_editorName = "Grenade";
			editorTooltip = "#1 Pull pin. #2 Throw grenade. Order of operations is important here.";
			_bio = "To cook grenade, pull pin and hold until feelings of terror run down your spine. Serves as many ducks as you can fit into a 3 meter radius.";
		}

		// Override Explosion
		public override void CreateExplosion(Vec2 pos)
		{
			if (!_explosionCreated)
			{
				float cx = pos.x;
				float cy = pos.y - 2f;
				Level.Add(new ExplosionPart(cx, cy));
				int num = 6;
				if (Graphics.effectsLevel < 2)
				{
					num = 3;
				}
				for (int i = 0; i < num; i++)
				{
					float dir = (float)i * 60f + Rando.Float(-10f, 10f);
					float dist = Rando.Float(12f, 20f);
					Level.Add(new ExplosionPart(cx + (float)(Math.Cos(Maths.DegToRad(dir)) * (double)dist), cy - (float)(Math.Sin(Maths.DegToRad(dir)) * (double)dist)));
				}
				_explosionCreated = true;
				SFX.Play("explode");
				RumbleManager.AddRumbleEvent(pos, new RumbleEvent(RumbleIntensity.Heavy, RumbleDuration.Short, RumbleFalloff.Medium));
			}
		}
	}
}
