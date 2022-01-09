using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Game.Gameplay
{
    public class TimeController : MonoBehaviour
    {
        [Range(0, 1)] 
        [SerializeField] private float time;

        [Header("Dependencies")] 
        [SerializeField] private Light2D globalLight;
        [SerializeField] private Light2D cameraLight;
        [SerializeField] private float globalDay;
        [SerializeField] private float globalNight;
        [SerializeField] private float cameraDay;
        [SerializeField] private float cameraNight;

        private void Update()
        {
            // globalLight.intensity = 
        }
    }
}