using Graphic;
using MapleLib.WzLib;
using Unity.VisualScripting;
using UnityEngine;

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
        public static Portal Create(WzObject src, int mapId)
        {
            var mapleObj = MapleObject.Create("Portal");
            var portal = mapleObj.AddComponent<Portal>();
            portal.type = (Type)src["pt"].GetInt();
            portal.portalName = src["pn"].GetString();
            portal.targetName = src["tn"].GetString();
            portal.targetMap = src["tm"].GetInt();
            portal.isValid = portal.targetMap < 999999999;
            portal.isTargetInSameMap = portal.targetMap == mapId;
            portal.position = new Vector2(src["x"].GetInt(), -src["y"].GetInt()) / Constant.PixelsPerUnit;
            portal.transform.position = portal.position;
            var ani = portal.GetAnimationWz();
            if (ani != null)
                MapleAnimator.Create(mapleObj, ani);
            return portal;
        }

        private WzObject GetAnimationWz()
        {
            var src = Loader.getInstance().Map["MapHelper.img"]["portal"]["game"];
            return type switch
            {
                Type.Hidden => src["ph"]["default"]["portalContinue"],
                Type.Regular => src["pv"],
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