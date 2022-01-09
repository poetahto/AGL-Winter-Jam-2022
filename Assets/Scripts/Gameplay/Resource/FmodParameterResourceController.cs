using FMOD.Studio;
using FMODUnity;
using Game;
using UniRx;
using UnityEngine;

namespace Gameplay.Resource
{
    public class FmodParameterResourceController : MonoBehaviour
    {
        [SerializeField] private StudioEventEmitter targetEmitter;
        [SerializeField] private string targetParameter;
        [SerializeField] private ResourceModel resource;

        private PARAMETER_DESCRIPTION _parameterDescription;
        
        private void Start()
        {
            targetEmitter.EventDescription.getParameterDescriptionByName(targetParameter, out _parameterDescription);
            resource.currentValue.Subscribe(UpdateParameter);
        }

        private void UpdateParameter(double value)
        {
            targetEmitter.SetParameter(_parameterDescription.id, (float) value);
        }
    }
}