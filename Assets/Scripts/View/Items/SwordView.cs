using Assets.Scripts.Core.Items.Weapons;

namespace Assets.Scripts.View.Items
{
    public class SwordView : ItemView
    {
        private Sword mModel;

        public override void Load<T>(T model, Root root)
        {
            mModel = model as Sword;
        }

        public override T Model<T>()
        {
            return mModel as T;
        }
    }
}