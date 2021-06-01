using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DuckGame;

// This class definition is not written by me.
// Original code sourced from Thomas' Grenade Pack: https://steamcommunity.com/sharedfiles/filedetails/?id=491555785&searchtext=grenade
// From what I can tell, this class is meant to gain access to certain "Grenade" methods that usually cannot be overriden using the keyword "override".

public abstract class GrenadeBase : Gun
{
	protected SpriteMap sprite;

	private bool hasPin;

	public StateBinding TimerBinding { get; set; }

	public StateBinding HasPinBinding { get; set; }

	public bool HasPin
	{
		get
		{
			return hasPin;
		}
		set
		{
			if (HasPin && !value)
			{
				CreatePinParticle();
			}
			hasPin = value;
		}
	}

	public float Timer { get; set; }

	public bool HasExploded { get; protected set; }

	public bool pullOnImpact { get; set; }

	public GrenadeBase(float xval, float yval)
		: base(xval, yval)
	{
		TimerBinding = new StateBinding("Timer", -1, rot: false);
		HasPinBinding = new StateBinding("HasPin", -1, rot: false);
		HasPin = true;
		Timer = 1.2f;
		_editorName = "Grenade base";
		_bio = "You should not see this item ingame.";
		_type = "gun";
		base.bouncy = 0.4f; // i fuck around with this value (high value makes it not bounce.
		friction = 0.05f;
		ammo = 1;
	}

	public override void Initialize()
	{
		base.Initialize();
	}

	public virtual void QuickFlash()
	{
		Graphics.flashAdd = 1.3f;
		Layer.Game.darken = 1.3f;
	}

	public virtual void Explode()
	{
		HasExploded = true;
	}

	public virtual void DestroyWindowsInRadius(float radius)
	{
		foreach (Window item in Level.CheckCircleAll<Window>(position, radius))
		{
			if (Level.CheckLine<Block>(position, item.position, item) == null)
			{
				item.Destroy(new DTImpact(this));
			}
		}
	}

	protected virtual void CreatePinParticle()
	{
		GrenadePin grenadePin = new GrenadePin(base.x, base.y);
		grenadePin.hSpeed = (float)(-offDir) * Rando.Float(1.5f, 2f);
		grenadePin.vSpeed = -2f;
		Level.Add(grenadePin);
		SFX.Play("pullPin");
	}

	protected virtual void UpdateTimer()
	{
		if (!HasPin)
		{
			if (Timer > 0f)
			{
				Timer -= 0.01f;
			}
			else if (!HasExploded)
			{
				Explode();
			}
		}
	}

	protected virtual void UpdateFrame()
	{
		sprite.frame = ((!HasPin) ? 1 : 0);
	}

	public override void Update()
	{
		base.Update();
		UpdateTimer();
		UpdateFrame();
	}

	public override void OnSolidImpact(MaterialThing with, ImpactedFrom from)
	{
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
			HasPin = false;
		}
	}
}
