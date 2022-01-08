using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Formatters/Percent")]
    public class PercentFormatter : ValueFormatter
    {
        [SerializeField] private bool showDecimals;
        
        public override string Format(double value)
        {
            string decimalResult = $"{value:P}";
            string noDecimalResult = $"{value:P0}";

            return showDecimals ? decimalResult : noDecimalResult;
        }
    }
}