using System;
using UnityEngine;
using YG;

namespace Infrastructure.Adds
{
    class AddsService : IAddsService
    {
        private bool _isShowingVideo;
        private Action _addMoney;
        public void ShowFullScreenAdd(Action onClosed = null)
        {
            YandexGame yandexGame =  GameObject.FindObjectOfType<YandexGame>();
            yandexGame._FullscreenShow();
            YandexGame.CloseFullAdEvent += onClosed;
        }

        public void ShowVideoAdd(Action onVideoEnded = null)
        {
            _addMoney = onVideoEnded;
            _isShowingVideo = true;
            YandexGame yandexGame =  GameObject.FindObjectOfType<YandexGame>();
            yandexGame._RewardedShow(0);
            YandexGame.CloseVideoEvent += ClaimReward;
        }

        private void ClaimReward()
        {
            if (_isShowingVideo)
            {
                _addMoney.Invoke();
                _isShowingVideo = false;
            }
        }
    }
}