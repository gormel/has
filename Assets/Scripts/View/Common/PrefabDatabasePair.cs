using System;
using UnityEngine;

namespace Assets.Scripts.View.Common {
    [Serializable]
    public class PrefabDatabasePair
    {
        [SerializeField]
        public string ObjectType;
        [SerializeField]
        public GameObject Prefab;
    }
}