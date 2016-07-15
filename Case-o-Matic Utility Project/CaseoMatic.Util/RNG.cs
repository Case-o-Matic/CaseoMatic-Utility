using System;

namespace Caseomatic.Util
{
    /// <summary>
    /// A random rumber generator that determines if a condition is filled.
    /// The more times the RNG is executed without receiving a positive result, the chance that a positive
    /// result occurs gets bigger.
    /// </summary>
    public sealed class RNG
    {
        private static readonly Random random = new Random(DateTime.Now.Millisecond);
        private static object randomLockObj = new object();

        private float currentBalancePercentage,
            balancePercentageStrength;

        /// <summary>
        /// Initializes a new RNG instance using a balance percentage strength value of 1.
        /// </summary>
        public RNG() : this(1)
        {
        }
        /// <summary>
        /// Initializes a new RNG instance using a custom balance percentage strength.
        /// </summary>
        /// <param name="balancePercentageStrength">
        /// The value that is added up to the generated number that determines if a condition is fulfilled.
        /// If the generated value is to big, the current balance value is substracted to give it a second try.
        /// If it still does not work, the balance percentage strength number is added up to the current balance for the next determination.
        /// If the generated value is less or equal to the given determination percentage, the current balance is reset.
        /// </param>
        public RNG(float balancePercentageStrength)
        {
            this.balancePercentageStrength = balancePercentageStrength;
        }

        /// <summary>
        /// Determines a condition.
        /// </summary>
        /// <param name="percentage">The minimum percentage that has to be reached.</param>
        /// <param name="maxPercentage">The maximum percentage that is used for clamping (default is 100%).</param>
        /// <returns>Returns if the condition has been fulfilled.</returns>
        public bool Determine(float percentage, float maxPercentage = 100)
        {
            // Generate a random double
            double determinedValue = 0;
            lock (randomLockObj)
                determinedValue = random.NextDouble() * maxPercentage;

            // The determined value is too big
            if (determinedValue < maxPercentage)
            {
                // Add up the current balance percentage to give it a second shot
                determinedValue -= currentBalancePercentage;
            }
            if (determinedValue >= 0)
            {
                // The determined value is successfully less or equal to the wanted percentage, reset the current balance
                ResetCurrentBalance();

                return true;
            }

            // Still didnt work? Add up the balance percentage strength 
            currentBalancePercentage += balancePercentageStrength;

            return false;
        }

        public void ResetCurrentBalance()
        {
            currentBalancePercentage = 0;
        }

        // TODO: Implement static methods to generate fast condition determinations on the fly?
    }
}
