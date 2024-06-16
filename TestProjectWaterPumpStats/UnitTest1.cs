using WaterPumpStats.Service;

namespace TestProjectWaterPumpStats
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            IMeasurementPumpService measurementPumpService = new MeasurementPumpService();
            DateTime start = new DateTime(2019, 1, 10, 11, 0, 0);
            DateTime end = new DateTime(2019, 1, 10, 13, 0, 0);
            TimeSpan expected = new TimeSpan(1, 21, 00);
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