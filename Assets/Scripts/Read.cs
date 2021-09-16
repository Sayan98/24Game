using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;


public class Read : MonoBehaviour {

      
   // public Text text;
   public Image[] image;
   public Transform[] transforms;
   public Text[] ef;
    private void Start() {
       
       image[0].transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.OutExpo).SetDelay(0);
       image[1].transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.OutExpo).SetDelay(0.2f);
       image[2].transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.OutExpo).SetDelay(0.4f);
       image[3].transform.DOScale(new Vector2(1, 1), 1).SetEase(Ease.OutExpo).SetDelay(0.6f);

       transforms[0].transform.DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(0.8f);
       transforms[1].transform.DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(1f);
       transforms[2].transform.DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(1.2f);
       transforms[3].transform.DOScale(Vector2.one, 1).SetEase(Ease.OutExpo).SetDelay(1.4f);

      
      StartCoroutine(e());
       // var stt = File.ReadAllLines("Assets/Resources/3000 word list.txt");
      //  text.text = stt[6];
    }

   IEnumerator e() {
      
      image[0].transform.DORotate(new Vector3(0,90,0), 0.25f).SetDelay(2);
      yield return new WaitForSecondsRealtime(2.25f);
      ef[0].text = Random.Range(1,10).ToString();
      image[0].transform.DORotate(new Vector3(0,0,0), 0.25f);

      image[1].transform.DORotate(new Vector3(0,90,0), 0.25f);
      yield return new WaitForSecondsRealtime(0.25f);
      ef[1].text = Random.Range(1,10).ToString();
      image[1].transform.DORotate(new Vector3(0,0,0), 0.25f);

      image[2].transform.DORotate(new Vector3(0,90,0), 0.25f);
      yield return new WaitForSecondsRealtime(0.25f);
      ef[2].text = Random.Range(1,10).ToString();
      image[2].transform.DORotate(new Vector3(0,0,0), 0.25f);

      image[3].transform.DORotate(new Vector3(0,90,0), 0.25f);
      yield return new WaitForSecondsRealtime(0.25f);
      ef[3].text = Random.Range(1,10).ToString();
      image[3].transform.DORotate(new Vector3(0,0,0), 0.25f);
   }

}
