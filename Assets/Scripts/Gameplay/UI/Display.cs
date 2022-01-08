using UnityEngine;

namespace Game.UI
{
    public abstract class Display : MonoBehaviour
    {
        public abstract void UpdateText(string text);
    }
}