using Fusion;
using UnityEngine;

namespace Runtime
{
    public struct NetInput : INetworkInput
    {
        public Vector2 Move;
        public Vector2 Look;

        public NetworkBool JumpHeld;
        public NetworkBool DashHeld;
        public NetworkBool SlideHeld;

        // One-shot flags consumed per network tick.
        public NetworkBool JumpPressed;
        public NetworkBool DashPressed;
        public NetworkBool SlidePressed;

        // Quantized scroll input: -1 / 0 / +1
        public sbyte RunSpeedDelta;

        public NetworkBool ClimbHeld;
        public NetworkBool DescendHeld;
    }
}
