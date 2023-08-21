using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Config.Stores.Attributes;
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
        private readonly PrioritySetterConfig Config;
        private readonly Timer RefreshTimer;

        [Init]
        public Plugin(IPALogger logger, Config config)
        {
            Config = config.Generated<PrioritySetterConfig>();
            Logger = logger;

            SetPriority(null);

            // Start with 10 seconds left, then 60 seconds (or one minutes) between checks
            RefreshTimer = new Timer(SetPriority, null, 10000, 60000);

            // Workaround for windows setting normal priority on window changing focus
            Application.focusChanged += (isFocused) => { if (isFocused) SetPriority(null); };
        }

        [OnExit] 
        public void OnExit()
        {
            RefreshTimer.Dispose();
        }

        private void SetPriority(object state)
        {
            var thisProcess = Process.GetCurrentProcess();

            if (thisProcess.PriorityClass == Config.ProcessPriority) return;

            Logger.Warn($"Priority no longer at desired level. Refreshing!");

            thisProcess.PriorityClass = Config.ProcessPriority;

            Logger.Info($"Set priority to {thisProcess.PriorityClass}");
        }

        public class PrioritySetterConfig
        {
            [UseConverter(typeof(EnumConverter))]
            public virtual ProcessPriorityClass ProcessPriority { get; set; } = ProcessPriorityClass.High;
        }
    }
}
