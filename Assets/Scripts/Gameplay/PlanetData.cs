using UniRx;
using UnityEngine;

namespace Game
{
    public class PlanetData : MonoBehaviour
    {
        public DoubleReactiveProperty population;
        public DoubleReactiveProperty currency;
        public DoubleReactiveProperty housing;
    }
}