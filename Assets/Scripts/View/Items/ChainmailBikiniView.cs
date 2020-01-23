using Assets.Scripts.Core.Items.Armors;

namespace Assets.Scripts.View.Items {
    public class ChainmailBikiniView : ItemView
    {
        private ChainmailBikini mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as ChainmailBikini;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}