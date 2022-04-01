using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GetData : MonoBehaviour {
    
    public bool Arcade;
    public int Streak, Difficulty, Rank, CurrentXp, NeededXp;
    public bool[] ToggleOn;       //OPTIONS

   
    private void Awake() {

        var data = SaveLoad.LoadFile();
        if(data == null) {

            InitializeFile();
            return;
        }

        Arcade = data.Arcade;

        Streak = data.Streak;
        Difficulty = data.Difficulty;
        Rank = data.Rank;
        CurrentXp = data.CurrentXp;
        NeededXp = data.NeededXp;

        if(SceneManager.GetActiveScene().buildIndex.Equals(1)) return;
        for (var i = 0; i < 4; i++)
            ToggleOn[i] = data.ToggleOn[i];
    }

    private void InitializeFile() {

        Arcade = true;

        Difficulty = -1;

        Streak = Rank = 0;
        CurrentXp = 135;
        NeededXp = 150;

        for (var i = 0; i < 3; i++)
            ToggleOn[i] = true;
        ToggleOn[3] = false;

        SaveLoad.SaveFile(this);
    }
    
}
