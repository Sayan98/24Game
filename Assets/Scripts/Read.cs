using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

public class Read : MonoBehaviour {
   
    public Text text;
    private void Start() {
        
        var stt = File.ReadAllLines("Assets/Resources/3000 word list.txt");
        text.text = stt[6];
        
    }
}
