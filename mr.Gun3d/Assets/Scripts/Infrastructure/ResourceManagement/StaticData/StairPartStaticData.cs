using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(fileName = "StairPartData", menuName = "StaticData/StairPartData")]
    public class StairPartStaticData : ScriptableObject
    {
        public GameObject Prefab;
    }
}