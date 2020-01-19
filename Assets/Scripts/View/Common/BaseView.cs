using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    public abstract class BaseView : MonoBehaviour
    {
        public abstract void Load<T>(T model, Root root) where T : class;
        public abstract T Model<T>() where T : class;
    }
}
