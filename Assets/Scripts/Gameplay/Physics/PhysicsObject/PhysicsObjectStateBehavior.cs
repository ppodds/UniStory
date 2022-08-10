using UnityEngine;
using UnityEngine.EventSystems;

namespace Gameplay.Physics.PhysicsObject
{
    public abstract class PhysicsObjectStateBehavior
    {
        protected PhysicsObjectInfo Info;

        protected PhysicsObjectStateBehavior(PhysicsObjectInfo info)
        {
            Info = info;
        }
        
        public abstract void UpdateParameters();
    }
}