using System;
using System.Collections;
using Infrastructure.EntitiesManagement;
using Infrastructure.GameCore;
using Infrastructure.SettingsManagement;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Screen = UnityEngine.Device.Screen;

namespace CameraComponents
{
    public class CameraFollow : ICameraFollow
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ILevelEnemiesHolder _enemiesHolder;
        private readonly CameraSettings _settings;
        private Transform _camera;
        private Coroutine _followCoroutine;
        private float _startXPosition;
        private float _startZPosition;


        public CameraFollow(ICoroutineRunner coroutineRunner, ILevelEnemiesHolder enemiesHolder,
            GameSettingsProvider settings)
        {
            _coroutineRunner = coroutineRunner;
            _enemiesHolder = enemiesHolder;
            _settings = settings.CameraSettings;
        }

        public void Activate()
        {
            _camera = Camera.main.transform;
            _followCoroutine = _coroutineRunner.StartCoroutine(Follow());
        }

        public void Deactivate()
        {
            if (_followCoroutine != null)
                _coroutineRunner.StopCoroutine(_followCoroutine);
            _camera.position = new Vector3(_startXPosition, 0, _startZPosition);
        }

        private IEnumerator Follow()
        {
            _startXPosition = _camera.position.x;
            var effectYPos = _enemiesHolder.Player.transform.position.y + _settings.YOffset * 3f;
            var effectZPos = _enemiesHolder.Player.transform.position.z;
            _camera.position = new Vector3(_startXPosition, effectYPos, effectZPos);

            while (true)
            {
                if (_enemiesHolder.ActiveEnemy != null)
                {
                    Vector3 enemyPos = _enemiesHolder.ActiveEnemy.transform.position;
                    Vector3 playerPos = _enemiesHolder.Player.transform.position;

                    float zOffset;
                    float xOffset;
                    float rotationCof = 1f;
                    float screenAspect = Camera.main.aspect;
                    if (screenAspect < 0.78f)
                    {
                        zOffset = _settings.MobileZOffset;
                        xOffset = 0f;
                        rotationCof = 0f;
                    }
                    else if (screenAspect < 1.5f)
                    {
                        xOffset = _settings.XOffset;
                        zOffset = _settings.PcZOffset;
                    }
                    else
                    {
                        xOffset = _settings.XOffset;
                        zOffset = _settings.UltraWidthZOffset;
                    }

                    float zEditionalOffset = -Mathf.Abs(enemyPos.x - playerPos.x) * _settings.ZOffsetCof;
                    float yPos = playerPos.y + _settings.YOffset;
                    float xPos = 0.5f * (enemyPos.x + playerPos.x)-xOffset* (enemyPos - playerPos).normalized.x;
                    float zPos = playerPos.z + zOffset+ zEditionalOffset;
                    Quaternion nextRotation;
                    if (enemyPos.x - playerPos.x > 0)
                    {
                        nextRotation = Quaternion.Euler(_settings.Rotation*rotationCof);
                    }
                    else
                    {
                        nextRotation = Quaternion.Euler(_settings.Rotation.x*rotationCof,-_settings.Rotation.y*rotationCof,_settings.Rotation.z*rotationCof);
                    }
                    Vector3 nextPos = new Vector3(xPos, yPos, zPos);
                    _camera.position = Vector3.Lerp(_camera.position, nextPos, _settings.Speed * Time.deltaTime);
                    _camera.rotation = Quaternion.Lerp(_camera.rotation, nextRotation,_settings.RotationSpeed);
                }

                yield return new WaitForFixedUpdate();
            }
        }
        
    }
}