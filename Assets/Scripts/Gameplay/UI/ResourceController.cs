using System;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class ResourceController : MonoBehaviour
    {
        [Header("Dependencies")]
        [SerializeField] private ResourceView resourceView;
        [SerializeField] private ResourceModel resourceModel;
        
        [Header("Optional")]
        [SerializeField] private ValueFormatter formatter;

        private void Start()
        {
            if (resourceView.HasName)
                BindName();

            if (resourceView.HasDescription)
                BindDescription();
            
            if (resourceView.HasValue)
                BindValue();
        }

        private void BindValue()
        {
            Func<double, string> valueFormatter = GetValueFormatter();
            
            resourceModel.currentValue
                .Select(valueFormatter)
                .Subscribe(resourceView.valueDisplay.UpdateText);    
        }

        private void BindName()
        {
            resourceModel.currentName
                .Subscribe(resourceView.nameDisplay.UpdateText);    
        }

        private void BindDescription()
        {
            resourceModel.currentDescription
                .Subscribe(resourceView.descriptionDisplay.UpdateText);  
        }

        private Func<double, string> GetValueFormatter()
        {
            return formatter == null 
                ? doubleValue => $"{doubleValue};" 
                : formatter.Format;
        }
    }
}