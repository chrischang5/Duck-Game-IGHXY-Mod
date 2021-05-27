// DuckGame.ATShrapnel
using DuckGame;

public class ATFlash : AmmoType
{
	public ATFlash()
	{
		accuracy = 0.75f;
		range = 100000000000f;
		penetration = 0.4f;
		bulletSpeed = 18f;
		combustable = true;
	}
}