using UniRx;
using UnityEngine;

namespace Game.Gameplay
{
    public class ClickController : MonoBehaviour
    {
        [SerializeField] private ClickModel clickModel;
        
        private void Awake()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Subscribe(_ => ApplyRewards())
                .AddTo(this);
        }

        private void ApplyRewards()
        {
            clickModel.destination.currentValue.Value += clickModel.source.currentValue.Value;
        }
    }
}