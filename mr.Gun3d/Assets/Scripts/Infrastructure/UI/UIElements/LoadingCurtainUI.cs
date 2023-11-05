using System;
using UnityEngine;

namespace Infrastructure.UI.UIElements
{
    public class LoadingCurtainUI:MonoBehaviour
    {
        [SerializeField] private float _lifeTime;
        private void Awake()
        {
            DontDestroyOnLoad(this.gameObject);
            Destroy(gameObject,_lifeTime);
        }
    }
}