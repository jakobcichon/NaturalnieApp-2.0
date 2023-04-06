using System.Diagnostics;

namespace NaturalnieApp2_Shared.DebugUtils;

public class DebugTimer
{
    private Stopwatch Timer { get; set; }
    private string Name { get; set; }

    public DebugTimer(string name)
    {
        Timer = new Stopwatch();
        Name = name;
    }

    public void StartTimer()
    {
        Timer.Reset();
        Timer.Start();
    }

    public void StopTimer()
    {
        Timer.Stop();
        TimeSpan e = Timer.Elapsed;
        Debug.WriteLine($"[{Name}] Action last: {e.Hours}:{e.Minutes}:{e.Seconds}.{e.Milliseconds}");
    }
}
