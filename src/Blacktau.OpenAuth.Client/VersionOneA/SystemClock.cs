namespace Blacktau.OpenAuth.Client.VersionOneA
{
    using System;

    using Blacktau.OpenAuth.Client.Interfaces.VersionOneA;

    public class SystemClock : IClock
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;
    }
}