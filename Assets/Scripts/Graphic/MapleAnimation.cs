using System;
using System.Collections.Generic;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Graphic
{
    [Serializable]
    public class MapleAnimation
    {
        [SerializeField] private List<MapleFrame> frames = new List<MapleFrame>();
        [SerializeField] private bool isAnimated;
        [SerializeField] private bool zigzag;

        public List<MapleFrame> Frames => frames;

        public bool IsAnimated => isAnimated;

        public bool Zigzag => zigzag;

        public MapleAnimation(Wz_Node src)
        {
            if (src.GetValue<Wz_Png>() != null)
            {
                frames.Add(new MapleFrame(src));
            }
            else
            {
                foreach (var sub in src.Nodes)
                {
                    if (sub.GetValue<Wz_Png>() != null)
                        frames.Add(new MapleFrame(sub));
                }
            }

            isAnimated = frames.Count > 1;
            zigzag = src.FindNodeByPath("zigzag")?.GetValue<bool>() ?? false;
        }
    }
}