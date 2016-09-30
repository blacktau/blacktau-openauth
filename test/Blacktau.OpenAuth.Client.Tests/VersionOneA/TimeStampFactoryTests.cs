namespace Blacktau.OpenAuth.Tests.VersionOneA
{
    using System;

    using Blacktau.OpenAuth.Interfaces.VersionOneA;
    using Blacktau.OpenAuth.VersionOneA;

    using NSubstitute;

    using Xunit;

    public class TimeStampFactoryTests
    {
        public class TheConstuctor
        {
            [Fact]
            public void GivenAnIClock_Constructs()
            {
                var clock = Substitute.For<IClock>();
                
                var timeStampFactory = new TimeStampFactory(clock);

                Assert.NotNull(timeStampFactory);
            }

            [Fact]
            public void GivenNull_ThrowsException()
            {
                Assert.Throws<ArgumentNullException>(() => new TimeStampFactory(null));
            }

        }

        public class TheGetTimeStampMethod
        {
            [Fact]
            public void GivenZeroDriftReturnsUtcNow()
            {
                var clock = Substitute.For<IClock>();
                var now = DateTime.UtcNow;
                clock.UtcNow.Returns(now);

                var timeStampFactory = new TimeStampFactory(clock);
                timeStampFactory.DriftInSeconds = 0;

                var result = timeStampFactory.GetTimeStamp();

                Assert.Equal(now, result);
                var temp = clock.Received().UtcNow;
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(10)]
            [InlineData(66)]
            public void GivenVaryingDriftsReturnsUtcNowPlusDrift(int driftInSeconds)
            {
                var clock = Substitute.For<IClock>();
                var now = DateTime.UtcNow;
                clock.UtcNow.Returns(now);

                var timeStampFactory = new TimeStampFactory(clock);
                timeStampFactory.DriftInSeconds = driftInSeconds;

                var result = timeStampFactory.GetTimeStamp();

                var expected = now.AddSeconds(driftInSeconds);

                Assert.Equal(expected, result);
                var temp = clock.Received().UtcNow;
            }
        }

        public class TheGetTimeSpanFromEpochMethod
        {
            [Fact]
            public void GivenZeroDriftReturnsEpochToUtcNow()
            {
                var now = DateTime.UtcNow;
                var expected = now.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));

                var clock = Substitute.For<IClock>();
                clock.UtcNow.Returns(now);
                

                var timeStampFactory = new TimeStampFactory(clock);
                timeStampFactory.DriftInSeconds = 0;

                var result = timeStampFactory.GetTimeSpanFromEpoch();

                Assert.Equal(expected, result);
                var temp = clock.Received().UtcNow;
            }

            [Theory]
            [InlineData(1)]
            [InlineData(2)]
            [InlineData(10)]
            [InlineData(66)]
            public void GivenVaryingDriftsReturnsUtcNowPlusDrift(int driftInSeconds)
            {
                var now = DateTime.UtcNow;
                var expected = now.AddSeconds(driftInSeconds).Subtract(new DateTime(1970, 1, 1, 0, 0, 0));

                var clock = Substitute.For<IClock>();
                clock.UtcNow.Returns(now);

                var timeStampFactory = new TimeStampFactory(clock);
                timeStampFactory.DriftInSeconds = driftInSeconds;

                var result = timeStampFactory.GetTimeSpanFromEpoch();

                Assert.Equal(expected, result);
                var temp = clock.Received().UtcNow;
            }
        }
    }
}