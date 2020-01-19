using System;
using UnityEngine;

namespace Assets.Scripts.Core.Common
{
    static class MathUtils
    {
        public static Rect Collide(Rect dyn, Rect stat)
        {
            if (stat.Overlaps(dyn))
            {
                var dir = dyn.center - stat.center;
                dir.Normalize();
                var wallDist = CenterEdgeDistance(stat, dir);
                var ofDist = CenterEdgeDistance(dyn, -dir);
                var dist = Math.Abs(wallDist + ofDist - (dyn.center - stat.center).magnitude);
                return new Rect(dyn.position + dir * dist, dyn.size);
            }

            return dyn;
        }

        private static float CenterEdgeDistance(Rect rect, Vector2 dir)
        {
            var c = rect.center;
            var tx = Math.Max((c.x - rect.xMin) / dir.x, (c.x - rect.xMax) / dir.x);
            var ty = Math.Max((c.y - rect.yMin) / dir.y, (c.y - rect.yMax) / dir.y);
            return Math.Min(tx, ty);
        }
    }
}
