using Game.Level.Constructor;
using Mirror;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace Game.Level
{
    public class LevelController : NetworkBehaviour
    {
        [SerializeField] private LevelConstructor _constructor;

        public override void OnStartServer() => _constructor.GenerateServerLevel();
        public override void OnStartClient() => _constructor.GenerateClientLevel();

        public Point GetStartPoint()
        {
            var spline = _constructor.Container.Spline;
            SplineUtility.Evaluate(spline, 0,
                out float3 pos, out float3 tangent, out float3 up);

            Quaternion rot = Quaternion.LookRotation(tangent, up);
            return new Point()
            {
                Position = pos,
                Rotation = rot,
            };
        }

        public LevelConstructor LevelConstructor { get => _constructor; private set { } }
    }
}