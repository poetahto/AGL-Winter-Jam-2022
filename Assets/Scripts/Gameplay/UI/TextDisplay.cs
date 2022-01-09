using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class TextDisplay : Display
    {
        [SerializeField] private Text targetText;
        
        public override void UpdateText(string text)
        {
            targetText.text = text;
        }
    }
}