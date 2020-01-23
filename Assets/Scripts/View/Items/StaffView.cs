using Assets.Scripts.Core.Items.Weapons;

namespace Assets.Scripts.View.Items {
    public class StaffView : ItemView
    {
        private Staff mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as Staff;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}