using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    public class CuboidTriggerZone : TriggerZone
    {
        public Vector3 center, scale;
        public float rotationY;

        public CuboidTriggerZone()
        {
        }
        public CuboidTriggerZone(Vector3 center, Vector3 scale, float rotationY)
        {
            this.center = center;
            this.scale = scale;
            this.rotationY = rotationY;
        }

        public override void Update(double deltaTime)
        {
        }
    }
}
