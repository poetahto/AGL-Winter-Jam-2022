using UniRx;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Resource")]
    public class ResourceModel : ScriptableObject
    {
        public StringReactiveProperty currentName;
        public StringReactiveProperty currentDescription;
        
        public DoubleReactiveProperty currentValue;
    }
}