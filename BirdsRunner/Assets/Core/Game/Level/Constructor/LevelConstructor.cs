using Game.Level.Constructor.Curve;
using Game.Level.Constructor.Obstacles;
using Game.Level.Constructor.Tube;
using Game.Level.Constructor.Plane;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;
using Game.Level.Constructor.TubeRings;

namespace Game.Level.Constructor
{
    public class LevelConstructor : MonoBehaviour
    {
        [field: SerializeField]
        public SplineContainer Container { get; private set; }

        [SerializeField] private CurveConstructor _curve;
        [SerializeField] private TubeConstructor _tube;
        [SerializeField] private TubeRingsContructor _tube_rings;
        [SerializeField] private ObstaclesConstructor _obstacles;
        [SerializeField] private MovingPlaneConstructor _plane;

        public void GenerateLevelInTheEditor()
        {
            _curve.Generate();
            _tube.Generate();
            _tube_rings.GenerateRings();
            _obstacles.Generate(true);
        }

        public void GenerateServerLevel()
        {
            _curve.Generate();
            _tube.Generate();
            _tube_rings.GenerateRings();
            _obstacles.Generate(false);
            _plane.Generate();
        }

        public void GenerateClientLevel()
        {
            _tube.Generate();
            _tube_rings.GenerateRings();
        }

        public ObstaclesConstructor Obstacles { get => _obstacles; private set { } }
    }
}