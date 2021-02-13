using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using ArcadeShooter.Managers;

namespace ArcadeShooter.Swarm{
    public class DestructionSystem : ComponentSystem
    {
        float thresholdDistance = 2f;

        protected override void OnUpdate()
        {
            if(GameManager.IsGameOver())
            {
                return;
            }

            float3 playerPosition = (float3)GameManager.GetPlayerPosition();

            Entities.WithAll<EnemyTag>().ForEach((Entity enemy, ref Translation enemyPos) =>
            {
                playerPosition.y = enemyPos.Value.y;
                if(math.distance(enemyPos.Value, playerPosition) <= thresholdDistance)
                {
                    //explosions
                    FXManager.Instance.CreateExplosion(enemyPos.Value);
                    FXManager.Instance.CreateExplosion(playerPosition);
                    GameManager.EndGame();
                    PostUpdateCommands.DestroyEntity(enemy);
                }

                float3 enemyPosition = enemyPos.Value;

                Entities.WithAll<BulletTag>().ForEach((Entity bullet, ref Translation bulletPos) =>
                {
                    if(math.distance(enemyPosition, bulletPos.Value) <= thresholdDistance)
                    {
                        PostUpdateCommands.DestroyEntity(enemy);
                        PostUpdateCommands.DestroyEntity(bullet);
                        FXManager.Instance.CreateExplosion(enemyPosition);
                        GameManager.AddScore(1);
                    }
                });
            });
        }
    }
}
