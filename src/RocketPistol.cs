using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DuckGame.MyMod
{
    [EditorGroup("MyMod|guns")]
    public class MyGun : Gun
    {
        public MyGun(float xval, float yval) : base(xval, yval)
        {
            this.ammo = 10; // ammo count
            this._ammoType = new ATMissile();
            this._ammoType.range = 200f;
            this._ammoType.accuracy = 1f; // high acc = sniper, low acc = smg
            this._ammoType.penetration = 1f; // Larger penetration = can go through more things
            this._type = "gun";
            base.graphic = new Sprite(GetPath("crateGun")); // See https://duckgamemodding.wordpress.com/2017/06/09/making-your-first-mod/ 
            this.center = new Vec2(8f, 4.5f); // center of your item 
            this.collisionOffset = new Vec2(-8f, -2f); // keep this as is...
            this.collisionSize = new Vec2(16f, 9f); // NO WHITESPACE
            this._barrelOffsetTL = new Vec2(16f, 1f); // Where bullet shoots from
            this._holdOffset = new Vec2(1f, 1.5f); // just mess around until it looks good in game
            this._fireSound = "missile"; // sound effect! I will have fun with this
            this._kickForce = 3f; // recoil (controls how much you can jump) OH MY GOODNESS IM GONNA MAKE A GUN JUMPER FOR SURE
        }
    }
}
