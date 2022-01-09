using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay
{
    public class SpaceProgram : MonoBehaviour
    {
        [SerializeField] private string launchScene;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                SceneLoader.LoadScene(launchScene).Forget();
        }
    }
}