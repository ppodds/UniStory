using MapleLib.WzLib;
using UnityEngine;
using Util;

namespace Graphic
{
    public class MapleFrame
    {
        public MapleTexture MapleTexture { get; }
        public float Delay { get; } 
        public MapleFrame(WzObject src)
        {
            MapleTexture = new MapleTexture(src);
            Delay = (src["delay"]?.GetInt() ?? 0) / Constant.TimeStep;
        }
    }
}