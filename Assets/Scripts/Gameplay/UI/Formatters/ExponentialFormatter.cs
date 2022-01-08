using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Formatters/Exponential")]
    public class ExponentialFormatter : ValueFormatter
    {
        public override string Format(double value)
        {
            return $"{value:E2}";
        }
    }
}