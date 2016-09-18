namespace Blacktau.OpenAuth.Interfaces.VersionOneA
{
    using System;

    public interface ITimeStampFactory
    {
        int DriftInSeconds { get; set; }

        DateTime GetTimeStamp();

        TimeSpan GetTimeSpanFromEpoch();
    }
}