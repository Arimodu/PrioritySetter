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

        internal readonly ProcessPriorityClass Priority = ProcessPriorityClass.High;

#if DEBUG
        private readonly DebugChecker DChecker;
#endif

        [Init]
        public Plugin(IPALogger logger)
        {
            Logger = logger;

            // Start with 10 seconds left, then 60 seconds (or one minute) between checks
            RefreshTimer = new Timer(SetPriority, null, 10000, 60000);

            // Workaround for windows setting normal priority on window changing focus
            Application.focusChanged += (isFocused) => { if (isFocused) SetPriority(null); };

#if DEBUG
            DChecker = new DebugChecker(1000, logger, this);
#endif
        }

#if DEBUG
        [OnExit]
        public void OnExit()
        {
            RefreshTimer.Dispose();
            DChecker.Dispose();
        }
#else
        [OnExit] 
        public void OnExit() => RefreshTimer.Dispose();
#endif

        internal void SetPriority(object _)
        {
            // Process object does not update, have to refresh manually
            var thisProcess = Process.GetCurrentProcess();

            if (thisProcess.PriorityClass == Priority) return;

            Logger.Warn($"Priority no longer at desired level. Refreshing...");

            thisProcess.PriorityClass = Priority;

            Logger.Info($"Set priority to {thisProcess.PriorityClass}");
        }
    }
}
