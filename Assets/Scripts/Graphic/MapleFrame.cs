using WzComparerR2.WzLib;

namespace Graphic
{
    public class MapleFrame
    {
        public MapleTexture MapleTexture { get; }
        public float Delay { get; } 
        public MapleFrame(Wz_Node src)
        {
            MapleTexture = new MapleTexture(src);
            Delay = (src.FindNodeByPath("delay")?.GetValue<int>() ?? 100) / Constant.TimeStep;
        }
    }
}