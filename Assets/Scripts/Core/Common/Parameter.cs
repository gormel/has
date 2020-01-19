using Assets.Scripts.Core.Common.ParameterStatuses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Common
{
    public class Parameter
    {
        public float BaseValue { get; }

        public float Value
        {
            get
            {
                var value = BaseValue;
                var toRemove = new List<ParameterStatus>();
                foreach (var status in Statuses)
                {
                    if (status.IsActive)
                        value = status.Modify(value);
                    else
                        toRemove.Add(status);
                }

                foreach (var status in toRemove)
                {
                    Statuses.Remove(status);
                }
                
                return value;
            }
        }

        public SortedSet<ParameterStatus> Statuses { get; } = new SortedSet<ParameterStatus>(ParameterStatus.DefaultComparer);

        public Parameter(float baseValue)
        {
            BaseValue = baseValue;
        }
    }
}
