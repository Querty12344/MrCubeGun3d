using System;
using System.Collections.Generic;
using EntityComponents.ShootingSystem;
using UnityEngine;
using YG;

namespace Infrastructure.DataSavingSystem
{
    public class SaveLoadService : ISaveLoadService
    {
        
        public void SaveMoneyAmount(int amount)
        {
            Debug.Log("Save money");
            YandexGame.savesData.MoneyAmount = amount;
            YandexGame.SaveProgress();
        }

        public void SaveGunShopData(List<GunTypeId> boughtGuns)
        {
            List<int> boughtGunsIndexes = new List<int>();
            foreach (var gunType in boughtGuns)
            {
                boughtGunsIndexes.Add((int)gunType);
            }

            YandexGame.savesData.BoughtGuns = boughtGunsIndexes.ToArray();
            YandexGame.SaveProgress();
        }

        public int LoadMoneyAmount()
        {
            YandexGame.LoadProgress();
            Debug.Log("Load money");
            return YandexGame.savesData.MoneyAmount;
        }

        public List<GunTypeId> LoadBoughtGuns()
        {
            YandexGame.LoadProgress();
            List<GunTypeId> boughtGuns = new List<GunTypeId>();
            foreach (var index in YandexGame.savesData.BoughtGuns)
            {
                boughtGuns.Add((GunTypeId)index);
            }

            return boughtGuns;
        }
        public void SaveScore(int score)
        {
            YandexGame.NewLeaderboardScores("LB1",score);
        }
        
    }
}