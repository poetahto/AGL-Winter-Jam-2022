using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.UI
{
    public class LoadSceneProxy : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void Load()
        {
            SceneLoader.LoadScene(sceneName).Forget();
        }
    }
}