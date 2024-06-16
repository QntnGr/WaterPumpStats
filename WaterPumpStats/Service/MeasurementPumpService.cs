namespace WaterPumpStats.Service;

public class MeasurementPumpService : IMeasurementPumpService
{
    public TimeSpan GetRunningDurationOnPeriod(Measure[] onOffMeasures, DateTime start, DateTime end)
    {
        TimeSpan result = TimeSpan.Zero;
        DateTime? startPerdiod = null;
        DateTime? endPerdiod = null;
        for (int i = 0; i < onOffMeasures.Count(); i++)
        {
            if (onOffMeasures[i].Time > start)
            {
                if (onOffMeasures[i].IsRunning)
                {
                    startPerdiod = onOffMeasures[i].Time;
                }
                else if (i > 0 && onOffMeasures[i - 1].IsRunning)
                {
                    startPerdiod = start;
                }
            }
            if (startPerdiod.HasValue
                && startPerdiod != onOffMeasures[i].Time
                && onOffMeasures[i].Time < end
                && !onOffMeasures[i].IsRunning)
            {
                endPerdiod = onOffMeasures[i].Time;
            }
            if (startPerdiod.HasValue
                && endPerdiod.HasValue)
            {
                result += endPerdiod.Value - startPerdiod.Value;
                startPerdiod = null;
                endPerdiod = null;
            }
        }
        return result;
    }
}
