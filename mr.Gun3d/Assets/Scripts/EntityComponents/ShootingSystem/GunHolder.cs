using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace EntityComponents.ShootingSystem
{
    public class GunHolder : MonoBehaviour, IGunHolder
    {
        [SerializeField] private Transform _gunTransform;
        [SerializeField] private Transform _gunTransformReference;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHandPos;
        [SerializeField] private Transform _leftHandPos;
        [SerializeField] private Transform _gunSpawn;
        private Transform _rightHandStartParent;
        private Transform _leftHandStartParent;
        private GameObject _aimLine;
        private Gun _gun;

        public void SetGun(Gun gun)
        {
            _rightHandStartParent = _rightHand.parent;
            _leftHandStartParent = _leftHand.parent;
            _gun = gun;
            _aimLine = gun.GetAimLine();
            DeactivateAimLine();
        }

        public void StartAiming()
        {
            SetDefaultTransform();
            _rightHand.SetParent(_rightHandPos);
            _leftHand.SetParent(_leftHandPos);
        }

        public void StopAiming()
        {
            SetDefaultTransform();
            _rightHand.SetParent(_rightHandStartParent);
            _leftHand.SetParent(_leftHandStartParent);
        }

        public Transform GetGunTransform()
        {
            return _gunSpawn;
        }

        public void UpdateRotation(Vector3 direction)
        {
            SetDefaultTransform();
            var zRotationAngle = Mathf.Atan(direction.y / direction.x);

            if (direction.x > 0)
            {
                _leftHandPos.rotation = Quaternion.Euler(0, 0, zRotationAngle * 50)* Quaternion.Euler(-90,25,90);
                _rightHandPos.rotation = Quaternion.Euler(0, 0, zRotationAngle * 50)* Quaternion.Euler(-90,-5,90);
            }
            else
            {
                _leftHandPos.rotation = Quaternion.Euler(0, 0, zRotationAngle * 50) * transform.rotation* Quaternion.Euler(-90,25,90);
                _rightHandPos.rotation = Quaternion.Euler(0, 0, zRotationAngle * 50) * transform.rotation* Quaternion.Euler(-90,-25,90);
            }
                
        }

        public void Shoot(Vector3 direction)
        {
            ShakeGun();
            _gun.Shoot(direction);
        }

        public void ShootOnce(Vector3 direction)
        {
            //ShakeGun();
            _gun.OneShoot(direction);
            StartCoroutine(SetDefaultTransformAfterTime());
        }
        public void ActivateAimLine()
        {
            _aimLine.SetActive(true);
        }

        public void DeactivateAimLine()
        {
            _aimLine.SetActive(false);
        }

        public float GetShootingTime()
        {
            return _gun.GetShootingTime();
        }

        private void ShakeGun()
        {
            SetDefaultTransform();
            _gunTransform.DOShakeRotation(0.7f, 10f).SetEase(Ease.Linear);
        }

        private IEnumerator SetDefaultTransformAfterTime()
        {
            yield return new WaitForSeconds(0.3f);
            SetDefaultTransform();
        }
        private void SetDefaultTransform()
        {
            if(_gunTransform == null) return;
            _gunTransform.position = _gunTransformReference.position;
            _gunTransform.rotation = _gunTransformReference.rotation;
        }
    }
}