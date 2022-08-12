using System;
using UnityEngine;
using WzComparerR2.WzLib;

namespace Gameplay.Physics
{
    [Serializable]
    public class Physics
    {
        [SerializeField] private FootholdTree footholdTree;

        public FootholdTree FootholdTree => footholdTree;

        public Physics(Wz_Node src)
        {
            footholdTree = new FootholdTree(src);
        }
    }
}