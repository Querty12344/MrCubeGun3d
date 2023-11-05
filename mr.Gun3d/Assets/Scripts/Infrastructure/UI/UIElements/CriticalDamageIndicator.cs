using DG.Tweening;
using Infrastructure.Random;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class CriticalDamageIndicator : MonoBehaviour
    {
        [SerializeField] private Vector3 maxPositionOffset;

        private void Start()
        {
            transform.position = transform.position + Randomizer.Range(-1f, 1f) * maxPositionOffset;
            transform.DOShakePosition(0.5f, 100f);
        }
    }
}