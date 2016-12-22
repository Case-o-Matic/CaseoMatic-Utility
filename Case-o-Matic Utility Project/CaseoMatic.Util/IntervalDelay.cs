using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Caseomatic.Util
{
    public sealed class IntervalDelay : Delay
    {
        private int maxIntervals, currentIntervalCount;
        public int MaxIntervals
        {
            get { return maxIntervals; }
        }
        public int CurrentIntervalCounts
        {
            get { return currentIntervalCount; }
        }

        public IntervalDelay(double intervalTime, Action onReached, int maxIntervals = 0)
            : base(intervalTime, onReached)
        {
            this.maxIntervals = maxIntervals;
        }

        public override void Start()
        {
            currentIntervalCount = 0;
            base.Start();
        }

        protected override void ReachTime()
        {
            base.ReachTime();

            if (maxIntervals != 0)
            {
                currentIntervalCount++;
                if (currentIntervalCount >= maxIntervals)
                    Pause();
            }

            Reset();
        }
    }
}
