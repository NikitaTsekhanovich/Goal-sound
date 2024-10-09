using UnityEngine;

namespace SoundsControllers
{
    [CreateAssetMenu(fileName = "SoundData", menuName = "Sound вata/ Sound")]
    public class SoundData : ScriptableObject
    {
        [SerializeField] private int _index;
        [SerializeField] private Sprite _spriteIcon;
        [SerializeField] private AudioClip _sound;
        [SerializeField] private string _description;

        public int Index => _index;
        public Sprite SpriteIcon => _spriteIcon;
        public AudioClip Sound => _sound;
        public string Description => _description;
    }
}

