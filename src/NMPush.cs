using System;

namespace DuckGame
{
    public class NMPush : NMEvent
    {
        public NMPush(Duck pTarget, Vec2 changeinspeed)
        {
            this.target = pTarget;
            this.changeinspeed = changeinspeed;
        }

        public NMPush()
        {
        }

        public override void Activate()
        {
            if (Level.current != null && this.target != null && this.target.isServerForObject && this.target.profile != null)
            {
                this.target.hSpeed += this.changeinspeed.x;
                this.target.vSpeed += this.changeinspeed.y;

            }
        }
        public Duck target;
        public Vec2 changeinspeed;
    }
}