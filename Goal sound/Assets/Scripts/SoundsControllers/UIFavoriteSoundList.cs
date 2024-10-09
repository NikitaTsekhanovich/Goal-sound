using System.Collections.Generic;
using UnityEngine;

namespace SoundsControllers
{
    public class UIFavoriteSoundList : UIList
    {
        [SerializeField] private GameObject _favoritePagesBlock;

        public override void LoadGrid()
        {
            _favoritePagesBlock.SetActive(true);

            ClearUIList();

            var favoriteSounds = GetFavoriteAnimals();
            InstantiatePageSounds(favoriteSounds);
            InstantiateSoundItems(favoriteSounds);
            InstantiateDotItems();
            InitButtons();
        }

        private List<SoundData> GetFavoriteAnimals()
        {
            var animalsData = new List<SoundData>();

            for (var i = 0; i < SoundDataContainer.SoundsData.Count; i++)
            {
                var typeChosenItem = PlayerPrefs.GetInt($"{SoundDataKeys.IsFavoriteSoundKey}{i}");

                if (typeChosenItem == (int)TypeChosenItem.IsChosen)
                    animalsData.Add(SoundDataContainer.SoundsData[i]);
            }

            return animalsData;
        }

        private void ClearUIList()
        {
            while (_parentPages.childCount > 0) 
            {
                DestroyImmediate(_parentPages.GetChild(0).gameObject);
            }
            while (_parentDots.childCount > 0) 
            {
                DestroyImmediate(_parentDots.GetChild(0).gameObject);
            }
            _pagesSounds.Clear();
            _dots.Clear();
        }

        public override void OffScreen()
        {
            _favoritePagesBlock.SetActive(false);
        }
    }
}

