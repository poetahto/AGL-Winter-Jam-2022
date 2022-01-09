using System;
using System.Collections.Generic;
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

        private List<RadioMessageModel> _oneOffMessages;
        private HashSet<RadioMessageCategory> _categories;
        private RadioMessageCategory _currentCategory;
        private int _currentCategoryIndex;

        private void Start()
        {
            InitializeMessageDictionary();

            _oneOffMessages = new List<RadioMessageModel>(oneOffCategory.messages);
            
            _currentCategory = GetRandomCategory();
            _currentCategoryIndex = 0;
            
            Observable
                .Interval(TimeSpan.FromSeconds(messageFrequencySeconds))
                .Subscribe(_ => ShowNewMessage());
        }

        private void InitializeMessageDictionary()
        {
            _categories = new HashSet<RadioMessageCategory>();

            foreach (var messageCategory in categories)
                _categories.Add(messageCategory);
        }

        private RadioMessageCategory GetRandomCategory()
        {
            // todo: gracefully handle category failure - e.g. by repopulating the set

            if (_categories.Count <= 0)
            {
                Debug.Log("No random categories available! Reshuffling...");
                InitializeMessageDictionary();
            }

            int randomIndex = Random.Range(0,  _categories.Count - 1);
            int currentIndex = 0;

            foreach (var messageCategory in _categories)
            {
                if (currentIndex == randomIndex)
                    return messageCategory;
                
                currentIndex++;
            }

            return null;
        }

        private RadioMessageModel GetRandomMessage()
        {
            Debug.Log("Showing one-off message.");

            if (_oneOffMessages.Count <= 0)
            {
                Debug.Log("Ran out of one shots! Refreshing...");
                _oneOffMessages = new List<RadioMessageModel>(oneOffCategory.messages);
            }
            
            int totalMessages = _oneOffMessages.Count;
            int randomIndex = Random.Range(0, totalMessages - 1);

            var message = _oneOffMessages[randomIndex];
            _oneOffMessages.Remove(message);
            
            return message;
        }

        private RadioMessageModel GetCurrentMessage()
        {
            Debug.Log("Showing story message.");
            
            if (_currentCategoryIndex >=_currentCategory.messages.Length)
            {
                Debug.Log($"Ran out of messages in {_currentCategory}.");
                _categories.Remove(_currentCategory);
                
                _currentCategoryIndex = 0;
                _currentCategory = GetRandomCategory();
                Debug.Log($"Started new category: {_currentCategory}");
            }

            return _currentCategory.messages[_currentCategoryIndex++];
        }
        
        private void ShowNewMessage()
        {
            var messageToShow = Random.value > oneOffChance
                ? GetRandomMessage()
                : GetCurrentMessage();

            Debug.Log(messageToShow.message);
        }
    }
}