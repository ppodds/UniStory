using Audio;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;

namespace Gameplay.Map
{
    public class MapInfo
    {
        private Rect _border;
        public AudioClip BGM { get; }

        public MapInfo(WzObject src)
        {
            if (src["VRLeft"] != null)
            {
                var left = src["VRLeft"].GetInt();
                var right = src["VRRight"].GetInt();
                var top = src["VRBottom"].GetInt();
                var bottom = src["VRTop"].GetInt();
                _border = new Rect(left / Constant.PixelsPerUnit, top / Constant.PixelsPerUnit,
                    (right - left) / Constant.PixelsPerUnit, (top - bottom) / Constant.PixelsPerUnit);
            }
            var bgmPath = src["bgm"].GetString();
            var split = bgmPath.Split("/");
            var loader = Loader.getInstance();
            var bgmRaw = (WzBinaryProperty)loader.Sound[split[0] + ".img"][split[1]];
            BGM = Mp3Loader.LoadMp3(split[1], bgmRaw.GetBytes());
        }
    }
}