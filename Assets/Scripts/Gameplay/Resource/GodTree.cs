using Game;
using Game.Gameplay.Building;
using UnityEngine;

namespace Gameplay.Resource
{
    public class GodTree : MonoBehaviour
    {
        [SerializeField] private ResourceChange[] results;
        
        public void GodMode()
        {
            Save currentSave = Save.Read("Save Slot 1");
            currentSave.Delete();

            foreach (ResourceChange result in results)
            {
                result.resource.startingValue = result.amount.Value;
            }
        }
    }
}