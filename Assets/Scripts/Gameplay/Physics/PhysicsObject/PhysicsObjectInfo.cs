using System;
using UnityEngine;

namespace Gameplay.Physics.PhysicsObject
{
    [Serializable]
    public class PhysicsObjectInfo
    {
        [SerializeField] private Flag flags = 0;
        [SerializeField] private int footholdId = 0;
        [SerializeField] private float footholdSlope = 0.0f;
        [SerializeField] private int footholdLayer = 0;
        [SerializeField] private float groundBelow = 0.0f;
        [SerializeField] private bool isOnGround = true;
        [SerializeField] private bool isEnableJd = false;

        public Flag Flags
        {
            get => flags;
            set => flags = value;
        }

        public int FootholdId
        {
            get => footholdId;
            set => footholdId = value;
        }

        public float FootholdSlope
        {
            get => footholdSlope;
            set => footholdSlope = value;
        }

        public int FootholdLayer
        {
            get => footholdLayer;
            set => footholdLayer = value;
        }

        public float GroundBelow
        {
            get => groundBelow;
            set => groundBelow = value;
        }

        public bool IsOnGround
        {
            get => isOnGround;
            set => isOnGround = value;
        }

        public bool IsEnableJd
        {
            get => isEnableJd;
            set => isEnableJd = value;
        }

        public Vector2 Force
        {
            get => force;
            set => force = value;
        }

        public Vector2 Speed
        {
            get => speed;
            set => speed = value;
        }

        public Vector2 Acceleration
        {
            get => acceleration;
            set => acceleration = value;
        }

        [SerializeField] private Vector2 force = Vector2.zero;
        [SerializeField] private Vector2 speed = Vector2.zero;
        [SerializeField] private Vector2 acceleration = Vector2.zero;

        [Flags]
        public enum Flag
        {
            NoGravity = 0x0001,
            TurnAtEdges = 0x0002,
            CheckBelow = 0x0004
        }
    }
}