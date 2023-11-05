namespace Infrastructure.GameCore
{
    public interface IGame
    {
        bool IsRuning { get; set; }
        void Play();
        void Exit();
        void Restart();
    }
}