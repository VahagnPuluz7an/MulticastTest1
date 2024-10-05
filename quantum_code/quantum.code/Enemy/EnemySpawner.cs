using Photon.Deterministic;

namespace Quantum
{
    public unsafe class EnemySpawner : SystemSignalsOnly
    {
        public override void OnEnabled(Frame f)
        {
            base.OnEnabled(f);
            EnemyComponent.Dead += Spawn;
        }

        public override void OnDisabled(Frame f)
        {
            base.OnDisabled(f);
            EnemyComponent.Dead -= Spawn;
        }

        private void Spawn(Frame f,EntityRef entity, EnemyComponent component)
        {
            string[] enemiesPaths = {
                "Resources/DB/BigSnakeEntity|EntityPrototype",
                "Resources/DB/ShellEntity|EntityPrototype",
                "Resources/DB/SnakeEntity|EntityPrototype"
            };
            
            string randomPath = enemiesPaths[f.Global->RngSession.Next(0, enemiesPaths.Length)];
            var entityPrototype = f.FindAsset<EntityPrototype>(randomPath);
            
            var exampleEntity = f.Create(entityPrototype);
            
            if (f.Unsafe.TryGetPointer<Transform3D>(exampleEntity, out var transform))
            {
                transform->Position = new FPVector3(1 * f.Global->RngSession.Next(-20, 20), 0,
                    1 * f.Global->RngSession.Next(0, 30));
            }
        }
    }
}