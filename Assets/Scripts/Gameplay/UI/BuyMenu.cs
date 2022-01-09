using DG.Tweening;
using FMODUnity;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    public class BuyMenu : MonoBehaviour
    {
        [SerializeField] private Button openMenuButton;
        [SerializeField] private Button closeMenuButton;
        [SerializeField] private RectTransform menuTransform;
        [SerializeField] private StudioEventEmitter mainMusic;
        [SerializeField] private StudioEventEmitter buyMusic;
        
        [SerializeField] private float menuClosedPosition;
        [SerializeField] private float menuCloseDuration;
        
        [SerializeField] private float menuOpenPosition;
        [SerializeField] private float menuOpenDuration;

        private Tweener _currentAnim;
        private bool _isOpen;

        private void Awake()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Tab))
                .Subscribe(_ => ToggleMenu());
            
            Observable.EveryUpdate()
                .Where(_ => _isOpen)
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ => CloseMenu());
        }
        
        public void OpenMenu()
        {
            buyMusic.Play();
            mainMusic.EventInstance.setPaused(true);

            _isOpen = true;
            _currentAnim.Kill();
            _currentAnim = menuTransform.DOAnchorPosX(menuOpenPosition, menuOpenDuration);
        }
        
        public void CloseMenu()
        {
            buyMusic.Stop();
            mainMusic.EventInstance.setPaused(false);
            
            _isOpen = false;
            _currentAnim.Kill();
            _currentAnim = menuTransform.DOAnchorPosX(menuClosedPosition, menuCloseDuration);
        }

        public void ToggleMenu()
        {
            if (_isOpen)
                CloseMenu();
            
            else OpenMenu();
        }
    }
}