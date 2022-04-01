using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour {

    public GameObject pauseScreen;
    public GameObject timer;
    public Text timerText;
    public int time = 60;
    public bool useTimer = true;

    public GameObject ResultScreen;
    public Text ResultText;

    public GetData _getdata;
    public Expressions _expressions;

    private void Start() {
        
        if(!_getdata.Arcade) {
            
            timer.SetActive(false);
            useTimer = false;
            return;
        }
        StartCoroutine(CountDown());
    }

    private IEnumerator CountDown() {

        yield return new WaitForSecondsRealtime(1);
        time--;
        timerText.text = time.ToString();
        
        if(time == 0) {

            ResultScreen.SetActive(true);
            ResultText.text = "Reached Solution: " + _expressions.sol + "\n"+ "Hints Used: " + _expressions.hintstreak +"\n" + "Highest Streak: 0";
        }
        else
            StartCoroutine(CountDown());
    }


    public void PauseScene() {

        pauseScreen.SetActive(true);
        Time.timeScale = 0;
    }



    public void Reseume() {

        pauseScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void RestartLevel() {

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MenuScene() {

        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
