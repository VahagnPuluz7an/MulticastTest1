using System.Collections.Generic;
using System.Linq;
using Photon.Deterministic;

namespace Quantum.Player
{
    public unsafe class AttackSystem : SystemMainThreadFilter<PlayerFilter>
    {
        private const int MaxAttackCount = 3;
        
        public override void Update(Frame f, ref PlayerFilter filter)
        {
            int count = 0;
            var playerData = f.Unsafe.GetPointer<PlayerData>(filter.Entity);

            var enemies = new List<EntityRef>();
            
            foreach (var enemy in f.Unsafe.GetComponentBlockIterator<EnemyComponent>())
            {
                enemies.Add(enemy.Entity);
            }

            var playerTransform = filter.Transform;
            enemies = enemies.OrderBy(x =>
            {
                var enemyTransform = f.Unsafe.GetPointer<Transform3D>(x);
                var distance = FPVector3.Distance(playerTransform->Position, enemyTransform->Position);
                return distance;
            }).ToList();
            
            foreach (var enemy in enemies)
            {
                var enemyTransform = f.Unsafe.GetPointer<Transform3D>(enemy);

                if (FPVector3.Distance(filter.Transform->Position, enemyTransform->Position) > playerData->AttackRange.Value)
                {
                    f.Events.EnemyTakingDamage(enemy,false, filter.Link->Player);
                    continue;
                }

                count++;
                if (count > MaxAttackCount)
                {
                    f.Events.EnemyTakingDamage(enemy,false, filter.Link->Player);
                    continue;
                }

                f.Events.EnemyTakingDamage(enemy,true, filter.Link->Player);

                var enemyComponent = f.Unsafe.GetPointer<EnemyComponent>(enemy);
                enemyComponent->TakeDamage(f,enemy, playerData->Damage.Value * f.DeltaTime);
            }
        }
    }
}