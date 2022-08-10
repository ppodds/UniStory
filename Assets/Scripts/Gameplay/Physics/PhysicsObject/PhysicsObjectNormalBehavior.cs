using UnityEngine;

namespace Gameplay.Physics.PhysicsObject
{
    public class PhysicsObjectNormalBehavior : PhysicsObjectStateBehavior
    {
        public PhysicsObjectNormalBehavior(PhysicsObjectInfo info) : base(info)
        {
        }

        public override void UpdateParameters()
        {
            Info.Acceleration = Vector2.zero;
            if (Info.IsOnGround)
            {
                Info.Acceleration += Info.Force;

                if (Info.Acceleration.x == 0.0 && Info.Speed.x < 0.1 / Constant.PixelsPerUnit && Info.Speed.x > -0.1 / Constant.PixelsPerUnit)
                {
                    Info.Speed = new Vector2(0, Info.Speed.y);
                }
                else
                {
                    var inertia = Info.Speed.x / Constant.GroundSlip;
                    var slope = Info.FootholdSlope;
                    if (slope > 0.5)
                    {
                        slope = 0.5f;
                    }
                    else if (slope < -0.5)
                    {
                        slope = -0.5f;
                    }

                    Info.Acceleration -=
                        new Vector2(
                            (float)(Constant.Friction + Constant.SlopeFactor * (1.0 + slope * -inertia)) * inertia, 0);
                }
            }
            else if (!Info.Flags.HasFlag(PhysicsObjectInfo.Flag.NoGravity))
            {
                Info.Acceleration += new Vector2(0, Constant.Gravity);
            }

            Info.Force = Vector2.zero;

            Info.Speed += Info.Acceleration;
        }
    }
}