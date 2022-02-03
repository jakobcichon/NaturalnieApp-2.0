using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.DebugUtils
{
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
}
