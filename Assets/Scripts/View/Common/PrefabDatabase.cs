using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    public abstract class PrefabDatabase : MonoBehaviour
    {
        [SerializeField]
        public List<PrefabDatabasePair> Pairs = new List<PrefabDatabasePair>();

        public GameObject this[string index]
        {
            get
            {
                return Pairs.FirstOrDefault(p => p?.ObjectType == index)?.Prefab;
            }
            set
            {
                var exist = Pairs.FirstOrDefault(p => p.ObjectType == index);
                if (exist != null)
                    exist.Prefab = value;
                else
                    Pairs.Add(new PrefabDatabasePair { ObjectType = index, Prefab = value });
            }
        }
    }
}
