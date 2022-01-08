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
        
        private void Awake()
        {
            Observable
                .Interval(TimeSpan.FromSeconds(intervalSeconds))
                .Subscribe(_ => ApplyRewards())
                .AddTo(this);
        }
        
        private void ApplyRewards()
        {
            destination.currentValue.Value += source.currentValue.Value;
        }
    }
}