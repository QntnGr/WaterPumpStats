namespace WaterPumpStats.Service;

public class MeasurementPumpService : IMeasurementPumpService
{
    public TimeSpan GetRunningDurationOnPeriod(Measure[] onOffMeasures, DateTime start, DateTime end)
    {
        TimeSpan result = TimeSpan.Zero;
        DateTime? startPerdiod = null;
        DateTime? endPerdiod = null;
        bool isStart = true;

        foreach (var measure in onOffMeasures.ToDictionary(m => m.Time))
        {
            if (measure.Key > start
                && !startPerdiod.HasValue)
            {
                if (measure.Value.IsRunning)
                {
                    startPerdiod = measure.Key;
                }
                else if(result ==  TimeSpan.Zero
                    && !isStart)
                {
                    startPerdiod = start;
                    endPerdiod = measure.Key;
                }
                isStart = false;
                continue;
            }
            isStart = false;
            if (startPerdiod.HasValue
                && !endPerdiod.HasValue
                && measure.Key < end)
            {
                if(!measure.Value.IsRunning)
                {
                    endPerdiod = measure.Key;
                }
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
