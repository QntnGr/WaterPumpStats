
namespace WaterPumpStats.Service;

public class Measure
{
    public DateTime Time { get; set; }
    public bool IsRunning { get; set; }

    public Measure(DateTime time, int status) 
    {
        Time = time;
        IsRunning = status == 1;
    }
}
