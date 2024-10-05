namespace Quantum.Player
{
    public unsafe class PlayerSpawnSystem : SystemSignalsOnly, ISignalOnPlayerDataSet
    {
        public void OnPlayerDataSet(Frame frame, PlayerRef player)
        {
            var data = frame.GetPlayerData(player);
            var prototype = frame.FindAsset<EntityPrototype>(data.CharacterPrototype.Id);
            var entity = frame.Create(prototype);
            var playerLink = new PlayerLink()
            {
                Player = player,
            };
            frame.Add(entity, playerLink);
            if (frame.Unsafe.TryGetPointer<PlayerLink>(entity, out var link))
            {
                link->Player = player;
            }
            if (frame.Unsafe.TryGetPointer<Transform3D>(entity, out var transform))
            {
                transform->Position.X = (int)player;
            }
        }
    }
}