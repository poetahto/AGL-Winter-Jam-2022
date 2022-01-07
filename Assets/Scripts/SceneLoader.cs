using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to transition between additively loaded scenes for the game.
/// </summary>

public class SceneLoader : MonoBehaviour
{
    public static async UniTask LoadScene(string targetScene, bool useLoadingScreen = true)
    {
        LoadingScreen screen = FindObjectOfType<LoadingScreen>();

        if (useLoadingScreen)
            await screen.Show();
        
        await SwitchToScene(SceneManager.GetActiveScene().path, targetScene);
        
        if (useLoadingScreen)
            await screen.Hide();
    }

    private static async UniTask SwitchToScene(string oldScene, string newScene)
    {
        DisableSingletonComponents();

        await SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        await SceneManager.UnloadSceneAsync(oldScene);

        var activeSceneName = SceneManager.GetSceneByName(newScene);
        var activeScenePath = SceneManager.GetSceneByPath(newScene);
        var activeScene = activeSceneName.IsValid() ? activeSceneName : activeScenePath;
        
        SceneManager.SetActiveScene(activeScene);
    }
    
    private static void DisableSingletonComponents()
    {
        // There should only be one of these components ever active at one time, so we clean them up here.
        
        foreach (var eventSystem in FindObjectsOfType<EventSystem>())
            eventSystem.enabled = false;
        
        foreach (var audioListener in FindObjectsOfType<AudioListener>())
            audioListener.enabled = false;
    }
}