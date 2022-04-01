using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class MenuUI : MonoBehaviour {

    public Text debugText;
    
    [Header("Menu Transition")]
    [SerializeField] private Image _menuLogoImg;
    [SerializeField] private Image _levelLogoImg;
    [SerializeField] private Image _menuBgImg;
    [SerializeField] private RectTransform _menuOptionsTransition;
    [SerializeField] private RectTransform _creditButtonRect;
    [SerializeField] private RectTransform _navigationRect;


    [Header("Mode")]
    [SerializeField] private RectTransform _modeTypeHolderTransform;
    [SerializeField] private RectTransform _difficultyHolderTransform;
    [SerializeField] private Text[] _modeTypeHolderText;
    [SerializeField] private Image[] _modeDifficultyImg;


    [Header("Options")]
    [SerializeField] private Image[] _optionButtonImg;
    [SerializeField] private Color[] _optionButtonColor;

    [SerializeField] private Image[] _optionHolderImg;
    [SerializeField] private Color[] _optionHolderColor;

    [SerializeField] private GetData _getData;


    private void Awake() {
        
    }

    private void Start() {
        
        SetupMenu();
        SetupSettings();
    }


    #region  InitializeMenu
    private void SetupMenu() {
    //*SETUP INITIAL SCREEN
        _menuBgImg.transform.DOScale(Vector2.one, 0.5f).SetEase(Ease.OutExpo);
        _menuLogoImg.rectTransform.DOAnchorPosY(-400, 0.7f).SetEase(Ease.OutExpo);
        _menuOptionsTransition.DOAnchorPosY(0, 0.7f).SetEase(Ease.OutExpo);
        _creditButtonRect.DOAnchorPosX(-80, 0.9f).SetEase(Ease.OutExpo);
        _navigationRect.DOAnchorPosX(0, 0.7f).SetEase(Ease.OutExpo);

        _getData.Difficulty = -1;
        _getData.Arcade = true;
        SaveLoad.SaveFile(_getData); 
    }

    private void SetupSettings() {
    //*SETUP SETTINGS OPTIONS
        for (int i = 0; i < 4; i++)
            ToggleIndex(i);
    }
    #endregion



    #region  Settings
    public void ToggleOnButtonClick(int index) {
    //*GET OPTION BUTTON INPUT
        _getData.ToggleOn[index] = !_getData.ToggleOn[index]; 
        SaveLoad.SaveFile(_getData);
        ToggleIndex(index);
    }

    private void ToggleIndex(int index) {
    //*SET TOGGLE ON OR OFF FROM SAVE FILE
        _optionHolderImg[index].rectTransform.DOLocalMoveX(_getData.ToggleOn[index]? 75: -75, 0.4f).SetEase(Ease.InOutBack);
        _optionButtonImg[index].DOColor(_getData.ToggleOn[index]? _optionButtonColor[index]: new Color(0.5f, 0.5f, 0.5f, 0.5f), 0.6f);
        _optionHolderImg[index].DOColor(_getData.ToggleOn[index]? _optionHolderColor[index]: Color.grey, 0.6f);
    }
    #endregion



    #region LoadMainLevel
    public void LoadLevelTransition() {
    //*SAVE AND LOAD LEVEL
        SaveLoad.SaveFile(_getData);
        StartCoroutine(LoadLevel());
    }

    private IEnumerator LoadLevel() {
    //*LOAD ANIMATION AND SWITCH LEVEL
        _menuLogoImg.DOFade(0,0.2f);
        _levelLogoImg.rectTransform.DOAnchorPosY(-264f, 0.5f).SetDelay(0.1f);
        _levelLogoImg.DOFade(1,0.5f).SetDelay(0.2f);
        _menuBgImg.transform.DOScale(new Vector2(10, 10), 0.5f).SetDelay(0.2f);
        _difficultyHolderTransform.parent.parent.DOLocalMoveY(-2800, 0.7f);
        
        var scene = SceneManager.LoadSceneAsync(1);
        scene.allowSceneActivation = false;
        yield return new WaitForSecondsRealtime(0.7f);
        scene.allowSceneActivation = true;
    }
    #endregion
    


    #region Mode
    public void SelectModeType(float pos) {

        _modeTypeHolderTransform.DOAnchorPosX(pos * 223, 0.5f).SetEase(Ease.OutExpo);
        _modeTypeHolderText[0].DOColor(pos < 0 ? SelectedColor(false) : SelectedColor(true), 0.7f);
        _modeTypeHolderText[1].DOColor(pos > 0 ? SelectedColor(false) : SelectedColor(true), 0.7f);
        if(pos == 1)
            _getData.Arcade = true;
        else
            _getData.Arcade = false;
        
        SaveLoad.SaveFile(_getData);
    }

    public void SelectDifficulty(float pos) {

        _difficultyHolderTransform.DOAnchorPosX(pos * 296.75f, 0.5f).SetEase(Ease.OutExpo);
        
        _modeDifficultyImg[0].DOColor(pos < 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);
        _modeDifficultyImg[1].DOColor(pos == 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);
        _modeDifficultyImg[2].DOColor(pos > 0 ? SelectedColor(true) : SelectedColor(false), 0.7f);

        _getData.Difficulty = (int)pos;
        SaveLoad.SaveFile(_getData);
    }

    private Color SelectedColor(bool choice) {

        return choice? new Color(0.8039216f, 0.8627451f, 0.2235294f): new Color(0.4039216f, 0.227451f, 0.7176471f);
    }

    public void OpenModePanel() {

        _modeTypeHolderTransform.parent.transform.DOLocalMoveX(-1500, 0);
        _difficultyHolderTransform.parent.transform.DOLocalMoveX(-1500, 0);
        _difficultyHolderTransform.parent.parent.GetChild(2).transform.DOLocalMoveX(-1500, 0);
        _difficultyHolderTransform.parent.parent.GetChild(3).transform.DOLocalMoveX(-1500, 0);

        _menuOptionsTransition.DOAnchorPosY(-2800, 0.7f);
        _creditButtonRect.DOAnchorPosY(500, 0.3f);
        _navigationRect.DOAnchorPosX(-1500, 0.7f).SetEase(Ease.OutExpo);


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
        _creditButtonRect.DOAnchorPosY(0, 0.9f).SetDelay(0.45f).SetEase(Ease.OutExpo);
        _navigationRect.DOAnchorPosX(0, 0.55f).SetDelay(0.55f).SetEase(Ease.OutExpo);
    }
    #endregion

}
