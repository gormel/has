using Assets.Scripts.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Core.Common;

namespace Assets.Scripts.Core
{
    public class Map
    {
        public MapObject[,] StaticObjects { get; private set; }

        public int Width => StaticObjects.GetLength(0);
        public int Height => StaticObjects.GetLength(1);

        public Map()
        {
            StaticObjects = new MapObject[50, 50];

            for (int i = 0; i < 500; i++)
            {
                var x = UnityEngine.Random.Range(0, Width);
                var y = UnityEngine.Random.Range(0, Height);
                
                StaticObjects[x, y] = new Wall();
            }

            StaticObjects[0, 0] = null;
        }

        public Rect CheckCollision(Rect ofRect)
        {
            if (ofRect.xMin < 0 || ofRect.xMax > Width - ofRect.width || ofRect.yMin < 0 || ofRect.yMax > Height - ofRect.height)
                ofRect = new Rect(
                    Math.Min(Math.Max(0, ofRect.xMin), Width - ofRect.width),
                    Math.Min(Math.Max(0, ofRect.yMin), Height - ofRect.height),
                    ofRect.width, ofRect.height
                    );


            for (int x = Math.Max(0, Mathf.FloorToInt(ofRect.xMin - 1)); x < Math.Min(ofRect.xMax + 2, Width); x++)
            {
                for (int y = Math.Max(0, Mathf.FloorToInt(ofRect.yMin - 1)); y < Math.Min(ofRect.yMax + 2, Height); y++)
                {
                    if (StaticObjects[x, y] == null || StaticObjects[x, y].IsWalkable)
                        continue;

                    ofRect = MathUtils.Collide(ofRect, new Rect(x, y, 1, 1));
                }
            }

            return ofRect;
        }
    }
}
