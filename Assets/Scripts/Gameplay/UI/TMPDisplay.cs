using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class TMPDisplay : Display
    {
        [SerializeField] private TMP_Text targetText;
        
        public override void ShowText(string text)
        {
            targetText.text = text;
        }
    }
}