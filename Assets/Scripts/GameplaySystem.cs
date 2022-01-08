using System;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

namespace Game
{
    public class GameplaySystem : MonoBehaviour
    {
        [SerializeField] private string mainMenuScene;
        [SerializeField] private LoadingScreen loadingScreen;

        public Subject<Save> OnStart;
        public Subject<Save> OnStop;

        private Save _currentSave;
        private bool _isRunning;

        private void Awake()
        {
            OnStart = new Subject<Save>().AddTo(this);
            OnStop = new Subject<Save>().AddTo(this);
        }

        public async UniTask StartGameplay(Save save)
        {
            await loadingScreen.Show();

            _isRunning = true;
            _currentSave = save;

            await SceneLoader.LoadScene(save.CurrentScene, false);
            OnStart.OnNext(save);

            await loadingScreen.Hide();
        }

        public async UniTask StopGameplay()
        {
            await loadingScreen.Show();

            OnStop.OnNext(_currentSave);
            
            _isRunning = false;
            _currentSave.Write();
            _currentSave = null;

            await SceneLoader.LoadScene(mainMenuScene, false);
            await loadingScreen.Hide();
        }
        
        public bool TryGetCurrentSave(out Save save)
        {
            save = _currentSave;
            return _isRunning && _currentSave != null;
        }
    }
}