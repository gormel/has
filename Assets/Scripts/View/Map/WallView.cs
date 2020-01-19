using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Static;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    public class WallView : MapObjectView
    {
        private Wall mWall;
        public override void Load<T>(T model, Root root)
        {
            mWall = model as Wall;
        }

        public override T Model<T>()
        {
            return mWall as T;
        }
    }
}
