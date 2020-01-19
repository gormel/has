using System.Collections.Generic;

namespace Assets.Scripts.Core.Common.ParameterStatuses
{
    public enum ChangeType
    {
        Set,
        Mul,
        Add
    }

    public abstract class ParameterStatus
    {
        private readonly ChangeType mChangeType;
        private readonly float mChangeValue;

        private class Comparer : IComparer<ParameterStatus>
        {
            public int Compare(ParameterStatus x, ParameterStatus y)
            {
                return x.Priority.CompareTo(y.Priority);
            }
        }

        public static IComparer<ParameterStatus> DefaultComparer { get { return new Comparer(); } }

        public int Priority { get; protected set; }
        public abstract bool IsActive { get; }

        protected ParameterStatus(ChangeType changeType, float changeValue)
        {
            this.mChangeType = changeType;
            this.mChangeValue = changeValue;

            switch (mChangeType)
            {
                case ChangeType.Set:
                    Priority = 0;
                    break;
                case ChangeType.Mul:
                    Priority = 1;
                    break;
                case ChangeType.Add:
                    Priority = 2;
                    break;
            }
        }

        public virtual float Modify(float value)
        {
            switch (mChangeType)
            {
                case ChangeType.Set:
                    return mChangeValue;
                case ChangeType.Mul:
                    return value * mChangeValue;
                case ChangeType.Add:
                    return value + mChangeValue;
            }

            return value;
        }
    }
}
