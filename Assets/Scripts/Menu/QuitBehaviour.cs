using UnityEngine;

namespace Game
{
    public class QuitBehaviour : MonoBehaviour
    {
        private LoadingScreen _loadingScreen;

        private void Awake()
        {
            _loadingScreen = FindObjectOfType<LoadingScreen>();
        }

        public async void Quit()
        {
            await _loadingScreen.Show();
            Application.Quit();
        }
    }   
}