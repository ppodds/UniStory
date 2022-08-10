using System;
using Audio;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;

namespace Gameplay.Map
{
    [Serializable]
    public class MapInfo
    {
        public Rect Border;
        [SerializeField] private AudioClip bgm;

        [SerializeField] private bool cloud;

        // This is a bit filter
        [SerializeField] private int fieldLimit;
        [SerializeField] private bool hideMinimap;
        [SerializeField] private string mapMark;
        [SerializeField] private bool isUnderWater;
        [SerializeField] private bool isTown;

        public AudioClip BGM => bgm;

        public bool Cloud => cloud;

        public int FieldLimit => fieldLimit;

        public bool HideMinimap => hideMinimap;

        public string MapMark => mapMark;

        public bool IsUnderWater => isUnderWater;

        public bool IsTown => isTown;


        public MapInfo(WzObject src)
        {
            if (src["VRLeft"] != null)
            {
                var left = src["VRLeft"].GetInt();
                var right = src["VRRight"].GetInt();
                var top = src["VRBottom"].GetInt();
                var bottom = src["VRTop"].GetInt();
                Border = new Rect(left / Constant.PixelsPerUnit, bottom / Constant.PixelsPerUnit,
                    (right - left) / Constant.PixelsPerUnit, (top - bottom) / Constant.PixelsPerUnit);
            }

            var bgmPath = src["bgm"].GetString();
            var split = bgmPath.Split("/");
            var loader = Loader.getInstance();
            var bgmRaw = (WzBinaryProperty)loader.Sound[split[0] + ".img"][split[1]];
            bgm = Mp3Loader.LoadMp3(split[1], bgmRaw.GetBytes());
            cloud = src["cloud"].GetInt() == 1;
            fieldLimit = src["fieldLimit"].GetInt();
            hideMinimap = src["hideMinimap"].GetInt() == 1;
            mapMark = src["mapMark"].GetString();
            isUnderWater = src["swim"].GetInt() == 1;
            isTown = src["town"].GetInt() == 1;
        }
    }
}