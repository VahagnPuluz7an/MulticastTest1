namespace Quantum.Player
{
    public unsafe class PlayerUpgradeSystem : SystemMainThreadFilter<PlayerFilter>
    {
        public override void Update(Frame f, ref PlayerFilter filter)
        {
            //if (f.Unsafe.TryGetPointer(filter.Entity,out PlayerData* playerData))
            //{
             //   if (playerData->Upgrading)
             //   {
                //    playerData->Upgrade(f);
             //   }
            //}
        }
    }
}