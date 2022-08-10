using System;
using MapleLib.WzLib;
using UnityEngine;

namespace Gameplay.Physics
{
    [Serializable]
    public class Foothold
    {
        [SerializeField] private int id;

        [SerializeField] private int layer;

        [SerializeField] private int prev;

        [SerializeField] private int next;

        [SerializeField] private Rect rect;

        [SerializeField] private float x1;

        [SerializeField] private float x2;

        [SerializeField] private float y1;

        [SerializeField] private float y2;

        public Foothold(WzObject src, int id, int layer)
        {
            prev = src["prev"].GetInt();
            next = src["next"].GetInt();
            this.id = id;
            this.layer = layer;
            var x1 = src["x1"].GetInt();
            var x2 = src["x2"].GetInt();
            var y1 = -src["y1"].GetInt();
            var y2 = -src["y2"].GetInt();
            rect = new Rect((x1 < x2 ? x1 : x2) / Constant.PixelsPerUnit, (y1 < y2 ? y1 : y2) / Constant.PixelsPerUnit,
                Math.Abs(x1 - x2) / Constant.PixelsPerUnit, Math.Abs(y1 - y2) / Constant.PixelsPerUnit);
            this.x1 = x1 / Constant.PixelsPerUnit;
            this.x2 = x2 / Constant.PixelsPerUnit;
            this.y1 = y1 / Constant.PixelsPerUnit;
            this.y2 = y2 / Constant.PixelsPerUnit;
        }

        public Foothold()
        {
            prev = 0;
            next = 0;
            id = 0;
            layer = 0;
            rect = new Rect(0, 0,
                0, 0);
            x1 = 0;
            x2 = 0;
            y1 = 0;
            y2 = 0;
        }

        public int ID => id;

        public int Layer => layer;

        public int Prev => prev;

        public int Next => next;

        public Rect Rect => rect;

        public float X1 => x1;

        public float X2 => x2;

        public float Y1 => y1;

        public float Y2 => y2;

        public float Left()
        {
            return rect.xMin;
        }

        public float Right()
        {
            return rect.xMax;
        }

        public float Top()
        {
            return rect.yMax;
        }

        public float Bottom()
        {
            return rect.yMin;
        }

        public bool IsWall()
        {
            return rect.width == 0;
        }

        public bool IsFloor()
        {
            return rect.height == 0;
        }

        public bool IsLeftEdge()
        {
            return prev == 0;
        }

        public bool IsRightEdge()
        {
            return next == 0;
        }

        public bool Contains(Vector2 point)
        {
            return rect.Contains(point);
        }

        public bool IsBlocking(Rect target)
        {
            return IsWall() && rect.Overlaps(target);
        }

        public float Width()
        {
            return rect.width;
        }

        public float Height()
        {
            return rect.height;
        }

        public float Slope()
        {
            return IsWall() ? 0.0f : (y2 - y1) / rect.width;
        }

        public float GroundBelow(float x)
        {
            if (IsFloor())
                return rect.y;
            var slope = Slope();
            return slope * (x - x1) + (slope > 0 ? rect.yMin : rect.yMax);
        }
    }
}