using Assets.Scripts.Core.Static;

namespace Assets.Scripts.View.Common {
    public class ExitView : MapObjectView
    {
        private Exit mExit;

        public override void Load<T>(T model, Root root)
        {
            mExit = model as Exit;
        }

        public override T Model<T>()
        {
            return mExit as T;
        }
    }
}