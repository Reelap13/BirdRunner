using Game.Level.Constructor.Curve;
using Game.Level.Constructor.Obstacles;
using Game.Level.Constructor.Tube;
using Game.Level.Constructor.Plane;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

namespace Game.Level.Constructor
{
    public class LevelConstructor : MonoBehaviour
    {
        [field: SerializeField]
        public SplineContainer Container { get; private set; }

        [SerializeField] private CurveConstructor _curve;
        [SerializeField] private TubeConstructor _tube;
        [SerializeField] private ObstaclesConstructor _obstacles;
        [SerializeField] private MovingPlaneConstructor _plane;

        public void GenerateLevelInTheEditor()
        {
            _curve.Generate();
            _tube.Generate();
            _obstacles.Generate(true);
        }

        public void GenerateServerLevel()
        {
            _curve.Generate();
            _tube.Generate();
            _obstacles.Generate(false);
            _plane.Generate();
        }

        public void GenerateClientLevel()
        {
            _tube.Generate();
        }

        public ObstaclesConstructor Obstacles { get => _obstacles; private set { } }
    }
}