using UnityEngine;

namespace Game.Gameplay.Radio
{
    [CreateAssetMenu(menuName = "Radio Message")]
    public class RadioMessageModel : ScriptableObject
    {
        public string message;
    }
}