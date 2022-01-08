using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Formatters/Normal")]
    public class NormalFormatter : ValueFormatter
    {
        public override string Format(double value)
        {
            return $"{value:N0}";
        }
    }
}