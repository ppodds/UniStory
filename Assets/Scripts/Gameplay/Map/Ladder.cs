using System;
using MapleLib.WzLib;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Map
{
    [Serializable]
    public class Ladder
    {
        [SerializeField] private float x;

        // ladder top
        [SerializeField] private float top;

        // ladder bottom
        [SerializeField] private float bottom;
        [SerializeField] private bool isLadder;

        public float X => x;
        public float Top => top;
        public float Bottom => bottom;
        public bool IsLadder => isLadder;

        private const float DeltaX = 10 / Constant.PixelsPerUnit;
        private const float DeltaY = 5 / Constant.PixelsPerUnit;

        public Ladder(WzObject src)
        {
            x = src["x"].GetInt() / Constant.PixelsPerUnit;
            top = -src["y1"].GetInt() / Constant.PixelsPerUnit;
            bottom = -src["y2"].GetInt() / Constant.PixelsPerUnit;
            isLadder = src["l"].GetInt() == 1;
        }

        public bool IsInRange(Vector2 position)
        {
            var range = new Rect(x - DeltaX, top, 2 * DeltaX, top - bottom + 2 * DeltaY);
            return range.Contains(position);
        }

        public bool FellOff(float y, bool downwards)
        {
            var dy = downwards ? y - DeltaY : y + DeltaY;
            return dy < bottom || y - DeltaY > top;
        }
    }
}