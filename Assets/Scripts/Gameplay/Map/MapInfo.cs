using System;
using Audio;
using UnityEngine;
using WzComparerR2.WzLib;

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


        public MapInfo(Wz_Node src)
        {
            if (src.Nodes["VRLeft"] != null)
            {
                var left = src.Nodes["VRLeft"].GetValue<int>();
                var right = src.Nodes["VRRight"].GetValue<int>();
                var top = src.Nodes["VRBottom"].GetValue<int>();
                var bottom = src.Nodes["VRTop"].GetValue<int>();
                Border = new Rect(left / Constant.PixelsPerUnit, bottom / Constant.PixelsPerUnit,
                    (right - left) / Constant.PixelsPerUnit, (top - bottom) / Constant.PixelsPerUnit);
            }

            var bgmPath = src.Nodes["bgm"].GetValue<string>();
            var split = bgmPath.Split("/");
            var loader = Loader.getInstance();
            var wzImage = loader.Sound.Nodes[split[0] + ".img"].GetValue<Wz_Image>();
            if (!wzImage.TryExtract())
                throw new Exception();
            var bgmRaw = wzImage.Node.Nodes[split[1]];
            bgm = Mp3Loader.LoadMp3(split[1], bgmRaw.GetValue<Wz_Sound>().ExtractSound());
            cloud = src.Nodes["cloud"].GetValue<bool>();
            fieldLimit = src.Nodes["fieldLimit"].GetValue<int>();
            hideMinimap = src.Nodes["hideMinimap"].GetValue<bool>();
            mapMark = src.Nodes["mapMark"].GetValue<string>();
            isUnderWater = src.Nodes["swim"].GetValue<bool>();
            isTown = src.Nodes["town"].GetValue<bool>();
        }
    }
}