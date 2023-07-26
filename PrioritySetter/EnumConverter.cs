using IPA.Config.Data;
using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySetter
{
    public class EnumConverter : ValueConverter<ProcessPriorityClass>
    {
        public override ProcessPriorityClass FromValue(Value value, object parent)
        {
            switch (value.ToString().Trim('"'))
            {
                case "Idle":
                    return ProcessPriorityClass.Idle;
                case "BelowNormal":
                    return ProcessPriorityClass.BelowNormal;
                case "Normal":
                    return ProcessPriorityClass.Normal;
                case "AboveNormal":
                    return ProcessPriorityClass.AboveNormal;
                case "High":
                    return ProcessPriorityClass.High;
                case "RealTime":
                    return ProcessPriorityClass.RealTime;
                default:
                    throw new InvalidOperationException($"Invalid config: {value.GetType()} '{value}'");
            }
        }

        public override Value ToValue(ProcessPriorityClass obj, object parent)
        {
            return Value.Text(obj.ToString());
        }
    }
}
