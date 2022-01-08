using System.Linq;
using System.Text;
using Cysharp.Threading.Tasks;
using Game.UI;
using UniRx;
using UnityEngine;

namespace Game.Gameplay.Building
{
    public class BuildingController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private BuildingView buildingView;
        [SerializeField] private BuildingModel buildingModel;

        [Header("Optional")] 
        [SerializeField] private ValueFormatter costFormatter;
        [SerializeField] private ValueFormatter rewardFormatter;
        [SerializeField] private ValueFormatter timeFormatter;
        
        private void Start()
        {
            BindObjectChanges();
            
            if (buildingView.HasBuildingButton)
                BindCreateBuilding();

            if (buildingView.HasName)
                BindName();

            if (buildingView.HasTime)
                BindTime();
            
            if (buildingView.HasCost)
            {
                foreach (ResourceChange cost in buildingModel.costs)
                    cost.amount.Subscribe(_ => UpdateCost());
            }

            if (buildingView.HasReward)
            {
                foreach (ResourceChange reward in buildingModel.rewards)
                    reward.amount.Subscribe(_ => UpdateReward());
            }

            if (buildingView.remainingTimeDisplay != null)
            {
                _remainingTime
                    .Select(timeFormatter.Format)
                    .Subscribe(buildingView.remainingTimeDisplay.UpdateText);    
            }
        }

        private void BindObjectChanges()
        {
            Observable.EveryUpdate()
                .Where(_ => CanAfford() == false && _remainingTime.Value <= 0)
                .Subscribe(_ => buildingView.SetStatus(BuildingView.Status.TooExpensive));
            
            _remainingTime
                .Where(time => time <= 0 && CanAfford())
                .Subscribe(_ => buildingView.SetStatus(BuildingView.Status.Available));

            _remainingTime
                .Where(time => time > 0)
                .Subscribe(_ => buildingView.SetStatus(BuildingView.Status.InProgress));
        }

        private void BindName()
        {
            buildingModel.buildingName
                .Subscribe(buildingView.nameDisplay.UpdateText);
        }

        private void UpdateCost()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (ResourceChange cost in buildingModel.costs)
                stringBuilder.AppendLine($"{cost.resource.currentName} : {costFormatter.Format(cost.amount.Value)}");
            
            buildingView.rewardDisplay.UpdateText(stringBuilder.ToString());
        }

        private void UpdateReward()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            foreach (ResourceChange reward in buildingModel.rewards)
                stringBuilder.AppendLine($"{reward.resource.currentName} : {rewardFormatter.Format(reward.amount.Value)}");
            
            buildingView.rewardDisplay.UpdateText(stringBuilder.ToString());
        }

        private void BindTime()
        {
            buildingModel.buildingTime
                .Select(timeFormatter.Format)
                .Subscribe(buildingView.timeDisplay.UpdateText);
        }

        private void BindCreateBuilding()
        {
            buildingView.createBuildingButton.OnClickAsObservable()
                .Subscribe(_ => CreateBuilding().Forget());
        }

        private DoubleReactiveProperty _remainingTime = new (0);

        private bool CanAfford()
        {
            bool result = buildingModel.costs.All(cost => cost.CanAfford()); 
            return result;
        }
        
        private async UniTask CreateBuilding()
        {
            if (CanAfford() == false)
            {
                Debug.Log("Too expensive for you!");
                return;
            }

            foreach (ResourceChange cost in buildingModel.costs)
                cost.Apply();

            _remainingTime.Value = buildingModel.buildingTime.Value;
            
            while (_remainingTime.Value > 0)
            {
                await UniTask.Yield();
                _remainingTime.Value = Mathf.Max(0f, (float) _remainingTime.Value - Time.deltaTime);
            }

            foreach (ResourceChange reward in buildingModel.rewards)
                reward.Apply();
        }
    }
}