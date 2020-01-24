using Assets.Scripts.Core.Static;
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

        public int Rooms { get; }

        public Vector2Int SpawnPoint { get; }

        public Map(Game game, int level)
        {
            var sizeAddition = level * 10;
            StaticObjects = new MapObject[25 + sizeAddition, 25 + sizeAddition];

            var mask = new bool[Width, Height];

            //crete rooms
            Rooms = Math.Max((int)(Width * Height * 0.6 / ((10 + level) * (10 + level))), 2);
            var roomCenters = new List<Vector2Int>(new Vector2Int[Rooms]);
            var roomSizes = new List<Vector2Int>(new Vector2Int[Rooms]);
            for (int i = 0; i < Rooms; i++)
            {
                var roomW = Random.Range(4, 10) + level;
                var roomH = Random.Range(4, 10) + level;
                if (roomW % 2 == 0)
                    roomW++;
                if (roomH % 2 == 0)
                    roomH++;

                var x = Random.Range(roomW / 2 + 1, Width - roomW / 2 - 1);
                var y = Random.Range(roomH / 2 + 1, Height - roomH / 2 - 1);

                bool incorrect = false;
                for (int rx = -1; rx < roomW + 1; rx++)
                {
                    for (int ry = -1; ry < roomH + 1; ry++)
                    {
                        var ex = x + rx - roomW / 2;
                        var ey = y + ry - roomH / 2;
                        if (ex < 0 || ey < 0 || ex >= Width || ey >= Height)
                            continue;

                        if (mask[ex, ey])
                        {
                            incorrect = true;
                            break;
                        }
                    }

                    if (incorrect)
                        break;
                }

                if (incorrect)
                {
                    i--;
                    continue;
                }

                roomCenters[i] = new Vector2Int(x, y);
                roomSizes[i] = new Vector2Int(roomW, roomH);

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
            for (int i = 0; i < Rooms * 4; i++)
            {
                var dirIndex = i % 4 + 1;
                var dir = new Vector2Int(1, 0);
                for (int j = 0; j < dirIndex; j++)
                {
                    dir = Rotate90(dir);
                }

                var offset = roomSizes[i / 4];
                offset.Scale(dir);
                offset.Set((offset.x / 2 + Math.Sign(offset.x)) * Math.Abs(Math.Sign(offset.x)), (offset.y / 2 + Math.Sign(offset.y)) * Math.Abs(Math.Sign(offset.y)));
                var p = roomCenters[i / 4] + offset;
                while (p.x >= 0 && p.x < Width && p.y >= 0 && p.y < Height)
                {
                    if (Neighbours(mask, p.x, p.y) > 1)
                    {
                        mask[p.x, p.y] = true;
                        break;
                    }

                    mask[p.x, p.y] = true;
                    p += dir;
                }
            }

            //fix border
            for (int x = 0; x < Width; x++)
            {
                mask[x, 0] = false;
                mask[x, Height - 1] = false;
            }

            for (int y = 0; y < Height; y++)
            {
                mask[0, y] = false;
                mask[Width - 1, y] = false;
            }

            //fix dead ends
            for (int x = 1; x < Width - 1; x++)
            {
                for (int y = 1; y < Height; y++)
                {
                    if (!mask[x, y])
                        continue;

                    if (CheckDeadEndAt(mask, x, y))
                        mask[x, y] = false;
                    else
                        break;
                }

                for (int y = Height - 2; y > 0; y--)
                {
                    if (!mask[x, y])
                        continue;

                    if (CheckDeadEndAt(mask, x, y))
                        mask[x, y] = false;
                    else
                        break;
                }
            }

            for (int y = 1; y < Height - 1; y++)
            {
                for (int x = 1; x < Width - 1; x++)
                {
                    if (!mask[x, y])
                        continue;

                    if (CheckDeadEndAt(mask, x, y))
                        mask[x, y] = false;
                    else
                        break;
                }

                for (int x = Width - 2; x > 0; x--)
                {
                    if (!mask[x, y])
                        continue;

                    if (CheckDeadEndAt(mask, x, y))
                        mask[x, y] = false;
                    else
                        break;
                }
            }

            //convert mask to objects
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    StaticObjects[x, y] = GetStaticObjectType(mask, x, y);
                }
            }

            var spawnRoomIndex = Random.Range(0, roomCenters.Count);
            SpawnPoint = roomCenters[spawnRoomIndex];
            roomCenters.RemoveAt(spawnRoomIndex);
            var exitIndex = Random.Range(0, roomCenters.Count);
            var exitX = Math.Max(0, Math.Min(Width - 1, roomCenters[exitIndex].x));
            var exitY = Math.Max(0, Math.Min(Height - 1, roomCenters[exitIndex].y));
            StaticObjects[exitX, exitY] = new Exit(game);
        }

        private float Sq(float x) => x * x;

        private int Neighbours(bool[,] mask, int x, int y)
        {
            var count = 0;
            if (x > 0 && x < Width - 1 && mask[x - 1, y])
                count++;
            if (x > 0 && x < Width - 1 && mask[x + 1, y])
                count++;
            if (y > 0 && y < Height - 1 && mask[x, y - 1])
                count++;
            if (y > 0 && y < Height - 1 && mask[x, y + 1])
                count++;

            return count;
        }

        public bool IsFree(Vector2 at)
        {
            var obj = StaticObjects[Mathf.FloorToInt(at.x), Mathf.FloorToInt(at.y)];
            return obj == null || obj.IsWalkable;
        }

        private bool CheckDeadEndAt(bool[,] mask, int x, int y)
        {

            return Neighbours(mask, x, y) == 1;
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
