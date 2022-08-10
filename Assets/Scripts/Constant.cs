public static class Constant
{
    public const float PixelsPerUnit = 64;
    public const float TimeStep = 1000;
    public const int LayerAmount = 8;

    public const float Gravity = -0.14f / PixelsPerUnit;
    public const float SwimGravity = -0.03f / PixelsPerUnit;
    public const float Friction = 0.3f / PixelsPerUnit;
    public const float SlopeFactor = 0.1f / PixelsPerUnit;
    public const float GroundSlip = 3.0f / PixelsPerUnit;
    public const float FlyFriction = 0.05f / PixelsPerUnit;
    public const float SwimFriction = 0.08f / PixelsPerUnit;
}