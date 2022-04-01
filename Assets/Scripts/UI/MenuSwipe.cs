using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MenuSwipe : MonoBehaviour {
    
    [SerializeField] private ScrollRect slider;
    [SerializeField] private RectTransform panelGrid;
    private readonly int swipeThreshold, page;
	private readonly float swipeTime, dragTime;
    private bool drag, lerp;
    
    
    private MenuSwipe() {
        
        swipeThreshold = 50;
        swipeTime = 0.5f;
    }

    private void Start() {
        
        slider.horizontalNormalizedPosition = 0;
        enabled = false;
    }

    private void Update() {
        
        Debug.Log("ss");
    }

    public void bb() {

        enabled = true;
    }

    public void aa() {
        enabled = false;
    }

}
