using System;
using UnityEngine;

namespace Gameplay.Physics.PhysicsObject
{
    public class PhysicsObject : MonoBehaviour
    {
        public PhysicsObjectStateBehavior PhysicsObjectStateBehavior;
        [SerializeField] private PhysicsObjectInfo info = new();
        [SerializeField] private PhysicsObjectConstraints constraints = new();

        public PhysicsObjectInfo Info => info;

        public PhysicsObjectConstraints Constraints => constraints;

        public static PhysicsObject Create(string name)
        {
            var obj = new GameObject(name);
            var physicsObject = obj.AddComponent<PhysicsObject>();
            physicsObject.PhysicsObjectStateBehavior = new PhysicsObjectNormalBehavior(physicsObject.info);
            return physicsObject;
        }

        private void Awake()
        {
            PhysicsObjectStateBehavior = new PhysicsObjectNormalBehavior(info);
        }

        private void Update()
        {
            UpdateFoothold();
            PhysicsObjectStateBehavior.UpdateParameters();
            LimitMovement();
            transform.position += (Vector3)info.Speed;
        }

        private void UpdateFoothold()
        {
            var currentFoothold = Stage.Instance.Physics.FootholdTree.GetById(info.FootholdId);
            var pos = transform.position;
            var checkSlope = false;

            if (info.IsOnGround)
            {
                // if object on the ground and leave the current foothold
                // update it
                if (pos.x > currentFoothold.Right()) info.FootholdId = currentFoothold.Next;
                else if (pos.x < currentFoothold.Left()) info.FootholdId = currentFoothold.Prev;

                // if object leave the floor
                if (info.FootholdId == 0)
                    info.FootholdId = Stage.Instance.Physics.FootholdTree.GetFootholdBelow(pos).ID;
                else
                    checkSlope = true;
            }
            else
            {
                // if object in the air
                info.FootholdId = Stage.Instance.Physics.FootholdTree.GetFootholdBelow(pos).ID;

                // if there is no foothold below the object
                if (info.FootholdId == 0)
                    return;
            }

            var newFoothold = Stage.Instance.Physics.FootholdTree.GetById(info.FootholdId);

            info.FootholdSlope = newFoothold.Slope();
            var ground = newFoothold.GroundBelow(pos.x);

            // if the object stay on the ground
            // we need to set its y according to the floor slope
            if (info.Speed.y == 0 && checkSlope)
            {
                var deltaY = Math.Abs(info.FootholdSlope);

                if (info.FootholdSlope < 0.0)
                    deltaY *= (ground - pos.y);
                else if (info.FootholdSlope > 0.0)
                    deltaY *= (pos.y - ground);

                if (currentFoothold.Slope() != 0.0 || newFoothold.Slope() != 0.0)
                {
                    if (info.Speed.x > 0.0 && deltaY <= info.Speed.x)
                        transform.position = new Vector3(pos.x, ground, pos.z);
                    else if (info.Speed.x < 0.0 && deltaY >= info.Speed.x)
                        transform.position = new Vector3(pos.x, ground, pos.z);
                }
            }

            info.IsOnGround = transform.position.y == ground;

            if (info.FootholdLayer == 0 || info.IsOnGround)
                info.FootholdLayer = newFoothold.Layer;

            if (info.FootholdId == 0)
            {
                info.FootholdId = currentFoothold.ID;
                LimitX(currentFoothold.X1);
            }
        }

        private void LimitMovement()
        {
            var pos = transform.position;
            var currentFoothold = Stage.Instance.Physics.FootholdTree.GetById(Info.FootholdId);
            if (!Constraints.FreezeXPosition)
            {
                var currentX = pos.x;
                var nextX = currentX + Info.Speed.x;
                var left = Info.Speed.x < 0;
                var wall = Stage.Instance.Physics.FootholdTree.GetWall(currentFoothold, pos, left);
                var collision = left ? currentX >= wall && nextX <= wall : currentX <= wall && nextX >= wall;
                if (collision)
                    LimitX(wall);
            }

            if (!Constraints.FreezeYPosition)
            {
                var currentY = pos.y;
                var nextY = currentY + Info.Speed.y;

                var currentGround = currentFoothold.GroundBelow(pos.x);
                var nextGround = currentFoothold.GroundBelow(pos.x + Info.Speed.x);
                var collision = currentY >= currentGround && nextY <= nextGround;
                if (collision)
                {
                    LimitY(nextGround);
                }
            }
        }

        public void LimitX(float x)
        {
            Info.Speed = new Vector2(0, Info.Speed.y);
            var t = transform;
            var position = t.position;
            t.position = new Vector3(x, position.y, position.z);
        }

        public void LimitY(float y)
        {
            Info.Speed = new Vector2(Info.Speed.x, 0);
            var t = transform;
            var position = t.position;
            t.position = new Vector3(position.x, y, position.z);
        }
    }
}