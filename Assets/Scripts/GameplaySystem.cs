using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameplaySystem : MonoBehaviour
    {
        [SerializeField] private string mainMenuScene;
        [SerializeField] private LoadingScreen loadingScreen;
        
        private Save _currentSave;
        private bool _isRunning;
        
        public async UniTask StartGameplay(Save save)
        {
            _isRunning = true;
            _currentSave = save;
            
            await loadingScreen.Show();
            await SceneLoader.LoadScene(save.CurrentScene);
            await loadingScreen.Hide();
        }

        public async UniTask StopGameplay()
        {
            _isRunning = false;
            _currentSave.Write();
            _currentSave = null;

            await loadingScreen.Show();
            await SceneLoader.LoadScene(mainMenuScene);
            await loadingScreen.Hide();
        }
        
        public bool TryGetCurrentSave(out Save save)
        {
            save = _currentSave;
            return _isRunning && _currentSave != null;
        }
    }
}