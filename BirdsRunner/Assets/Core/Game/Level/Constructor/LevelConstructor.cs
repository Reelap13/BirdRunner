using Game.Level.Constructor.Curve;
using Game.Level.Constructor.Obstacles;
using Game.Level.Constructor.Tube;
using UnityEngine;

namespace Game.Level.Constructor
{
    public class LevelConstructor : MonoBehaviour
    {
        [SerializeField] private CurveConstructor _curve;
        [SerializeField] private TubeConstructor _tube;
        [SerializeField] private ObstaclesConstructor _obstacles;

        public void GenerateLevel()
        {
            _curve.Generate();
            _tube.Generate();
            _obstacles.Generate();
        }
    }
}