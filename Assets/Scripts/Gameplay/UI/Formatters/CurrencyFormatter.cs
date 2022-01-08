using UnityEngine;

namespace Game.UI
{
    [CreateAssetMenu(menuName = "Formatters/Currency")]
    public class CurrencyFormatter : ValueFormatter
    {
        [SerializeField] private bool showDecimals;
        
        public override string Format(double value)
        {
            string decimalResult = $"{value:C}";
            string noDecimalResult = $"{value:C0}";

            return showDecimals ? decimalResult : noDecimalResult;
        }
    }
}