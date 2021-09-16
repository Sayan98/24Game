using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using NCalc;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Expressions : MonoBehaviour {

    [SerializeField] private Button[] _numberButton;
    [SerializeField] private Text[] _numberText;
    [SerializeField] private Text _resultText, _operationText;
    [SerializeField] private Transform[] _operatorTransform;
    [SerializeField] private Transform[] _miscellaneousButton;

    private readonly char[] operatorSigns;
    private string _inputString, _passedNumbers;

    private string[] _letters;
    private string _hintSolution;

    private GetData _getData;

    public Text debugText;
    

    private Expressions() {

        operatorSigns = new []{'+', '-', '*', '/'};
    }

    
    private void Awake() {
        
        _resultText.text = _operationText.text = _inputString = _passedNumbers = null;
        _letters = new string[4];
    }

    void Start() {
        
        _getData = GameObject.Find("GetData").GetComponent<GetData>();

        StartCoroutine(SetupLevel());
        StartCoroutine(ShuffleNumber(1.75f));
    }

    #region Input
    public void OperatorInput(int num) {

        if (_inputString == null) return;

        if (_inputString != null) {
            
            for (var i = 0; i < 4; i++)
                if (operatorSigns[i].Equals(_inputString[_inputString.Length - 1])) {

                    DeleteLastInput();
                    break;
                }

            /*if (_inputString[_inputString.Length - 1].Equals(operatorSigns))
                return;*/
        }
        
        _inputString += operatorSigns[num];
        _operationText.text = _inputString;
        Debug.Log("reached");
    }

    
    public void NumberInput(int num) {
        
        if (!string.IsNullOrEmpty(_inputString)) {

            if ( _inputString[_inputString.Length - 1] > 48)
                DeleteLastInput();
        }

        _numberButton[num].interactable = false;

        _passedNumbers += num + "";
        _inputString += _numberText[num].text;
        _operationText.text = _inputString;

        /*var count = 0;
        var contain = inputString;
        for (int i = 0; i < contain.Length; i++)
        {
            if(contain[i] >= 1 && contain[i] < 10)
                count++;
            
            if(count == 2)
                resultText.text = Calculation(contain.Substring(0, i));
            

        }*/

        var contain = _inputString;
        Calculate(true);
        _resultText.text = _inputString;
        _inputString = contain;
    
        StartCoroutine(CheckOutput());

    }
    #endregion Input

    
    private IEnumerator CheckOutput() {
        
        var allUsed = true;

        for (int i = 0; i < 4; i++)
            if(_numberButton[i].interactable) {
             
                allUsed = false;
                break;
            }

        if(allUsed && _resultText.text == "24" || _resultText.text == "-24" && allUsed) {
                
            _resultText.text = "24";
            yield return new WaitForSecondsRealtime(0.5f);
            ResetLevel(true);
        }
        else if(allUsed) {

                yield return new WaitForSecondsRealtime(0.5f);
                ResetLevel(false);
        }
    }
    
    public void Calculate(bool update) {

        if (_passedNumbers.Length < 1) return;

        var input = _inputString;
        var output = (float)char.GetNumericValue(input[0]);
        
        _resultText.text = output.ToString();

        for (int i = 0; i < input.Length; i=i+2)
            if(i+2 < input.Length && !input[i+2].Equals(operatorSigns)) {
            
                output = float.Parse(Calculation(output + "" + input[i+1] + "" + input[i+2]));
                _resultText.text = output.ToString();
//                Debug.Log((Convert.ToSingle(input[i]) + Convert.ToSingle(input[i+1]) + Convert.ToSingle(input[i+2])));
               // debugText.text = _resultText.text;
            }

        if(update)
            _inputString = output.ToString();
    }

    private string Calculation(string localInput) {

        var expression = new Expression(localInput);
        //return Mathf.Abs(float.Parse(expression.Evaluate().ToString())).ToString(CultureInfo.InvariantCulture);
        return expression.Evaluate().ToString();
    }

    
    private void DeleteLastInput() {
        
        if (_inputString == null) return;
        
        /*for (var i = 0; i < 4; i++) {
            
            if (_inputString[_inputString.Length - 1].Equals(char.Parse(_numberText[i].text)) && !_numberButton[i].interactable) {
                
                Debug.Log(i + "" + _numberButton[i].interactable);
                _numberButton[i].interactable = true;
                break;
            }
        }*/

        if((int)_inputString[_inputString.Length - 1] > 48) {

            _numberButton[(int)char.GetNumericValue(_passedNumbers[_passedNumbers.Length - 1])].interactable = true;
            Debug.Log("" + _passedNumbers[_passedNumbers.Length - 1]);
            _passedNumbers = _passedNumbers.Remove(_passedNumbers.Length - 1, 1);
        }
        
        _inputString = _inputString.Remove(_inputString.Length - 1, 1);
        _operationText.text = _inputString;

        if (_inputString.Length == 0) {
            
            _resultText.text = _operationText.text = _inputString;
            return;
        }
    }


    public void ResetLevel(bool shuffle) {

        _resultText.text = _operationText.text = _inputString = "";
        
        for (var i = 0; i < 4; i++) 
            _numberButton[i].interactable = true;
        _inputString = _passedNumbers = null;

        if(shuffle)
            StartCoroutine(ShuffleNumber(0));
    }


    private IEnumerator ShuffleNumber(float t) {

        yield return new WaitForSecondsRealtime(t);

        _hintSolution = GenerateSolution();

        for (var i = 0; i < 4; i++) {
        
            _numberButton[i].transform.DORotate(new Vector2(0,90), 0.25f);
            yield return new WaitForSecondsRealtime(0.25f);
            _numberText[i].text = _letters[i];
            _numberButton[i].transform.DORotate(Vector2.zero, 0.25f);
        }
    }

    private IEnumerator SetupLevel() {
        
        yield return null;
        var delayTime = 0.0f;

        for (int i = 0; i < 4; i++) {
            
            _numberButton[i].transform.DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(delayTime);
            _operatorTransform[i].DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(delayTime + 0.8f);
            delayTime += 0.2f;
        }

        for (int i = 0; i < 4; i++)
            _miscellaneousButton[i].DOScale(Vector2.one, 0.25f);
    }
    
    public void ShowHint() {

       debugText.text = _hintSolution;
    }


    private string GenerateSolution() {

        var min = 1;
        var max = 10;

        var str = Random.Range(min, max).ToString();
        str += Random.Range(min, max);
        str += Random.Range(min, max);
        str += Random.Range(min, max);

        return CheckPossibleOutput(str);
    }


    private float Operation (int f, float m, float n){

        if (f == 0) return(m + n);
        if (f == 1) return(m - n);
        if (f == 2) return(m * n);
        if (f == 3) return(m / n);
        
        return 0;
    }


    private string CheckPossibleOutput (string digits) {

        var n = digits[0];
        string[] b = new string[9];
    
        b[1] = digits[0].ToString();
        b[2] = digits[1].ToString();
        b[4] = digits[2].ToString();
        b[8] = digits[3].ToString();

        var c = 0;
        digits = null;

        for (var i = 1; i <= 8; i*=2)
        for (var j = 1; j <= 8; j*=2)
        for (var k = 1; k <= 8; k*=2)
        for (var l = 1; l <= 8; l*=2) {
            
            if(digits != null) break;
            if ((i|j|k|l) != 0xf) continue;

            for (var f1 = 0; f1 <= 3; f1++)
            for (var f2 = 0; f2 <= 3; f2++)
            for (var f3 = 0; f3 <= 3; f3++) {
                
                if(digits != null) break;

                var m = Operation(f3, Operation(f2, Operation(f1, float.Parse(b[i]), float.Parse(b[j])), float.Parse(b[k])), float.Parse(b[l]));
                if (Mathf.Abs(m-24) < 1e-5) {
                                
                    //digits = digits + "((" + b[i] + operatorSigns[f1] + b[j] + ")" + operatorSigns[f2] + b[k] + ")" + operatorSigns[f3] + b[l];
                    digits = b[i] + operatorSigns[f1] + b[j] + operatorSigns[f2] + b[k] + operatorSigns[f3] + b[l];
                    if ((n != 0) && (++c >= n)) return(digits);
                    break;
                }

                m = Operation(f1, float.Parse(b[i]), Operation(f3, Operation(f2, float.Parse(b[j]), float.Parse(b[k])), float.Parse(b[l])));

                if (Mathf.Abs(m-24) < 1e-5) {
                                
                    //digits = digits + b[i] + operatorSigns[f1] + "((" + b[j] + operatorSigns[f2] + b[k] + ")" + operatorSigns[f3] + b[l] + ")";
                    digits = b[j] + operatorSigns[f2] + b[k] +  operatorSigns[f3] + b[l] + operatorSigns[f1] + b[i];
                    if ((n != 0) && (++c >= n)) return(digits);
                    break;
                }

                m = Operation(f3, Operation(f1, float.Parse(b[i]), Operation(f2, float.Parse(b[j]), float.Parse(b[k]))), float.Parse(b[l]));
            
                if (Mathf.Abs(m-24) < 1e-5) {
                
                    //digits = digits + "(" + b[i] + operatorSigns[f1] + "(" + b[j] + operatorSigns[f2] + b[k] + "))" + operatorSigns[f3] + b[l];
                    digits = b[j] + operatorSigns[f2] + b[k] + operatorSigns[f1] + b[i] + operatorSigns[f3] + b[l];
                    if ((n != 0) && (++c >= n)) return(digits);
                    break;
                }

                m = Operation(f1, float.Parse(b[i]), Operation(f2, float.Parse(b[j]), Operation(f3, float.Parse(b[k]), float.Parse(b[l]))));
            
                if (Mathf.Abs(m-24) < 1e-5) {

                    //digits = digits + b[i] + disoper[f1] + "(" + b[j] + disoper[f2] + "(" + b[k] + disoper[f3] + b[l] + "))";
                    digits = b[k] + operatorSigns[f3] + b[l] + operatorSigns[f2] + b[j] + operatorSigns[f1] + b[i];
                    if ((n != 0) && (++c >= n)) return(digits);
                    break;
                }
            }
        }

        _letters[0] = b[1];
        _letters[1] = b[2];
        _letters[2] = b[4];
        _letters[3] = b[8];

        if(digits == null)
            digits = GenerateSolution();


        var multi = false;
        var div= false;

        var cc = 0;

        for (int i = 0; i < 7; i++) {
            
            if(digits[i] < 48)
                if(digits[i] == '*')
                    multi = true;
                else if(digits[i] == '/') {
                    
                    div = true;
                    cc++;
                }
        }

        if(_getData.Difficulty < 0 && multi && !div || _getData.Difficulty > 0 && multi && div && cc == 1 ||
                                                        _getData.Difficulty == 0 && !multi && div)
            Debug.Log("all good");
        else
            digits = GenerateSolution();


            return(digits);

    }


    
}
