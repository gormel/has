using Assets.Scripts.Core.Static;

namespace Assets.Scripts.View.Common {
    public class VoidView : MapObjectView
    {
        private MapVoid mVoid;

        public override void Load<T>(T model, Root root)
        {
            mVoid = model as MapVoid;
        }

        public override T Model<T>()
        {
            return mVoid as T;
        }
    }
}