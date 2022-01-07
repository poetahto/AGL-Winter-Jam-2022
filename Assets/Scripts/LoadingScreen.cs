using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

/// <summary>
/// A persistent image used to hide the screen when things are loading.
/// </summary>

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private float introDurationSeconds = 1f;
    [SerializeField] private float outroDurationSeconds = 1f;
    [SerializeField] private CanvasGroup loadingCanvas;

    public async UniTask Show()
    {
        loadingCanvas.interactable = true;
        loadingCanvas.blocksRaycasts = true;
        
        await loadingCanvas.DOFade(1, introDurationSeconds);
    }

    public async UniTask Hide()
    {
        await loadingCanvas.DOFade(0, outroDurationSeconds);
        
        loadingCanvas.interactable = false;
        loadingCanvas.blocksRaycasts = false;
    }
}