using System;
using System.Collections.Generic;
using MapleLib.WzLib;
using MapleLib.WzLib.WzProperties;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Physics
{
    [Serializable]
    class SerializableFootholdDictionary : SerializableDictionary<int, Foothold>
    {
    }

    [Serializable]
    public class FootholdTree
    {
        [SerializeField] private SerializableFootholdDictionary footholds = new SerializableFootholdDictionary();

        public FootholdTree(WzObject src)
        {
            foreach (var layer in ((WzSubProperty)src).WzProperties)
            {
                foreach (var a in layer.WzProperties)
                {
                    foreach (var b in a.WzProperties)
                    {
                        var id = int.Parse(b.Name);
                        var foothold = new Foothold(b, id, int.Parse(layer.Name));
                        footholds.Add(id, foothold);
                    }
                }
            }
        }

        public Foothold GetById(int id)
        {
            try
            {
                return footholds[id];
            }
            catch (KeyNotFoundException e)
            {
                return new Foothold();
            }
        }

        public Foothold GetFootholdBelow(Vector2 pos)
        {
            Foothold ret = new Foothold();
            var ground = float.MinValue;
            foreach (var pair in footholds)
            {
                var foothold = pair.Value;
                var g = foothold.GroundBelow(pos.x);
                if (pos.x <= foothold.Right() && pos.x >= foothold.Left() && pos.y >= g && g >= ground)
                {
                    ground = g;
                    ret = foothold;
                }
            }

            return ret;
        }

        public float GetWall(Foothold foothold, Vector2 pos, bool left)
        {
            var detection = new Rect(pos.x, pos.y, 0, 50 / Constant.PixelsPerUnit);

            var check1 = GetById(left ? foothold.Prev : foothold.Next);
            if (check1.IsBlocking(detection)) return left ? foothold.Left() : foothold.Right();
            var check2 = GetById(left ? check1.Prev : check1.Next);
            if (check2.IsBlocking(detection)) return left ? foothold.Left() : foothold.Right();
            return left ? float.MinValue : float.MaxValue;
        }
    }
}