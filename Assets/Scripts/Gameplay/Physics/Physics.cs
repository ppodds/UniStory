using System;
using MapleLib.WzLib;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.Physics
{
    [Serializable]
    public class Physics
    {
        [SerializeField] private FootholdTree footholdTree;

        public FootholdTree FootholdTree => footholdTree;

        public Physics(WzObject src)
        {
            footholdTree = new FootholdTree(src);
        }
    }
}