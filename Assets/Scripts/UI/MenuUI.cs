using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour {

    public Text debugText;

    [Header("Options")]
    [SerializeField] private Image[] _optionButtonImg;
    [SerializeField] private Image[] _optionHolderImg;
    private bool _optionToggleOn;

    
    [Header("Menu Transition")]
    [SerializeField] private Image _menuLogoImg;
    [SerializeField] private Image _levelLogoImg;
    [SerializeField] private Image _menuBgImg;
    [SerializeField] private RectTransform _menuOptionsTransition;


    [Header("Mode")]
    [SerializeField] private RectTransform _modeTypeHolderTransform;
    [SerializeField] private RectTransform _difficultyHolderTransform;
    [SerializeField] private Text[] _modeTypeHolderText;
    [SerializeField] private Image[] _modeDifficultyImg;


    private GetData _getData;


    void Awake() {
        
        _optionToggleOn = true;
    }

    void Start() {
        
        _getData = GameObject.Find("GetData").GetComponent<GetData>();

        SetupMenu();
        SetupOptions();
    }

    #region  Options
    public void ToggleOn(bool on) {

        _optionToggleOn = on;
    }

    public void ToggleIndex(int index) {

        var color = new Color(0.6588235f, 0.5411765f, 0.8784314f, 0.5f);

        _optionHolderImg[index].rectTransform.DOLocalMoveX(_optionToggleOn ? 75 : -75, 0.4f).SetEase(Ease.InOutBack);
        _optionButtonImg[index].DOColor(_optionToggleOn? color : new Color(0.5f, 0.5f, 0.5f, 0.5f), 0.6f);
        color.a = 1;
        _optionHolderImg[index].DOColor(_optionToggleOn? color : Color.grey, 0.6f);

        _getData.ToggleOn[index] = _optionToggleOn;
    }
    #endregion

    #region LoadMainLevel
    public void LoadLevelTransition() {

        SaveLoad.SaveFile(_getData);
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel() {


        _menuLogoImg.DOFade(0,0.2f);
        _levelLogoImg.rectTransform.DOAnchorPosY(-326.6f,0.5f).SetDelay(0.2f);
        _levelLogoImg.DOFade(1,0.5f).SetDelay(0.2f);
        _menuBgImg.transform.DOScale(new Vector2(10, 10), 0.5f).SetDelay(0.2f);
        //_menuOptionsTransition.DOAnchorPosY(-2800, 0.7f);
        _difficultyHolderTransform.parent.parent.DOLocalMoveY(-2800,0.7f);
        
        yield return new WaitForSecondsRealtime(0.7f);
        SceneManager.LoadScene(1);
    }
    #endregion


    #region  InitializeMenu
    private void SetupMenu() {

        _menuBgImg.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutExpo);
        _menuLogoImg.rectTransform.DOAnchorPosY(-650, 0.7f).SetEase(Ease.OutExpo);
        _menuOptionsTransition.DOAnchorPosY(0, 0.7f).SetEase(Ease.OutExpo);
    }


    private void SetupOptions() {

        for (int i = 0; i < 3; i++) {
            
            ToggleOn(_getData.ToggleOn[i]);
            ToggleIndex(i);
        }
    }
    #endregion


    #region Panel
    public void OpenPanel(CanvasGroup obj) {

        obj.DOFade(1,0.5f);
        obj.interactable = true;
        obj.blocksRaycasts = true;
    }

    public void ClosePanel(CanvasGroup obj) {

        obj.DOFade(0,0.5f);
        obj.interactable = false;
        obj.blocksRaycasts = false;

        SaveLoad.SaveFile(_getData);
    }
    #endregion


    #region Mode
    public void SelectModeType(float pos) {

        _modeTypeHolderTransform.DOAnchorPosX(pos * 223, 0.5f).SetEase(Ease.OutExpo);
        _modeTypeHolderText[0].DOColor(pos < 0 ? SelectedColor(false) : SelectedColor(true), 0.7f);
        _modeTypeHolderText[1].DOColor(pos > 0 ? SelectedColor(false) : SelectedColor(true), 0.7f);
    }

    private Color SelectedColor(bool choice) {

        return choice? new Color(0.8666667f, 0.9843137f, 0.5176471f): new Color(0.6352941f, 0.5176471f, 0.9843137f);
    }

    public void SelectDifficulty(float pos) {

        _difficultyHolderTransform.DOAnchorPosX(pos * 296.75f, 0.5f).SetEase(Ease.OutExpo);
        
        _modeDifficultyImg[0].DOColor(pos < 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);
        _modeDifficultyImg[1].DOColor(pos == 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);
        _modeDifficultyImg[2].DOColor(pos > 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);

        _getData.Difficulty = (int)pos;

    }

    public void OpenModePanel() {

        _modeTypeHolderTransform.parent.transform.DOLocalMoveX(1500, 0);
        _difficultyHolderTransform.parent.transform.DOLocalMoveX(1500, 0);
        _difficultyHolderTransform.parent.parent.GetChild(2).transform.DOLocalMoveX(1500, 0);
        _difficultyHolderTransform.parent.parent.GetChild(3).transform.DOLocalMoveX(1500, 0);

        _menuOptionsTransition.DOAnchorPosY(-2800, 0.7f);

        _modeTypeHolderTransform.parent.transform.DOLocalMoveX(0, 0.5f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.transform.DOLocalMoveX(0, 0.5f).SetDelay(0.05f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.parent.GetChild(2).transform.DOLocalMoveX(0, 0.5f).SetDelay(0.15f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.parent.GetChild(3).transform.DOLocalMoveX(0, 0.5f).SetDelay(.25f).SetEase(Ease.InOutExpo);
    }

    public void CloseModePanel() {
    
        _modeTypeHolderTransform.parent.transform.DOLocalMoveX(-1500, 0.3f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.transform.DOLocalMoveX(-1500, 0.3f).SetDelay(0.05f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.parent.GetChild(2).transform.DOLocalMoveX(-1500, 0.3f).SetDelay(0.15f).SetEase(Ease.InOutExpo);
        _difficultyHolderTransform.parent.parent.GetChild(3).transform.DOLocalMoveX(-1500, 0.3f).SetDelay(.25f).SetEase(Ease.InOutExpo);
        _menuOptionsTransition.DOAnchorPosY(0, 0.5f).SetDelay(0.55f).SetEase(Ease.OutExpo);
    }
    #endregion

}
