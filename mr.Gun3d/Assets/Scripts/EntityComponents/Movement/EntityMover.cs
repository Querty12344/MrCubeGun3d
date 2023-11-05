using EnvironmentComponents;
using Infrastructure.Constants;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.Sound;
using UnityEngine;

namespace EntityComponents.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    public class EntityMover : MonoBehaviour, IEntityMover
    {
        private Collider _boxCollider;
        public LevelFloor ActiveFloor { get ;private set; }
        private IEntityAnimator _animator;
        private ICoroutineRunner _coroutineRunner;
        private LevelFloor _previousFloor;
        private Rigidbody _rigidbody;
        private EntitySettings _settings;
        private Jumper _jumper;
        private OnStairsMover _onStairsMover;
        private RunningMover _inRunMover;
        private Rotator _rotator;
        private PhysicsActivator _physics;
        private ISound _sound;
        public bool IsRunning { get; private set; }
        public bool IsMoving { get; set; }
        
        public void Construct(ICoroutineRunner coroutineRunner, LevelFloor floor, GameSettingsProvider settingsProvider,
            IEntityAnimator animator,ISound sound)
        {
            _sound = sound;
            _animator = animator;
            _settings = settingsProvider.EntitySettings;
            _coroutineRunner = coroutineRunner;
            _rigidbody = GetComponent<Rigidbody>();
            _boxCollider = GetComponent<Collider>();
            _jumper = gameObject.AddComponent<Jumper>();
            _jumper.Construct(_settings);
            _onStairsMover = new OnStairsMover(this,_settings,_animator);
            _inRunMover = new RunningMover(_settings,_jumper,this);
            _rotator = new Rotator();
            _physics = new PhysicsActivator(_rigidbody,_boxCollider);
            _physics.SetStartSettings();
            SetNextFloor(floor);
        }
        
        public void SetNextFloor(LevelFloor floor)
        {
            if (ActiveFloor != null) _previousFloor = ActiveFloor;
            ActiveFloor = floor;
        }
        
        public void ChangePosition()
        {
            WayPoint[] activeWay;
            if (_previousFloor == null)
                activeWay = new[]
                {
                    ActiveFloor.GetShootingPosition()
                };
            else
                activeWay = new []
                {
                    _previousFloor.GetOutWayPoint(transform.position),
                    ActiveFloor.GetInPoint(_previousFloor.GetOutWayPoint(transform.position)),
                    ActiveFloor.GetShootingPosition()
                };
            IsMoving = true;
            _animator.OnMovement();
            _sound.PlayRunSound();
            _coroutineRunner.StartCoroutine(_onStairsMover.Moving(_rigidbody,activeWay,_sound.StopRunSound));
        }

        public void RotateToTarget(Vector3 targetPosition)
        {
            _coroutineRunner.StartCoroutine(_rotator.Rotating(targetPosition,transform,_settings.RotationSpeed));
        }

        public void StartRunning(int rotationCof,Transform enemy,bool isHunter)
        {
            if (!isHunter)
            {
                _coroutineRunner.StartCoroutine(_rotator.Rotating(enemy.position + Vector3.left*100f*rotationCof,transform,_settings.RotationSpeed));
            }
            _sound.PlayRunSound();
            _jumper.Activate();
            IsMoving = false; 
            _physics.ActivatePhysics();
            IsRunning = true;
            _animator.OnMovement();
            _coroutineRunner.StartCoroutine(_inRunMover.Run(2,isHunter,rotationCof,_rigidbody,enemy));
        }

        public void StopRunning()
        {
            _sound.StopRunSound();
            _jumper.Deactivate();
            _physics.DeactivatePhysics();
            IsRunning = false;
        }

        public void JumpFromTrigger(Vector3 triggerPos)
        {
            _jumper.JumpFromTrigger();
        }

        public void SetDefault()
        {
            ActiveFloor = null;
            _previousFloor = null;
        }

        public void TeleportToActiveLevel()
        {
            transform.position = ActiveFloor.GetTeleportPosition();
        }

        public void ConnectInput(IInputService input)
        {
            input.Init(_jumper.Jump);
        }
    }
}