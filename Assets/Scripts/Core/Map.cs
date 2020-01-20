﻿using Assets.Scripts.Core.Static;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;
using Assets.Scripts.Core.Common;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core
{
    public class Map
    {
        public MapObject[,] StaticObjects { get; private set; }

        public int Width => StaticObjects.GetLength(0);
        public int Height => StaticObjects.GetLength(1);

        public Vector2Int SpawnPoint { get; }

        public Map()
        {
            StaticObjects = new MapObject[50, 50];

            var mask = new bool[Width, Height];

            //crete rooms
            var rooms = 10;
            var roomCenters = new Vector2Int[rooms];
            for (int i = 0; i < rooms; i++)
            {
                var x = Random.Range(0, Width);
                var y = Random.Range(0, Height);
                var roomW = Random.Range(4, 10);
                var roomH = Random.Range(4, 10);

                roomCenters[i] = new Vector2Int(x, y);

                for (int rx = 0; rx < roomW; rx++)
                {
                    for (int ry = 0; ry < roomH; ry++)
                    {
                        var ex = x + rx - roomW / 2;
                        var ey = y + ry - roomH / 2;
                        if (ex < 0 || ey < 0 || ex >= Width || ey >= Height)
                            continue;

                        mask[ex, ey] = true;
                    }
                }
            }

            //create tunnels
            for (int i = 0; i < rooms * 3; i++)
            {
                var dirIndex = Random.Range(0, 3) + 1;
                var dir = new Vector2Int(1, 0);
                for (int j = 0; j < dirIndex; j++)
                {
                    dir = Rotate90(dir);
                }

                var p = roomCenters[i / 3];
                var prev = mask[p.x, p.y];
                while (p.x >= 0 && p.x < Width && p.y >= 0 && p.y < Height && (prev || !mask[p.x, p.y]))
                {
                    prev = mask[p.x, p.y];
                    mask[p.x, p.y] = true;
                    p += dir;
                }
            }

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    StaticObjects[x, y] = GetStaticObjectType(mask, x, y);
                }
            }

            SpawnPoint = GetRandomFreeLocation();
        }

        private MapObject GetStaticObjectType(bool[,] mask, int x, int y)
        {
            if (mask[x, y])
                return null;

            for (int xx = 0; xx < 3; xx++)
            {
                for (int yy = 0; yy < 3; yy++)
                {
                    var cx = x + xx - 1;
                    var cy = y + yy - 1;
                    if (cx < 0 || cy < 0 || cx >= Width || cy >= Width)
                        continue;

                    if (mask[cx, cy])
                        return new Wall();
                }
            }

            return new MapVoid();
        }

        public Vector2Int GetRandomFreeLocation()
        {
            var sx = -1;
            var sy = -1;

            while (sx < 0 || sy < 0 || (StaticObjects[sx, sy] != null && !StaticObjects[sx, sy].IsWalkable))
            {
                sx = Random.Range(0, Width);
                sy = Random.Range(0, Height);
            }

            return new Vector2Int(sx, sy);
        }

        private Vector2Int Rotate90(Vector2Int v) => new Vector2Int(v.y, -v.x);

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
