using System;
using System.Collections.Generic;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;
using UnityEngine.Serialization;
using Util;

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

        public MapleAnimation(WzObject src)
        {
            if (src is WzCanvasProperty)
            {
                frames.Add(new MapleFrame(src));
            }
            else
            {
                foreach (var sub in new WzObjectEnumerable(src))
                {
                    frames.Add(new MapleFrame(sub));
                }
            }

            isAnimated = frames.Count > 1;
            zigzag = src["zigzag"]?.GetInt() == 1;
        }
    }
}