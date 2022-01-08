using System;
using UniRx;

namespace Game.Gameplay.Building
{
    [Serializable]
    public class ResourceChange
    {
        public ResourceModel resource;
        public DoubleReactiveProperty amount;

        public bool CanAfford()
        {
            return resource.currentValue.Value + amount.Value >= 0;
        }
        
        public void Apply()
        {
            resource.currentValue.Value += amount.Value;
        }
    }
}