using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using SoundsControllers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private GraphicRaycaster _loadingScreenBlockClick;
    [SerializeField] private Image _background;
    [SerializeField] private TMP_Text _loadingText;
    [SerializeField] private Image _logo;
    [SerializeField] private Image _goalkeeperImage;
    [SerializeField] private Image _ballImage;
    [SerializeField] private Image _progressBarFrameImage;
    [SerializeField] private Image _progressBarImage;
    [SerializeField] private List<Transform> _goalPosotion = new();
    [SerializeField] private UISoundsList _uiSoundsList;
    [SerializeField] private UIFavoriteSoundList _uiFavoriteSoundList;
    [SerializeField] private Transform _startPositionBall;
    private Coroutine _loadingTextAnimation;
    private const float _duretionAnimation = 0.7f;
    
    public void ChangeScene(int typeScreen)
    {
        _loadingScreenBlockClick.enabled = true;
        StartAnimationFade((TypeScreen)typeScreen);
    }

    private void StartAnimationFade(TypeScreen typeScreen)
    {
        _loadingTextAnimation = StartCoroutine(StartLoadingTextAnimation(2f));
        _loadingText.DOFade(1f, _duretionAnimation);
        _logo.DOFade(1f, _duretionAnimation);
        _goalkeeperImage.DOFade(1f, _duretionAnimation);
        _ballImage.DOFade(1f, _duretionAnimation);
        _progressBarFrameImage.DOFade(1f, _duretionAnimation);
        _progressBarImage.DOFade(1f, _duretionAnimation);

        DOTween.Sequence()
            .Append(_background.DOFade(1f, _duretionAnimation))
            .AppendInterval(1.5f)
            .AppendCallback(() => LoadScene(typeScreen))
            .AppendInterval(0.3f)
            .OnComplete(() => EndAnimationFade());

        var randomIndexGoal = Random.Range(0, _goalPosotion.Count);

        _ballImage.transform.DOMove(_goalPosotion[randomIndexGoal].position, 2.2f);
        _ballImage.transform.DORotate(new Vector3(0f, 0f, 180f), 2.2f);
        _ballImage.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 2.2f);
    }

    private void LoadScene(TypeScreen typeScreen)
    {
        if (typeScreen == TypeScreen.SoundsScreen)
            _uiSoundsList.LoadGrid();
        else if (typeScreen == TypeScreen.FavoriteSoundsScreen)
            _uiFavoriteSoundList.LoadGrid();
        else
        {
            _uiSoundsList.OffScreen();
            _uiFavoriteSoundList.OffScreen();
        }
    }

    private void EndAnimationFade()
    {
        _ballImage.DOFade(0f, _duretionAnimation);
        _goalkeeperImage.DOFade(0f, _duretionAnimation);
        _logo.DOFade(0f, _duretionAnimation);
        _loadingText.DOFade(0f, _duretionAnimation);
        _progressBarFrameImage.DOFade(0f, _duretionAnimation);
        _progressBarImage.DOFade(0f, _duretionAnimation);

        DOTween.Sequence()
            .Append(_background.DOFade(0f, _duretionAnimation))
            .AppendCallback(() => StopCoroutine(_loadingTextAnimation))
            .AppendCallback(() => _loadingScreenBlockClick.enabled = false)
            .AppendCallback(() => _progressBarImage.fillAmount = 0)
            .AppendCallback(() => _ballImage.transform.localPosition = _startPositionBall.localPosition)
            .AppendCallback(() => _ballImage.transform.localScale = Vector3.one);
    }

    private IEnumerator StartLoadingTextAnimation(float duration)
    {
        while (duration >= 0)
        {
            yield return new WaitForSeconds(0.01f);
            duration -= 0.01f;
            _progressBarImage.fillAmount += 0.005f;
        }
    } 
}
