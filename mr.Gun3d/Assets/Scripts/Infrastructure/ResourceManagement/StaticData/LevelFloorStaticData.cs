using System;
using EnvironmentComponents;
using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(fileName = "LevelFloorData", menuName = "StaticData/LevelFloors")]
    public class LevelFloorStaticData : ScriptableObject
    {
        public GameObject Prefab;

        public LevelFloor GetLevelFloorComponent()
        {
            if (Prefab.TryGetComponent(out LevelFloor levelFloor))
                return levelFloor;
            throw new Exception("Add LevelFloor component To floor prefab");
        }
    }
}