using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsControllers
{
    public class SoundItem : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private Image _favoriteImageButton;
        [SerializeField] private Sprite _spriteChosenFavoriteButton;
        [SerializeField] private Sprite _spriteNotChosenFavoriteButton; 
        [SerializeField] private TMP_Text _description;
        private int _index;

        public void InitItem(Sprite spriteIcon, int index, string desription)
        {
            _icon.sprite =  spriteIcon;
            _index = index;
            _description.text = desription;
            UpdateFavoriteState();
        }

        public void PlaySound()
        {
            _audioSource.PlayOneShot(SoundDataContainer.SoundsData[_index].Sound);
        }

        public void SetFavoriteSound()
        {
            var isChosenAnimal = PlayerPrefs.GetInt($"{SoundDataKeys.IsFavoriteSoundKey}{_index}");

            if (isChosenAnimal == (int)TypeChosenItem.IsNotChosen)
            {
                _favoriteImageButton.sprite = _spriteChosenFavoriteButton;
                PlayerPrefs.SetInt($"{SoundDataKeys.IsFavoriteSoundKey}{_index}", (int)TypeChosenItem.IsChosen);
            }
            else if (isChosenAnimal == (int)TypeChosenItem.IsChosen)
            {
                _favoriteImageButton.sprite = _spriteNotChosenFavoriteButton;
                PlayerPrefs.SetInt($"{SoundDataKeys.IsFavoriteSoundKey}{_index}", (int)TypeChosenItem.IsNotChosen);
            }
        }

        public void UpdateFavoriteState()
        {
            var isChosenAnimal = PlayerPrefs.GetInt($"{SoundDataKeys.IsFavoriteSoundKey}{_index}");

            if (isChosenAnimal == (int)TypeChosenItem.IsNotChosen)
            {
                _favoriteImageButton.sprite = _spriteNotChosenFavoriteButton;
            }
            else if (isChosenAnimal == (int)TypeChosenItem.IsChosen)
            {
                _favoriteImageButton.sprite = _spriteChosenFavoriteButton;
            }
        }
    }
}

