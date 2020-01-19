using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Core;
using System;

namespace Assets.Scripts.View.Common
{
    public class MapView : BaseView
    {
        public SpriteRenderer Background;
        public MapObjectInfo[] ObjectInfos;

        private Map mMap;

        public override void Load<T>(T model, Root root)
        {
            var map = model as Map;
            mMap = map;
            var width = map.StaticObjects.GetLength(0);
            var height = map.StaticObjects.GetLength(1);
            Background.size = new Vector2(width, height);
            Background.transform.localPosition = new Vector3(width / 2 - 0.5f, height / 2 - 0.5f);

            var infoCache = ObjectInfos.ToDictionary(i => Type.GetType(i.ObjectType), i => i.Prefab);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    var obj = map.StaticObjects[x, y];
                    if (obj != null && infoCache.TryGetValue(obj.GetType(), out var prefab))
                    {
                        var inst = Instantiate(prefab);
                        inst.transform.SetParent(transform);
                        inst.transform.localPosition = new Vector3(x, y, 0);
                        var view = inst.GetComponentInChildren<BaseView>();
                        view.Load(obj, root);
                    }
                }
            }
        }

        public override T Model<T>()
        {
            return mMap as T;
        }
    }
}
