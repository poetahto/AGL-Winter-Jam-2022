using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private string mainMenuName = "Main Menu";
        
        public void LoadMenu()
        {
            SceneManager.LoadScene(mainMenuName);
        }
    }
}