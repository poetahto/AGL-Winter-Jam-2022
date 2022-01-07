using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class GameplaySystem : MonoBehaviour
    {
        [SerializeField] private LoadingScreen loadingScreen;
        
        private Save _currentSave;
        private bool _isRunning;
        
        public async UniTaskVoid StartGameplay(Save save)
        {
            _isRunning = true;
            _currentSave = save;
            
            await loadingScreen.Show();
            await SceneLoader.LoadScene(save.CurrentScene);
            await loadingScreen.Hide();
        }

        public void StopGameplay()
        {
            _isRunning = false;
            _currentSave = null;
        }
        
        public bool TryGetCurrentSave(out Save save)
        {
            save = _currentSave;
            return _isRunning && _currentSave != null;
        }
    }
}