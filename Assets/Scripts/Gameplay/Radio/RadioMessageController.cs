using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Gameplay.Radio
{
    public class RadioMessageController : MonoBehaviour
    {
        [SerializeField] private RadioMessageView radioMessageView;
        [SerializeField] private RadioMessageCategory[] categories;
        [SerializeField] private RadioMessageCategory oneOffCategory;

        [SerializeField] private float messageFrequencySeconds = 10f;
        [SerializeField] private float oneOffChance = 0.5f;
        
        private SortedSet<RadioMessageCategory> _categories;
        private RadioMessageCategory _currentCategory;
        private int _currentCategoryIndex;

        private void Start()
        {
            InitializeMessageDictionary();
            
            _currentCategory = GetRandomCategory();
            _currentCategoryIndex = 0;
            
            Observable
                .Interval(TimeSpan.FromSeconds(messageFrequencySeconds))
                .Subscribe(_ => ShowNewMessage());
        }

        private void InitializeMessageDictionary()
        {
            _categories = new SortedSet<RadioMessageCategory>();

            foreach (var messageCategory in categories)
                _categories.Add(messageCategory);
        }

        private RadioMessageCategory GetRandomCategory()
        {
            int totalCategories = _categories.Count;

            // todo: gracefully handle category failure - e.g. by repopulating the set
            
            if (totalCategories <= 0)
                throw new Exception("No random categories available!");

            int randomIndex = Random.Range(0, totalCategories - 1);
            int currentIndex = 0;

            foreach (var messageCategory in _categories)
            {
                if (currentIndex == randomIndex)
                    return messageCategory;
                
                currentIndex++;
            }

            return null;
        }

        private RadioMessageModel GetRandomMessage(RadioMessageCategory category)
        {
            if (!_categories.Contains(category) || category.messages.Length <= 0)
                throw new Exception("No random messages available!");
            
            int totalMessages = category.messages.Length;
            int randomIndex = Random.Range(0, totalMessages - 1);

            return category.messages[randomIndex];
        }

        private RadioMessageModel GetCurrentMessage()
        {
            if (_currentCategoryIndex >=_currentCategory.messages.Length)
            {
                _categories.Remove(_currentCategory);
                
                _currentCategoryIndex = 0;
                _currentCategory = GetRandomCategory();
            }

            return _currentCategory.messages[_currentCategoryIndex];
        }
        
        private void ShowNewMessage()
        {
            var messageToShow = Random.value > oneOffChance
                ? GetRandomMessage(oneOffCategory)
                : GetCurrentMessage();

            Debug.Log(messageToShow.message);
        }
    }
}