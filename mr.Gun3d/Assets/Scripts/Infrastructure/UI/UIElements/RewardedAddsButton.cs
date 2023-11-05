using Infrastructure.Adds;
using Infrastructure.MoneyManagement;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class RewardedAddsButton:MonoBehaviour
    {
        private IAddsService _addsService;
        private IMoney _money;

        public void Construct(IMoney money,IAddsService addsService)
        {
            _money = money;
            _addsService = addsService;
        }

        public void ShowRewardedAdds()
        {
             _addsService.ShowVideoAdd(_money.AddVideoReward);
        }
    }
}