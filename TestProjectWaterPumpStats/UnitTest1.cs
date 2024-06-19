using WaterPumpStats.Service;

namespace TestProjectWaterPumpStats
{
    public class UnitTest1
    {
        [Theory]
        [InlineData("2019-01-10T11:00:00", "2019-01-10T13:00:00", "01:21:00")]
        [InlineData("2019-01-10T10:00:00", "2019-01-10T15:00:00", "02:57:02")]
        [InlineData("2019-01-10T10:00:00", "2019-01-10T14:14:12", "02:26:00")]
        public void Test1(string startStr, string endStr, string expectedStr)
        {

            IMeasurementPumpService measurementPumpService = new MeasurementPumpService();
            DateTime start = DateTime.Parse(startStr);
            DateTime end = DateTime.Parse(endStr);
            TimeSpan expected = TimeSpan.Parse(expectedStr);

            TimeSpan result = measurementPumpService.GetRunningDurationOnPeriod(TestPumpData(), start, end);

            Assert.Equal(expected, result);
        }


        private Measure[] TestPumpData()
        {
            return new Measure[]
            {
                new Measure(new DateTime(2019, 1, 10, 10, 14, 11), 0),
                new Measure(new DateTime(2019, 1, 10, 10, 55, 0), 1),
                new Measure(new DateTime(2019, 1, 10, 12, 21, 0), 0),
                new Measure(new DateTime(2019, 1, 10, 13, 14, 12), 1),
                new Measure(new DateTime(2019, 1, 10, 14, 45, 14), 0),
            };
        }
    }
}