using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using NCalc;
using Random = UnityEngine.Random;

public class Expressions : MonoBehaviour {

    [SerializeField] private Button[] numberButton;
    [SerializeField] private Text[] numberText;

    [SerializeField] private Text resultText, operationText;

    private readonly char[] operatorSigns;

    private string inputString;

    [SerializeField] private Text debugText;
    

    private Expressions() {

        operatorSigns = new []{'(', ')', '+', '-', '*', '/'};
    }

    
    private void Awake() {
        
        resultText.text = operationText.text = "";
        inputString = null;

        debugText.text = Calculation("2-1+3*2");
    }

    #region Input
    public void OperatorInput(int num) {

        if (inputString == null && num > 0) return;

        if (inputString != null) {
            
            for (var i = 2; i < 6; i++)
                if (operatorSigns[i].Equals(inputString[inputString.Length - 1])) {

                    DeleteLastInput();
                    break;
                }

            if (inputString[inputString.Length - 1].Equals(operatorSigns[0]))
                return;
        }
        
        inputString += operatorSigns[num];
        operationText.text = inputString;
    }

    
    public void NumberInput(int num) {
        
        if (!string.IsNullOrEmpty(inputString)) {

            if ( inputString[inputString.Length - 1] > 48)
                DeleteLastInput();
        }

        numberButton[num].interactable = false;

        inputString += numberText[num].text;
        operationText.text = inputString;

        resultText.text = Calculation(inputString);
        StartCoroutine(CheckPositiveOutput());
    }
    #endregion

    
    
    
    private IEnumerator CheckPositiveOutput() {
        
        yield return new WaitForSecondsRealtime(0.5f);
        
        if (float.Parse(Calculation(inputString)) < 0 || float.Parse(Calculation(inputString)) % 1 != 0)
            ResetLevel();
    }
    
    
    private string Calculation(string localInput) {

        var expression = new Expression(localInput);
        return Mathf.Abs(float.Parse(expression.Evaluate().ToString())).ToString(CultureInfo.InvariantCulture);
        return expression.Evaluate().ToString();
    }

    
    private void DeleteLastInput() {
        
        if (inputString.Length == 0) return;
        
        for (var i = 0; i < 4; i++) {
            
            if (inputString[inputString.Length - 1].Equals(char.Parse(numberText[i].text))) 
                numberButton[i].interactable = true;
        }
        
        inputString = inputString.Remove(inputString.Length - 1);

        if (inputString.Length == 0) {
            
            resultText.text = operationText.text = inputString;
            return;
        }
        
        debugText.text = inputString.Length + ":::::::";
       
        var signBalanced = inputString;
        if (inputString[inputString.Length - 1] < 48)
            signBalanced = inputString.Trim(inputString[inputString.Length - 1]);

        resultText.text = Calculation(signBalanced);
        operationText.text = inputString;
    }


    public void ResetLevel() {

        resultText.text = operationText.text = inputString = "";
        
        for (var i = 0; i < 4; i++) 
            numberButton[i].interactable = true;
        inputString = null;
    }

    public void ShuffleNumber() {

        for (var i = 0; i < 4; i++)
            numberText[i].text = Random.Range(1, 10).ToString();
    }
    
    
}
