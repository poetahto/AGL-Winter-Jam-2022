using System;
using UniRx;
using UnityEngine;

namespace Game.Gameplay
{
    public class ResourceOverTime : MonoBehaviour
    {
        public double intervalSeconds;
        public ResourceModel source;
        public ResourceModel destination;
        public bool autoApply = true;
        
        private void Awake()
        {
            if (autoApply)
                Observable
                    .Interval(TimeSpan.FromSeconds(intervalSeconds))
                    .Subscribe(_ => ApplyRewards())
                    .AddTo(this);
        }
        
        public void ApplyRewards()
        {
            destination.currentValue.Value += source.currentValue.Value;
        }
    }
}