using System;

namespace Infrastructure.InputSystem
{
    public interface IInputService
    {
        void ActivateShooting(Action shoot);
        void Init(Action jump);
        void DeactivateShooting();
    }
}