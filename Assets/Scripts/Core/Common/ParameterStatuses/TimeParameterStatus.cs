using System;
using System.Diagnostics;

namespace Assets.Scripts.Core.Common.ParameterStatuses
{
    public class TimeParameterStatus : ParameterStatus
    {
        private Stopwatch mTimer = Stopwatch.StartNew();
        private readonly TimeSpan mTime;
        public override bool IsActive => mTimer.Elapsed < mTime;

        public TimeParameterStatus(ChangeType changeType, float changeValue, TimeSpan time) 
            : base(changeType, changeValue)
        {
            this.mTime = time;
        }
    }
}
