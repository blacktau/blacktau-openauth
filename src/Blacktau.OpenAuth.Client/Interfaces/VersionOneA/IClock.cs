namespace Blacktau.OpenAuth.Client.Interfaces.VersionOneA
{
    using System;

    public interface IClock
    {
        DateTime Now { get; }

        DateTime UtcNow { get; }
    }
}