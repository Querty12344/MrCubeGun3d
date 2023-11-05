using DG.Tweening;
using UnityEngine;

namespace CameraComponents
{
    public class CameraEffects : ICameraEffects
    {
        public void StrongShakeCamera()
        {
            var camera = GameObject.FindGameObjectWithTag("CameraEffects").transform;
            camera.DOShakePosition(0.7f).SetEase(Ease.InOutBounce);
        }

        public void LiteShakeCamera()
        {
            var camera = GameObject.FindGameObjectWithTag("CameraEffects").transform;
            camera.DOShakePosition(0.7f, 0.1f).SetEase(Ease.InOutBounce);
        }
    }
}