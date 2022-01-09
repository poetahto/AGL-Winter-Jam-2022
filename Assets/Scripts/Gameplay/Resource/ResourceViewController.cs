using System;
using UniRx;
using UnityEngine;

namespace Game.UI
{
    public class ResourceViewController : MonoBehaviour
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
                .Subscribe(resourceView.valueDisplay.UpdateText).AddTo(this);    
        }

        private void BindName()
        {
            resourceModel.currentName
                .Subscribe(resourceView.nameDisplay.UpdateText).AddTo(this);    
        }

        private void BindDescription()
        {
            resourceModel.currentDescription
                .Subscribe(resourceView.descriptionDisplay.UpdateText).AddTo(this);  
        }

        private Func<double, string> GetValueFormatter()
        {
            return formatter == null 
                ? doubleValue => $"{doubleValue};" 
                : formatter.Format;
        }
    }
}