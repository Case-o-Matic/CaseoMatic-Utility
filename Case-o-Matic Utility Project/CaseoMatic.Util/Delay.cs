using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    public class Delay
    {
        private bool isStarted;
        public bool IsStarted
        {
            get { return isStarted; }
        }

        private double time, currentTime;
        public double Time
        {
            get { return time; }
        }
        public double CurrentTime
        {
            get { return currentTime; }
        }

        private Action onReached;
        public Action OnReached
        {
            get { return onReached; }
        }

        public Delay(double time, Action onReached)
        {
            this.time = time;
            this.onReached = onReached;
        }

        public virtual void Start()
        {
            isStarted = true;
        }

        public void Pause()
        {
            isStarted = false;
        }

        public void SetCurrentTime(double newCurrentTime)
        {
            currentTime = newCurrentTime;
        }

        public void Reset()
        {
            SetCurrentTime(0);
        }

        public void Update(double deltaTime)
        {
            if (isStarted)
            {
                currentTime += deltaTime;
                if (currentTime >= time)
                    ReachTime();
            }
        }

        protected virtual void ReachTime()
        {
            onReached();
        }
    }
}
