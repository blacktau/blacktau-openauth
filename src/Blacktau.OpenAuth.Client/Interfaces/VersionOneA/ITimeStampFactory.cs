namespace Blacktau.OpenAuth.Client.Interfaces.VersionOneA
{
    using System;

    public interface ITimeStampFactory
    {
        int DriftInSeconds { get; set; }

        DateTime GetTimeStamp();

        TimeSpan GetTimeSpanFromEpoch();
    }
}