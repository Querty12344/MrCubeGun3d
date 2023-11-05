namespace EntityComponents.Movement
{
    public interface IEntityAnimator
    {
        void OnMovement();
        void OnWaiting();
        void Death();
    }
}