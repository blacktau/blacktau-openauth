namespace Blacktau.OpenAuth.VersionOneA
{
    using System;

    using Blacktau.OpenAuth.Interfaces.VersionOneA;

    public class TimeStampFactory : ITimeStampFactory
    {
        private readonly IClock clock;

        public TimeStampFactory(IClock clock)
        {
            if (clock == null)
            {
                throw new ArgumentNullException(nameof(clock));
            }

            this.clock = clock;
        }

        public int DriftInSeconds { get; set; }

        public DateTime GetTimeStamp()
        {
            return this.clock.UtcNow.AddSeconds(this.DriftInSeconds);
        }

        public TimeSpan GetTimeSpanFromEpoch()
        {
            return this.GetTimeStamp().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
        }
    }
}