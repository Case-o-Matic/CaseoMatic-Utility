using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    public class CylinderTriggerZone : TriggerZone
    {
        public Vector3 center;
        public double radius, height;

        public CylinderTriggerZone()
        {
        }
        public CylinderTriggerZone(Vector3 center, double radius, double height)
        {
            this.center = center;
            this.radius = radius;
            this.height = height;
        }

        public override void Update(double deltaTime)
        {
        }
    }
}
