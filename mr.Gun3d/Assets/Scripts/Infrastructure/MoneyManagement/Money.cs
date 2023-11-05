using System;
using Infrastructure.Constants;
using Infrastructure.DataSavingSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.UI.UIInfrostructure;
using UnityEngine;

namespace Infrastructure.MoneyManagement
{
    public class Money : IMoney
    {
        private readonly ISaveLoadService _saveLoadService;
        private int _money;
        private readonly OptimizationSettings _settings;

        public Money(ISaveLoadService saveLoadService,GameSettingsProvider settingsProvider)
        {
            _settings = settingsProvider.OptimizationSettings;
            _saveLoadService = saveLoadService;
            _money = _saveLoadService.LoadMoneyAmount();
        }

        public event Action<int> OnMoneyChanged;

        public void AddMoney(int amount)
        {
            _money += amount;
            OnMoneyChanged?.Invoke(_money);
            SaveMoney();
        }

        public bool TryGetMoney(int amount)
        {
            if (amount <= _money)
            {
                _money -= amount;
                OnMoneyChanged?.Invoke(_money);
                SaveMoney();
                return true;
            }

            return false;
        }

        public int GetAmount()
        {
            return _money;
        }

        public void AddVideoReward()
        {
            AddMoney(_settings.MoneyReward);
        }

        public void SaveMoney()
        {
            _saveLoadService.SaveMoneyAmount(_money);
        }
    }
}