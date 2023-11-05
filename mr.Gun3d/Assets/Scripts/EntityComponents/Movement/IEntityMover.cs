using EnvironmentComponents;
using Infrastructure.GameCore;
using Infrastructure.InputSystem;
using Infrastructure.SettingsManagement;
using Infrastructure.Sound;
using UnityEngine;

namespace EntityComponents.Movement
{
    public interface IEntityMover
    {
        LevelFloor ActiveFloor { get;}
        bool IsMoving { get; set; }
        bool IsRunning { get; }

        public void Construct(ICoroutineRunner coroutineRunner, LevelFloor floor, GameSettingsProvider settingsProvider,
            IEntityAnimator animator,ISound sound);

        void SetNextFloor(LevelFloor floor);

        void ChangePosition();
        void RotateToTarget(Vector3 targetPosition);
        void StartRunning(int rotationCof,Transform enemy,bool isHunter);
        void StopRunning();
        void JumpFromTrigger(Vector3 triggerPos);
        void SetDefault();
        void TeleportToActiveLevel();
        void ConnectInput(IInputService input);
    }
}