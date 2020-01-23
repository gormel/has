using Assets.Scripts.Core.Items.Weapons;

namespace Assets.Scripts.View.Items {
    public class SpearView : ItemView
    {
        private Spear mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as Spear;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}