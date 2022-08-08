using System;
using MapleLib.WzLib;
using UnityEngine;

namespace Gameplay.Physics
{
    public class Foothold
    {
        public int Id { get; }

        public int Layer { get; }

        public int Prev { get; }

        public int Next { get; }

        public Rect Rect { get; }

        public float X1 { get; }

        public float X2 { get; }

        public float Y1 { get; }

        public float Y2 { get; }

        public Foothold(WzObject src, int id, int layer)
        {
            Prev = src["prev"].GetInt();
            Next = src["next"].GetInt();
            Id = id;
            Layer = layer;
            var x1 = src["x1"].GetInt();
            var x2 = src["x2"].GetInt();
            var y1 = src["y1"].GetInt();
            var y2 = src["y2"].GetInt();
            Rect = new Rect(x1 < x2 ? x1 : x2 / Constant.PixelsPerUnit, y1 > y2 ? y1 : y2 / Constant.PixelsPerUnit,
                Math.Abs(x1 - x2) / Constant.PixelsPerUnit, Math.Abs(y1 - y2) / Constant.PixelsPerUnit);
            X1 = x1 / Constant.PixelsPerUnit;
            X2 = x2 / Constant.PixelsPerUnit;
            Y1 = y1 / Constant.PixelsPerUnit;
            Y2 = y2 / Constant.PixelsPerUnit;
        }

        public float Left()
        {
            return Rect.xMin;
        }

        public float Right()
        {
            return Rect.xMax;
        }

        public float Top()
        {
            return Rect.yMax;
        }

        public float Bottom()
        {
            return Rect.yMin;
        }

        public bool IsWall()
        {
            Debug.Log(Rect.width);
            return Rect.width == 0;
        }

        public bool IsFloor()
        {
            Debug.Log(Rect.height);
            return Rect.height == 0;
        }

        public bool IsLeftEdge()
        {
            return Prev == 0;
        }

        public bool IsRightEdge()
        {
            return Next == 0;
        }

        public bool Contains(Vector2 point)
        {
            return Rect.Contains(point);
        }

        public bool IsBlocking(Rect rect)
        {
            return IsWall() && Rect.Overlaps(rect);
        }

        public float Width()
        {
            return Rect.width;
        }

        public float Height()
        {
            return Rect.height;
        }

        public float Slope()
        {
            return IsWall() ? 0.0f : Y1 - Y2 / X2 - X1;
        }

        public float GroundBelow(float x)
        {
            return IsFloor() ? Rect.y : Slope() * (x - X1) + Rect.y;
        }
    }
}