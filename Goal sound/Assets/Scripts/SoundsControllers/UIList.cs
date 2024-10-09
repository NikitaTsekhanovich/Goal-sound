using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SoundsControllers
{
    public abstract class UIList : MonoBehaviour
    {
        [SerializeField] protected Transform _parentPages;
        [SerializeField] protected Transform _parentDots;
        [SerializeField] private PageSoundsData _pageItem;
        [SerializeField] private SoundItem _soundItem;
        [SerializeField] private DotItem _dotItem;
        [SerializeField] private Button _nextButton;
        [SerializeField] private Button _previousButton;

        protected List<PageSoundsData> _pagesSounds = new();
        protected List<DotItem> _dots = new();
        protected List<SoundItem> _soundItems = new();
        private int _currentPageIndex;
        protected bool _isLoaded;

        public abstract void LoadGrid();
        public abstract void OffScreen();

        protected void InstantiatePageSounds(List<SoundData> soundsData)
        {
            var firstPage = Instantiate(_pageItem, _parentPages);
            var countPages = Math.Ceiling((float)soundsData.Count / (float)firstPage.CounterSizePage);
            _pagesSounds.Add(firstPage);

            for (var i = 1; i < countPages; i++)
            {
                var newPage = Instantiate(_pageItem, _parentPages);
                _pagesSounds.Add(newPage);
                newPage.gameObject.SetActive(false);
            }
        }

        protected void InstantiateSoundItems(List<SoundData> soundsData)
        {
            var indexPage = 0;
            var currentPage = _pagesSounds[indexPage];

            foreach (var soundData in soundsData)
            {
                if (currentPage.IsOverflowPage())
                {
                    indexPage++;
                    currentPage = _pagesSounds[indexPage];
                    currentPage.IsOverflowPage();
                    var newItem = Instantiate(_soundItem, currentPage.transform);
                    newItem.InitItem(soundData.SpriteIcon, soundData.Index, soundData.Description);
                    _soundItems.Add(newItem);
                }
                else
                {
                    var newItem = Instantiate(_soundItem, currentPage.transform);
                    newItem.InitItem(soundData.SpriteIcon, soundData.Index, soundData.Description);
                    _soundItems.Add(newItem);
                }
            }
        }

        protected void InstantiateDotItems()
        {
            var firstDot = Instantiate(_dotItem, _parentDots);
            firstDot.ChooseDot(true);
            _dots.Add(firstDot);

            for (var i = 1; i < _pagesSounds.Count; i++)
            {
                var newDot = Instantiate(_dotItem, _parentDots);
                newDot.ChooseDot(false);
                _dots.Add(newDot);
            }
        }

        protected void InitButtons()
        {
            _previousButton.gameObject.SetActive(false);

            if (_pagesSounds.Count >= 1)
                _nextButton.gameObject.SetActive(true);
        }

        public void SwitchNextPage()
        {
            if (_currentPageIndex < _pagesSounds.Count - 1)
            {
                _currentPageIndex++;
                SwitchPage(_currentPageIndex - 1, _currentPageIndex);

                _previousButton.gameObject.SetActive(true);

                if (_currentPageIndex >= _pagesSounds.Count - 1)
                    _nextButton.gameObject.SetActive(false);
            }
        }

        public void SwitchPreviousPage()
        {
            if (_currentPageIndex > 0)
            {
                _currentPageIndex--;
                SwitchPage(_currentPageIndex + 1, _currentPageIndex);

                _nextButton.gameObject.SetActive(true);

                if (_currentPageIndex <= 0)
                    _previousButton.gameObject.SetActive(false);
            }              
        }  

        private void SwitchPage(int previousIndex, int currentIndex)
        {
            _pagesSounds[previousIndex].gameObject.SetActive(false);
            _pagesSounds[currentIndex].gameObject.SetActive(true);
            _dots[previousIndex].ChooseDot(false);
            _dots[currentIndex].ChooseDot(true);
        }
    }
}

