using System;
using UnityEngine;

namespace EnvironmentComponents
{
    public class BackgroundCube : MonoBehaviour
    {
        [SerializeField] private Transform _camera;

        private void Update()
        {
            transform.position = new Vector3(_camera.position.x,_camera.position.y,0);
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
}
