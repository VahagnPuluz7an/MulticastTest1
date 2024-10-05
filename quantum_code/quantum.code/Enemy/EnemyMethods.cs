using System;
using Photon.Deterministic;

namespace Quantum
{
    public partial struct EnemyComponent
    {
        public static event Action<Frame,EntityRef,EnemyComponent> Dead;
        
        public bool IsDead => Health <= 0;
        
        public void TakeDamage(Frame f, EntityRef entity, FP damage, Action Killed)
        {
            Health -= damage;
            if (!IsDead)
                return;
            Dead?.Invoke(f,entity,this);
            Killed?.Invoke();
            f.Destroy(entity);
        }
    }
}