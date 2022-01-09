using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

namespace Game.Gameplay
{
    public class TimeController : MonoBehaviour
    {
        [Range(0, 1)] 
        [SerializeField] private float time;

        [SerializeField] private float dayDuration;
        [SerializeField] private float nightDuration;
        [SerializeField] private float transitionDuration;
        [SerializeField] private Gradient globalGradient;
        [SerializeField] private AnimationCurve cameraLightCurve;
        [SerializeField] private bool autoProgress = true;

        [Header("Dependencies")] 
        [SerializeField] private Light2D globalLight;
        [SerializeField] private Light2D cameraLight;
        [SerializeField] private float globalDay;
        [SerializeField] private float globalNight;

        public UnityEvent onNewDay;

        private void Awake()
        {
            if (autoProgress)
                DayNightCycle(gameObject.GetCancellationTokenOnDestroy());
        }

        private void Update()
        {
            globalLight.intensity = Mathf.Lerp(globalDay, globalNight, time);
            globalLight.color = globalGradient.Evaluate(time);
            cameraLight.intensity = cameraLightCurve.Evaluate(time);
        }

        private async void DayNightCycle(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(dayDuration), cancellationToken: token);
                await DOTween.To(value => time = value, 0, 1, transitionDuration).WithCancellation(token);
                await UniTask.Delay(TimeSpan.FromSeconds(nightDuration), cancellationToken: token);
                await DOTween.To(value => time = value, 1, 0, transitionDuration).WithCancellation(token);
            }
        }
    }
}