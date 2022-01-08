using System;
using UnityEngine;
using UnityEngine.UI;
using Display = Game.UI.Display;

namespace Game.Gameplay.Building
{
    public class BuildingView : MonoBehaviour
    {
        public enum Status
        {
            TooExpensive,
            Available,
            InProgress
        }
        
        [SerializeField] private GameObject tooExpensiveObject;
        [SerializeField] private GameObject availableObject;
        [SerializeField] private GameObject inProgressObject;

        public Button createBuildingButton;

        public Display nameDisplay;
        public Display costDisplay;
        public Display timeDisplay;
        public Display remainingTimeDisplay;
        public Display rewardDisplay;

        public bool HasBuildingButton => createBuildingButton != null;

        public bool HasName => nameDisplay != null;
        public bool HasCost => costDisplay != null;
        public bool HasTime => timeDisplay != null;
        public bool HasReward => rewardDisplay != null;

        public void SetStatus(Status status)
        {
            switch (status)
            {
                case Status.TooExpensive:
                    tooExpensiveObject.SetActive(true);
                    availableObject.SetActive(false);
                    inProgressObject.SetActive(false);
                    break;
                
                case Status.Available:
                    tooExpensiveObject.SetActive(false);
                    availableObject.SetActive(true);
                    inProgressObject.SetActive(false);
                    break;
                
                case Status.InProgress:
                    tooExpensiveObject.SetActive(false);
                    availableObject.SetActive(false);
                    inProgressObject.SetActive(true);
                    break;
                
                default: throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}