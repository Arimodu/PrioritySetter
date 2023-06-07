using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System.Diagnostics;
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

        [Init]
        public Plugin(IPALogger logger, Config config)
        {
            Config = config.Generated<PrioritySetterConfig>();
            ThisProcess = Process.GetCurrentProcess();
            Logger = logger;

            SetPriority();

            Config.OnChanged += Config_OnChanged;
        }

        private void Config_OnChanged()
        {
            Logger.Info("Config changed! Updating process priority.");
            SetPriority();
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
            Logger.Info($"Set priority to {ppc}");
        }
    }
}
