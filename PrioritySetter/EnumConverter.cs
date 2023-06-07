using IPA.Config.Data;
using IPA.Config.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrioritySetter
{
    public class EnumConverter : ValueConverter<Priority>
    {
        public override Priority FromValue(Value value, object parent)
        {
            switch (value.ToString().Trim('"'))
            {
                case "Idle":
                    return Priority.Idle;
                case "BelowNormal":
                    return Priority.BelowNormal;
                case "Normal":
                    return Priority.Normal;
                case "AboveNormal":
                    return Priority.AboveNormal;
                case "High":
                    return Priority.High;
                case "RealTime":
                    return Priority.RealTime;
                default:
                    throw new InvalidOperationException($"Invalid config: {value.GetType()} '{value}'");
            }
        }

        public override Value ToValue(Priority obj, object parent)
        {
            return Value.Text(obj.ToString());
        }
    }
}
