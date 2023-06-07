using IPA.Config.Stores.Attributes;
using System;

namespace PrioritySetter
{
    public enum Priority
    {
        Idle,
        BelowNormal,
        Normal,
        AboveNormal,
        High,
        RealTime
    }

    public class PrioritySetterConfig
    {
        [UseConverter(typeof(EnumConverter))]
        public virtual Priority ProcessPriority { get; set; } = Priority.High;

        internal event Action OnChanged;

        public virtual void Changed()
        {
            OnChanged?.Invoke();
        }
    }
}