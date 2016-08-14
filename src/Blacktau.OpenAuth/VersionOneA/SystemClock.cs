namespace Blacktau.OpenAuth.VersionOneA
{
    using System;
    using Interfaces.VersionOneA;
    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}