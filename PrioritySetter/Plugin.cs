using IPA;
using IPA.Config;
using IPA.Config.Stores;
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
        private readonly Process ThisProcess;
        private readonly IPALogger Logger;
        private readonly PrioritySetterConfig Config;
        private readonly Timer RefreshTimer;

        [Init]
        public Plugin(IPALogger logger, Config config)
        {
            Config = config.Generated<PrioritySetterConfig>();
            ThisProcess = Process.GetCurrentProcess();
            Logger = logger;

            SetPriority();

            Config.OnChanged += Config_OnChanged;

            // Start with 10 seconds left, then 120 seconds (or two minutes) between checks
            RefreshTimer = new Timer(RefreshPriority, null, 10000, 120000);
        }

        [OnExit] 
        public void OnExit()
        {
            RefreshTimer.Dispose();
        }

        private void Config_OnChanged()
        {
            Logger.Info("Config changed! Updating process priority.");
            SetPriority();
        }

        private void RefreshPriority(object state)
        {
            ProcessPriorityClass ppc;

            switch (Config.ProcessPriority)
            {
                case Priority.Idle:
                    ppc = ProcessPriorityClass.Idle;
                    break;
                case Priority.BelowNormal:
                    ppc = ProcessPriorityClass.BelowNormal;
                    break;
                case Priority.Normal:
                    ppc = ProcessPriorityClass.Normal;
                    break;
                case Priority.AboveNormal:
                    ppc = ProcessPriorityClass.AboveNormal;
                    break;
                case Priority.High:
                    ppc = ProcessPriorityClass.High;
                    break;
                case Priority.RealTime:
                    ppc = ProcessPriorityClass.RealTime;
                    break;
                default:
                    ppc = ProcessPriorityClass.Normal;
                    break;
            }

            // Debug log here
            //Logger.Notice($"Cheking priority. Set: {ThisProcess.PriorityClass}, Desired: {Config.ProcessPriority}, Desired converted: {ppc}");

            if (ThisProcess.PriorityClass == ppc) return;

            Logger.Info($"Priority no longer at desired level. Refreshing!");

            Logger.Info($"Set priority to {ThisProcess.PriorityClass}");
        }

        internal void SetPriority()
        {
            ProcessPriorityClass ppc;

            switch (Config.ProcessPriority)
            {
                case Priority.Idle:
                    ppc = ProcessPriorityClass.Idle;
                    break;
                case Priority.BelowNormal:
                    ppc = ProcessPriorityClass.BelowNormal;
                    break;
                case Priority.Normal:
                    ppc = ProcessPriorityClass.Normal;
                    break;
                case Priority.AboveNormal:
                    ppc = ProcessPriorityClass.AboveNormal;
                    break;
                case Priority.High:
                    ppc = ProcessPriorityClass.High;
                    break;
                case Priority.RealTime:
                    ppc = ProcessPriorityClass.RealTime;
                    break;
                default:
                    ppc = ProcessPriorityClass.Normal;
                    break;
            }

            ThisProcess.PriorityClass = ppc;
            Logger.Info($"Set priority to {ThisProcess.PriorityClass}");
        }
    }
}
