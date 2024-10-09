using UnityEngine;

namespace SoundsControllers
{
    public class UISoundsList : UIList
    {
        [SerializeField] private GameObject _soundsPageBlock;

        public override void LoadGrid()
        {
            _soundsPageBlock.SetActive(true);

            if (!_isLoaded)
            {
                InstantiatePageSounds(SoundDataContainer.SoundsData);
                InstantiateSoundItems(SoundDataContainer.SoundsData);
                InstantiateDotItems();
                InitButtons();

                _isLoaded = true;
            }
            else
            {
                foreach (var soundItem in _soundItems)
                {
                    soundItem.UpdateFavoriteState();
                }
            }
        }

        public override void OffScreen()
        {
            _soundsPageBlock.SetActive(false);
        }
    }
}

