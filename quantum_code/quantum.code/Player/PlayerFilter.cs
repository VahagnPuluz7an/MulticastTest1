namespace Quantum.Player
{
    public unsafe struct PlayerFilter
    {
        public Transform3D* Transform;
        public CharacterController3D* CharacterController;
        public EntityRef Entity;
        public PlayerLink* Link;
    }
}