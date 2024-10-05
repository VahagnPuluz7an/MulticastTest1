using Photon.Deterministic;

namespace Quantum.Player
{
    public unsafe class MovementSystem : SystemMainThreadFilter<PlayerFilter>
    {
        public override void Update(Frame f, ref PlayerFilter filter)
        {
            if (!f.Unsafe.TryGetPointer(filter.Entity, out PlayerData* data))
                return;
            
            filter.CharacterController->MaxSpeed = data->Speed.Value;
           
            var input = *f.GetPlayerInput(filter.Link->Player);
            filter.CharacterController->Move(f, filter.Entity, input.Direction.XOY);
            if (input.Direction.Magnitude > 0)
                filter.Transform->Rotation = FPQuaternion.Lerp(filter.Transform->Rotation,
                    FPQuaternion.LookRotation(input.Direction.XOY), f.DeltaTime * 10);
            
            if(filter.Transform->Position.Y < -5)
                filter.Transform->Position = new FPVector3(filter.Link->Player, 0,0);
        }
    }
}
