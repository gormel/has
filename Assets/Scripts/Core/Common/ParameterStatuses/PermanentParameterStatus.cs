namespace Assets.Scripts.Core.Common.ParameterStatuses
{
    public class PermanentParameterStatus : ParameterStatus
    {
        public PermanentParameterStatus(ChangeType changeType, float changeValue) : base(changeType, changeValue)
        {
        }

        public override bool IsActive => true;
    }
}
