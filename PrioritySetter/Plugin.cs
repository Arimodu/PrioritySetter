using IPA;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace PrioritySetter
{
    [NoEnableDisable]
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        private readonly IPALogger Logger;
        private readonly Timer RefreshTimer;

        private readonly ProcessPriorityClass Priority = ProcessPriorityClass.High;

        [Init]
        public Plugin(IPALogger logger)
        {
            Logger = logger;

            // Start with 10 seconds left, then 60 seconds (or one minute) between checks
            RefreshTimer = new Timer(SetPriority, null, 10000, 60000);
        }

        [OnExit] 
        public void OnExit() => RefreshTimer.Dispose();

        private void SetPriority(object state)
        {
            // Process obejct does not update, have to refresh manually
            var thisProcess = Process.GetCurrentProcess();

#if DEBUG
            Logger.Notice($"Cheking priority. Set: {thisProcess.PriorityClass}, Desired: {Priority}");
#endif

            if (thisProcess.PriorityClass == Priority) return;

            Logger.Warn($"Priority no longer at desired level. Refreshing...");

            thisProcess.PriorityClass = Priority;

            Logger.Info($"Set priority to {thisProcess.PriorityClass}");
        }
    }
}
