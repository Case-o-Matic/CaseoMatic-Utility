using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    [Serializable]
    public abstract class TriggerZone
    {
        public delegate void OnTriggerHandler(TriggerZone other); // Add more collision information?
        public event OnTriggerHandler OnEnterTrigger, OnExitTrigger;

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
        }

        public TriggerZone()
        {
        }
        
        public abstract void Update(double deltaTime);
    }
}
