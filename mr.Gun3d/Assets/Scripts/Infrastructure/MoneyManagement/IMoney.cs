using System;

namespace Infrastructure.MoneyManagement
{
    public interface IMoney
    {
        event Action<int> OnMoneyChanged;
        void AddMoney(int amount);
        bool TryGetMoney(int amount);
        int GetAmount();
        void AddVideoReward();
        void SaveMoney();
    }
}