using System.Collections;
using Infrastructure.GameCore;
using UnityEngine;

namespace EnvironmentComponents
{
    public class Block : MonoBehaviour
    {
        [SerializeField]private Animator _animator;
        private ICoroutineRunner _coroutineRunner;
        private Transform _player;
        private Coroutine _fade;

        public void Init(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
            _animator.SetBool("Spawning",true);
        }

        public void Remove()
        {
            if(_fade != null)
                _coroutineRunner.StopCoroutine(_fade);
            Destroy(gameObject);
        }
        
        private IEnumerator Fade(bool appear)
        {
            _animator.SetBool("Spawning",appear);
            _animator.SetBool("Removing",!appear);
            yield return new WaitForSeconds(1);
            if (!appear)
            {
                Destroy(gameObject);
            }
        }
        public void Deactivate()
        {
            _fade = _coroutineRunner.StartCoroutine(Fade(false));
        }

        public void Activate()
        {
            _fade = _coroutineRunner.StartCoroutine(Fade(true));
        }
    }
}