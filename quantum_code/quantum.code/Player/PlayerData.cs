using System.Collections.Generic;
using System.Linq;
using Photon.Deterministic;

namespace Quantum
{
    public unsafe partial struct PlayerData
    {
        public void StartUpgrade()
        {
            Upgrading = true;
        }
        
        public void Upgrade(Frame f)
        {
            var upgrades = new List<UpgradeData>()
            {
                Damage,
                AttackRange,
                Speed
            };

            int i = GetRandomUpgradeIndex(f, upgrades);
            Upgrading = false;

            switch (i)
            {
                case 0:
                    Damage.Value += Damage.Step;
                    break;
                case 1:
                    AttackRange.Value += AttackRange.Step;
                    break;
                case 2:
                    Speed.Value += Speed.Step;
                    break;
            }

            f.Events.PlayerUpgraded(this);
        }
        
        private int GetRandomUpgradeIndex(Frame f, List<UpgradeData> upgrades)
        {
            var totalChance = upgrades.Aggregate<UpgradeData, FP>(0, (current, upgrade) => current + upgrade.Chance);
            var randomValue = f.Global->RngSession.Next(0, totalChance);
            FP cumulativeChance = 0;
            for (int i = 0; i < upgrades.Count; i++)
            {
                var upgrade = upgrades[i];
                cumulativeChance += upgrade.Chance;
                if (randomValue < cumulativeChance)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}