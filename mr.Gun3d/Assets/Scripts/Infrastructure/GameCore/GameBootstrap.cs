using Infrastructure.GameCore.GameStates;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.GameCore
{
    public class GameBootstrap : MonoBehaviour, ICoroutineRunner
    {
        private IGameStateMachine _stateMachine;
        [SerializeField] public GameObject _indicator;
        private void Start()
        {
            DontDestroyOnLoad(this);
            _stateMachine.EnterState<BootstrapState>();
        }

        [Inject]
        public void Construct(IGameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }
    }
}