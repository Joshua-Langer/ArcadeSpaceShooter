using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;

namespace ArcadeShooter.Swarm{
    public class MovementSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<MoveForward>().ForEach((ref Translation trans, ref Rotation rotation, ref MoveForward moveForward) =>
            {
                trans.Value += moveForward.speed * Time.DeltaTime * math.forward(rotation.Value);
            });
        }
    }
}
