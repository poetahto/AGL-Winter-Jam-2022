using FMODUnity;
using UniRx;
using UnityEngine;

namespace Game.Gameplay.Building
{
    [CreateAssetMenu]
    public class BuildingModel : ScriptableObject
    {
        public StringReactiveProperty buildingName;
        public DoubleReactiveProperty buildingTime;
        public DoubleReactiveProperty unlockThreshold;

        public GameObject finishedPrefab;
        public GameObject constructionPrefab;
        public EventReference completionSound;
        
        public ResourceChange[] costs;
        public ResourceChange[] rewards;
    }
}