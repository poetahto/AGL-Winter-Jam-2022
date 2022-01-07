using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private string mainMenuName = "Main Menu";
        [SerializeField] private string gameplaySceneName = "Gameplay";
        
        public async void LoadMenu()
        {
            await SceneManager.UnloadSceneAsync(gameplaySceneName);
            await SceneLoader.LoadScene(mainMenuName);
        }
    }
}