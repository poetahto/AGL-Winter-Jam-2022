using UnityEngine;

namespace Game.Gameplay
{
    [CreateAssetMenu(menuName = "Click Rewards")]
    public class ClickModel : ScriptableObject
    {
        public ResourceModel source;
        public ResourceModel destination;
    }
}