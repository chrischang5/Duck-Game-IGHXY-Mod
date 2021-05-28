
using System.Collections.Generic;
using DuckGame;

public class Flash : Thing
{
	private float radius;

	public bool IsLocalDuckAffected { get; set; }

	public float Timer { get; set; }

	public Flash(float xval, float yval, float stayTime = 1.0f, float radius = 250f)
		: base(xval, yval)
	{
		Timer = stayTime;
		this.radius = radius;
		SetIsLocalDuckAffected();
		if (IsLocalDuckAffected)
		{
			SFX.Play(GetPath("sounds/flashbang_csgo.wav"));
		}
	}

	public virtual void SetIsLocalDuckAffected()
	{
		List<Duck> list = new List<Duck>();
		foreach (Duck item in Level.CheckCircleAll<Duck>(position, radius))
		{
			if (!list.Contains(item))
			{
				list.Add(item);
			}
		}
		foreach (Ragdoll item2 in Level.CheckCircleAll<Ragdoll>(position, radius))
		{
			if (!list.Contains(item2._duck))
			{
				list.Add(item2._duck);
			}
		}
		foreach (Duck item3 in list)
		{
			if (item3.profile.localPlayer && (Level.CheckLine<Block>(position, item3.position, item3) == null))
			{
				IsLocalDuckAffected = true;
				return;
			}
		}
		IsLocalDuckAffected = false;
	}

	public override void Update()
	{
		base.Update();
		if (Timer > 0f)
		{
			Timer -= 0.01f;
		}
		else
		{
			Level.Remove(this);
		}
	}

	public override void Draw()
	{
		base.Draw();
		if (IsLocalDuckAffected)
		{
			Graphics.flashAdd = 1.3f;
		}
	}
}
