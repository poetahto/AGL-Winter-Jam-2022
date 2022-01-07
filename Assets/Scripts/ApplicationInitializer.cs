using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationInitializer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private string firstScene;
    [SerializeField] private string applicationScene;

    private async void Start()
    {
        if (SceneManager.GetSceneByName(applicationScene).IsValid() == false)
            await SceneManager.LoadSceneAsync(applicationScene, LoadSceneMode.Additive);
        
        await SceneLoader.LoadScene(firstScene);
    }
}