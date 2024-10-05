using Photon.Deterministic;

namespace Quantum.Player
{
    public unsafe class PlayerUpgradeCommand : DeterministicCommand
    {
        public EntityRef PlayerEntity;

        public PlayerUpgradeCommand()
        {
            Log.Debug("Created");
        }
        
        public void Execute(Frame f)
        {
            Log.Debug("Executed");
            if (f.Unsafe.TryGetPointer(PlayerEntity, out PlayerData* playerData))
            {
                playerData->Upgrade(f,PlayerEntity);
            }
        }

        public override void Serialize(BitStream stream)
        {
            Log.Debug("Serialized");
            stream.Serialize(ref PlayerEntity);
        }
    }
}