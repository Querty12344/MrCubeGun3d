using DG.Tweening.Core.Easing;
using Infrastructure.SettingsManagement;
using UnityEngine;
using YG;

namespace Infrastructure.Sound
{
    public class Sound : ISound
    {
        private readonly SoundData _presets;
        private AudioSource _musicAudioSource;
        private AudioSource _shootAudioSource;
        private AudioSource _runAudioSource;

        public Sound(GameSettingsProvider settingsProvider)
        {
            _presets = settingsProvider.SoundSettings;
        }

        public bool IsOn { get; private set; }

        public void PlayShootSound(float tone)
        {
            if (IsOn)
            {
                _shootAudioSource.pitch = tone;
                _shootAudioSource.PlayOneShot(_presets.Shoot,_presets.ShootVolume);
            }
        }
        public void Init()
        {
            _musicAudioSource = GameObject.Instantiate(_presets.AudioSource);
            _shootAudioSource = GameObject.Instantiate(_presets.AudioSource);
            GameObject.DontDestroyOnLoad(_musicAudioSource);
            GameObject.DontDestroyOnLoad(_shootAudioSource);
            _musicAudioSource.clip = _presets.MusicBackGround;
            _musicAudioSource.loop = true;
            _shootAudioSource.loop = false;
            _musicAudioSource.volume = _presets.Volume;
            IsOn = true;
            _musicAudioSource.Play();
        }
        public void OffSound()
        {
            IsOn = false;
            _musicAudioSource.volume = 0f;
            if (_runAudioSource != null)
            {
                _runAudioSource.volume = _presets.Volume;
            }
        }

        public void OnSound()
        {
            IsOn = true;
            _musicAudioSource.volume = _presets.Volume;
            if (_runAudioSource != null)
            {
                _runAudioSource.volume = _presets.Volume;
            }
        }

        public void PlayRunSound()
        {
            if (_runAudioSource == null)
            {
                _runAudioSource = GameObject.Instantiate(_presets.AudioSource);
                _runAudioSource.volume = IsOn ? _presets.Volume : 0f;
                _runAudioSource.loop = true;
                _runAudioSource.clip = _presets.RunSound;
            } 
            _runAudioSource.loop = true;
            _runAudioSource.Play();
        }

        public void StopRunSound()
        {
            _runAudioSource.loop = false;
            _runAudioSource.Stop();
        }
    }
}