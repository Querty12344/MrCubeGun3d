using DG.Tweening.Core.Easing;

namespace Infrastructure.Sound
{
    public interface ISound
    {
        bool IsOn { get; }
        void PlayShootSound(float tone);
        void Init();
        void OffSound();
        void OnSound();
        void PlayRunSound();
        void StopRunSound();
    }
}