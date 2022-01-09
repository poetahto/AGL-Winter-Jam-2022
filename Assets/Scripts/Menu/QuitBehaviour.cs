using UnityEngine;

namespace Game
{
    public class QuitBehaviour : MonoBehaviour
    {
        private LoadingScreen _loadingScreen;

        private void Start()
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