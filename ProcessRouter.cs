using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace i3win64
{
    internal class ProcessRouter : IDisposable
    {
        private Dictionary<int, Process> processes = new Dictionary<int, Process>();

        public EventHandler<WindowAttachedEventArgs>? WindowAttached;

        public virtual void OnWindowAttached(Object sender, WindowAttachedEventArgs e)
        {
            EventHandler<WindowAttachedEventArgs>? handler = WindowAttached;
            handler?.Invoke(sender, e);
        }
        
        public ProcessRouter()
        {
        }
        
        public void Run()
        {
            var _processes = Process.GetProcesses().ToList();
            foreach(Process p in _processes)
            {
                try
                {
                    if (!processes.ContainsKey(p.Id))
                    {
                        processes.Add(p.Id, p);
                        if(p.ProcessName=="Notepad3")
                        {
                            OnWindowAttached(this, new WindowAttachedEventArgs(p.MainWindowHandle));
                        }
                    }
                }catch(Exception)
                {
                    
                }
            }

            foreach(int id in processes.Keys)
            {
                if(!_processes.Exists(e => e.Id == id ))
                {
                    processes.Remove(id);
                }
            }
        }
        
        public void Dispose()
        {
        }
    }
}
