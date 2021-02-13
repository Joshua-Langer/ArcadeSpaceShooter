using ArcadeShooter.Managers;
using Unity.Entities;

namespace ArcadeShooter.Swarm{
    public class ClearOnRestartSystem : ComponentSystem
    {
        float endLifetime = 2f;

        protected override void OnUpdate()
        {
            if(GameManager.IsGameOver())
            {
                EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                Entities.WithAll<EnemyTag>().WithNone<Lifetime>().ForEach((Entity enemy) =>
                {
                    entityManager.AddComponentData(enemy, new Lifetime { Value = endLifetime});
                });
            }
        }
    }
}

