using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.View.Common
{
    internal class GameObjectCache
    {
        private readonly Transform mParent;
        private readonly GameObject mPrefab;

        private Queue<GameObject> mBuffer = new Queue<GameObject>();

        public List<GameObject> Allocated { get; } = new List<GameObject>();

        public GameObjectCache(Transform parent, GameObject prefab)
        {
            mParent = parent;
            mPrefab = prefab;
        }

        public GameObject Allocate()
        {
            if (mBuffer.Count > 0)
            {
                var buffered = mBuffer.Dequeue();
                buffered.SetActive(true);
                Allocated.Add(buffered);
                return buffered;
            }

            var allocated = Object.Instantiate(mPrefab);
            allocated.transform.SetParent(mParent);
            allocated.SetActive(true);
            Allocated.Add(allocated);
            return allocated;
        }

        public void Release(GameObject obj)
        {
            Allocated.Remove(obj);
            obj.SetActive(false);
            mBuffer.Enqueue(obj);
        }
    }
}
