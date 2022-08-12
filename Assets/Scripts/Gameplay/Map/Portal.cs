using System;
using Graphic;
using Unity.VisualScripting;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Map
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Type type;
        [SerializeField] private string portalName;
        [SerializeField] private string targetName;
        [SerializeField] private int targetMap;
        [SerializeField] private Vector2 position;
        [SerializeField] private bool isValid;
        [SerializeField] private bool isTargetInSameMap;

        public static Portal Create(Wz_Node src, int mapId)
        {
            var mapleObj = MapleObject.Create("Portal");
            var portal = mapleObj.AddComponent<Portal>();
            portal.type = (Type)src.Nodes["pt"].GetValue<int>();
            portal.portalName = src.Nodes["pn"].GetValue<string>();
            portal.targetName = src.Nodes["tn"].GetValue<string>();
            portal.targetMap = src.Nodes["tm"].GetValue<int>();
            portal.isValid = portal.targetMap < 999999999;
            portal.isTargetInSameMap = portal.targetMap == mapId;
            portal.position =
                new Vector2(src.Nodes["x"].GetValue<int>(), -src.Nodes["y"].GetValue<int>()) /
                Constant.PixelsPerUnit;
            portal.transform.position = portal.position;
            var ani = portal.GetAnimationWz();
            if (ani != null)
                MapleAnimator.Create(mapleObj, ani);
            return portal;
        }

        private Wz_Node GetAnimationWz()
        {
            var wzImage = Loader.getInstance().Map.Nodes["MapHelper.img"].GetValue<Wz_Image>();
            if (!wzImage.TryExtract())
                throw new Exception();
            var src = wzImage.Node.Nodes["portal"].Nodes["game"];
            return type switch
            {
                Type.Hidden => src.Nodes["ph"].Nodes["default"].Nodes["portalContinue"],
                Type.Regular => src.Nodes["pv"].Nodes["default"],
                _ => null
            };
        }

        public enum Type
        {
            Spawn,
            Invisible,
            Regular,
            Touch,
            Type4,
            Type5,
            Warp,
            Scripted,
            ScriptedInvisible,
            ScriptedTouch,
            Hidden,
            ScriptedHidden,
            Spring1,
            Spring2,
            Type14
        };
    }
}