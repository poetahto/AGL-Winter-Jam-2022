using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameplayInitializer : MonoBehaviour
    {
        [SerializeField] private string gameplayPersistentScene = "Gameplay";
        [SerializeField] private UnityEvent<Save> onInitialize;

        public async UniTask Initialize(Save currentSave)
        {
            await InitializeGameplayPersistent(currentSave);
            onInitialize.Invoke(currentSave);

            Debug.Log($"Initialized game from \"{currentSave.Name}\".");
        }

        private async UniTask InitializeGameplayPersistent(Save currentSave)
        {
            if (SceneManager.GetSceneByName(gameplayPersistentScene).IsValid())
                return;
            
            await SceneManager.LoadSceneAsync(gameplayPersistentScene, LoadSceneMode.Additive);
            FindObjectOfType<GameplayCurrentSave>().CurrentSave = currentSave;
        }
    }
}