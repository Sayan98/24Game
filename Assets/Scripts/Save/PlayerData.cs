using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData {
    
   
    public bool Arcade;
    public int Streak, Difficulty, Rank, CurrentXp, NeededXp;
    public bool[] ToggleOn = new bool[4];  
    
    public PlayerData(GetData value) {
        
        Arcade = value.Arcade;

        Streak = value.Streak;
        Difficulty = value.Difficulty;
        Rank = value.Rank;
        CurrentXp = value.CurrentXp;
        NeededXp = value.NeededXp;

        for (var i = 0; i < 4; i++)
            ToggleOn[i] = value.ToggleOn[i];
    }
    
    
}
