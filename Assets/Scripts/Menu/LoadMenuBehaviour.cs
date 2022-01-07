using UnityEngine;

namespace Game
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private string mainMenuName = "Main Menu";
        
        public async void LoadMenu()
        {
            await SceneLoader.LoadScene(mainMenuName);
        }
    }
}