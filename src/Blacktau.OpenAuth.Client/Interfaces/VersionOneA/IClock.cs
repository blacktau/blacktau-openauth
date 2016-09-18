namespace Blacktau.OpenAuth.Interfaces.VersionOneA
{
    using System;

    public interface IClock
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
    }
}