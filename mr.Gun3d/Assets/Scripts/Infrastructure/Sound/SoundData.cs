using UnityEngine;

namespace Infrastructure.Sound
{
    [CreateAssetMenu(menuName = "Create SoundData", fileName = "SoundData")]
    public class SoundData : ScriptableObject
    {
        public float Volume;
        public AudioClip Shoot;
        public AudioClip MusicBackGround;
        public AudioSource AudioSource;
        public float ShootVolume;
        public AudioClip RunSound;
    }
}