using UnityEngine;

namespace Infrastructure.ResourceManagement.StaticData
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "StaticData/PlayerDate")]
    public class PlayerStaticData : ScriptableObject
    {
        public GameObject Prefab;
        public float Health;
    }
}