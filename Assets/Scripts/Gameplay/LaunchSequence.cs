using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Gameplay
{
    public class LaunchSequence : MonoBehaviour
    {
        [SerializeField] private float totalDuration;
        [SerializeField] private float launchDuration;
        [SerializeField] private string creditsScene;
        [SerializeField] private Transform rocket;
        [SerializeField] private Transform rocketEnd;
        [SerializeField] private float rocketDuration;
        [SerializeField] private RectTransform earth;
        
        private void Awake()
        {
            LaunchRocket().Forget();
            MoveToCredits().Forget();
        }

        private async UniTaskVoid LaunchRocket()
        {
            await earth.DOScale(1, launchDuration);
            await rocket.DOMove(rocketEnd.position, rocketDuration);
        }

        private async UniTaskVoid MoveToCredits()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(totalDuration));
            SceneLoader.LoadScene(creditsScene).Forget();
        }
    }
}