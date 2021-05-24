using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCalc;

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
    }

    public void OperatorInput(int num) {

        inputString += operatorSigns[num];
        operationText.text = inputString;
        
        debugText.text = inputString;
    }

    public void NumberInput(int num) {

        numberButton[num].interactable = false;

        inputString += numberText[num].text;
        operationText.text = inputString;
        
        debugText.text = inputString;

        resultText.text = Calculation();
        
        //debugText.text = Calculation();
    }

    private string Calculation() {

        var expression = new Expression(inputString);
        return expression.Evaluate().ToString();
    }

    public void DeleteLastInput() {

        inputString = inputString.Remove(inputString.Length - 1);
        
        debugText.text = inputString;

        if (inputString.Length == 0) return;
        operationText.text = inputString;
        resultText.text = Calculation();
        
        
    }
}
