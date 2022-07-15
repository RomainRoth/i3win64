using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace i3win64
{
    internal class WindowRouter
    {
        // Singleton
        private static readonly Lazy<WindowRouter> instance = new Lazy<WindowRouter>(() => new WindowRouter());
        public static WindowRouter Instance => instance.Value;

        // Management Events Watchers
        ManagementEventWatcher processStartWatch;
        ManagementEventWatcher processStopWatch;
        
        private WindowRouter()
        {
            var processes = Process.GetProcesses();
            /*processStartWatch = new ManagementEventWatcher(
                    new WqlEventQuery("SELECT * FROM Win32_ProcessStartTrace")
                );
            processStartWatch.EventArrived += new EventArrivedEventHandler(ProcessStartWatchCallback);
            processStartWatch.Start();

            processStopWatch = new ManagementEventWatcher(
                    new WqlEventQuery("SELECT * FROM Win32_ProcessStopTrace")
                );
            processStopWatch.EventArrived += new EventArrivedEventHandler(ProcessStopWatchCallback);
            processStopWatch.Start();*/
        }

        public string Hello()
        {
            return "Hello";
        }

        // On process creation
        private void ProcessStartWatchCallback(object sender, EventArrivedEventArgs e)
        {
            
        }

        // On process termination
        private void ProcessStopWatchCallback(object sender, EventArrivedEventArgs e)
        {

        }
    }
}
