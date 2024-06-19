using System.Collections;
using System.Collections.Generic;

namespace WaterPumpStats.Service;

public class MeasurementPumpService : IMeasurementPumpService
{
    public TimeSpan GetRunningDurationOnPeriod(Measure[] onOffMeasures, DateTime start, DateTime end)
    {
        TimeSpan result = TimeSpan.Zero;
        DateTime? startPerdiod = null;
        DateTime? endPerdiod = null;

        SortedList<DateTime, Measure> sortedMeasures = new SortedList<DateTime, Measure>(
            onOffMeasures.ToDictionary(m => m.Time)
                 .Where(m => m.Key >= start && m.Key <= end)
                 .ToDictionary(m => m.Key, m => m.Value)
        );

        //var getRunningValue = sortedMeasures.GetValueAtIndex(sortedMeasures.IndexOfKey(sortedMeasures.First().Key) - 1).IsRunning;

        int i = 0;
        int lastItem = sortedMeasures.Count;


        foreach (var measure in sortedMeasures)
        {
            i++;
            if (!startPerdiod.HasValue)
            {
                if (i == lastItem)
                {
                    if (measure.Value.IsRunning)
                    {
                        startPerdiod = measure.Key;
                        endPerdiod = end;
                    }
                    else
                    {
                        startPerdiod = start;
                        endPerdiod = measure.Key;
                    }
                }
                else
                {
                    if (measure.Value.IsRunning)
                    {
                        startPerdiod = measure.Key;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (!endPerdiod.HasValue
            && !measure.Value.IsRunning)
            {
                endPerdiod = measure.Key;
            }

            if (endPerdiod.HasValue)
            {
                result += endPerdiod.Value - startPerdiod.Value;
                startPerdiod = null;
                endPerdiod = null;
            }
        }
        return result;
    }
}
