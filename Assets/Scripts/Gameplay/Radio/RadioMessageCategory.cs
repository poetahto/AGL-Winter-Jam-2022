using UnityEngine;

namespace Game.Gameplay.Radio
{
    [CreateAssetMenu(menuName = "Radio Category")]
    public class RadioMessageCategory : ScriptableObject
    {
        public RadioMessageModel[] messages;
    }
}