using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game
{
    public class LoadMenuBehaviour : MonoBehaviour
    {
        private GameplaySystem _gameplaySystem;

        private void Start()
        {
            _gameplaySystem = FindObjectOfType<GameplaySystem>();
        }

        public void LoadMenu()
        {
            _gameplaySystem.StopGameplay().Forget();
        }
    }
}