using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class Credits : MonoBehaviour
    {
        [SerializeField] private string mainMenuScene;
        [SerializeField] private float creditsDuration;
        [SerializeField] private GameObject[] frames;
        [SerializeField] private Transform frameSpawn;
        [SerializeField] private Transform frameActive;
        [SerializeField] private Transform frameEnd;
        [SerializeField] private float entranceTime;
        [SerializeField] private float exitTime;

        private void Awake()
        {
            Observable.EveryUpdate()
                .Where(_ => Input.GetKeyDown(KeyCode.Escape))
                .Subscribe(_ => SceneLoader.LoadScene(mainMenuScene).Forget())
                .AddTo(this);
            
            RunCredits(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid RunCredits(CancellationToken token)
        {
            float timePerFrame = creditsDuration / frames.Length;

            foreach (GameObject frame in frames)
            {
                frame.transform.position = frameSpawn.transform.position;

                await frame.transform.DOMove(frameActive.position, entranceTime).WithCancellation(token);
                await UniTask.Delay(TimeSpan.FromSeconds(timePerFrame - entranceTime - exitTime), cancellationToken: token);
                await frame.transform.DOMove(frameEnd.position, exitTime).WithCancellation(token);
            }

            SceneLoader.LoadScene(mainMenuScene).Forget();
        }
    }
}