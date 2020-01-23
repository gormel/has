using Assets.Scripts.Core.Items.Armors;

namespace Assets.Scripts.View.Items {
    public class RobeView : ItemView
    {
        private Robe mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as Robe;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}