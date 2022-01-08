using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class ResourceSaveController : MonoBehaviour
    {
        [SerializeField] private ResourceModel resourceModel;
        
        private void Start()
        {
            var gameplaySystem = FindObjectOfType<GameplaySystem>();

            gameplaySystem.OnStart.Subscribe(ReadValue);
            gameplaySystem.OnStop.Subscribe(WriteValue);
        }

        private void ReadValue(Save save)
        {
            string resourceName = resourceModel.name;

            resourceModel.currentValue.Value = save.Resources.TryGetValue(resourceName, out double resourceAmount) 
                ? resourceAmount 
                : resourceModel.startingValue;
        }
        
        private void WriteValue(Save save)
        {
            string modelName = resourceModel.name;
            double currentValue = resourceModel.currentValue.Value;

            if (save.Resources.ContainsKey(modelName))
                save.Resources[modelName] = currentValue;
            
            else save.Resources.Add(modelName, currentValue);
        }
    }
}