using System.Collections.Generic;

namespace WaterPumpStats.Service;

public class MeasurementPumpService : IMeasurementPumpService
{
    public TimeSpan GetRunningDurationOnPeriod(Measure[] onOffMeasures, DateTime start, DateTime end)
    {
        TimeSpan result = TimeSpan.Zero;
        DateTime? startPerdiod = null;
        DateTime? endPerdiod = null;

        SortedDictionary<DateTime, Measure> sortedMeasures = new SortedDictionary<DateTime, Measure>(
            onOffMeasures.ToDictionary(m => m.Time)
                 .Where(m => m.Key >= start && m.Key <= end)
                 .ToDictionary(m => m.Key, m => m.Value)
        );
        int i = 0;
        int maxItem = sortedMeasures.Count;

        foreach (var measure in sortedMeasures)
        {
            i++;
            if (!startPerdiod.HasValue)
            {
                if (measure.Value.IsRunning)
                {
                    startPerdiod = measure.Key;
                    if (i == maxItem)
                    {
                        endPerdiod = end;
                    }
                    else
                    {
                        continue;
                    }
                }
                else if (i == maxItem)
                {
                    startPerdiod = start;
                    endPerdiod = measure.Key;
                }
            }

            if (startPerdiod.HasValue
            && !endPerdiod.HasValue
            && !measure.Value.IsRunning)
            {
                endPerdiod = measure.Key;
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
