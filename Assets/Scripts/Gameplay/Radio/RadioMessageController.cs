﻿using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
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
        [SerializeField] private float messageAutoClearSeconds = 10f;
        [SerializeField] private float oneOffChance = 0.5f;
        [SerializeField] private float radioFadeInSeconds = 1f;
        [SerializeField] private float radioFadeOutSeconds = 1f;

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

            SendMessages(gameObject.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid SendMessages(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
                await ShowNewMessage(token);
        }

        private void InitializeMessageDictionary()
        {
            _categories = new HashSet<RadioMessageCategory>();

            foreach (var messageCategory in categories)
                _categories.Add(messageCategory);
        }

        private RadioMessageCategory GetRandomCategory()
        {
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
        
        private async UniTask ShowNewMessage(CancellationToken token)
        {
            var messageToShow = Random.value > oneOffChance
                ? GetRandomMessage()
                : GetCurrentMessage();

            radioMessageView.textDisplay.UpdateText(messageToShow.message);

            radioMessageView.canvasGroup.interactable = true;
            radioMessageView.canvasGroup.blocksRaycasts = true;
            await radioMessageView.canvasGroup.DOFade(1, radioFadeInSeconds).WithCancellation(token);
            
            UniTask userInput = radioMessageView.clearButton.OnClickAsync(token);
            UniTask autoClear = UniTask.Delay(TimeSpan.FromSeconds(messageAutoClearSeconds), cancellationToken: token);
            await UniTask.WhenAny(userInput, autoClear);

            radioMessageView.canvasGroup.interactable = false;
            radioMessageView.canvasGroup.blocksRaycasts = false;
            await radioMessageView.canvasGroup.DOFade(0, radioFadeOutSeconds).WithCancellation(token);
            
            await UniTask.Delay(TimeSpan.FromSeconds(messageFrequencySeconds), cancellationToken: token);
            
            Debug.Log(messageToShow.message);
        }
    }
}