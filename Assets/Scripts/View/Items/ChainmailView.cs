using Assets.Scripts.Core.Items.Armor;

namespace Assets.Scripts.View.Items {
    public class ChainmailView : ItemView
    {
        private Chainmail mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as Chainmail;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}