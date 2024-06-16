
namespace WaterPumpStats.Service;

public interface IMeasurementPumpService
{
    public TimeSpan GetRunningDurationOnPeriod(Measure[] onOffMeasures, DateTime start, DateTime end);
}
