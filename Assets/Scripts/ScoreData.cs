using System;
using System.Collections.Generic;
// using UnityEngine;

[Serializable]
public class ScoreData
{
    public List<Score> scores;

    public ScoreData(){
        scores = new List<Score>();
    }
}

[Serializable]
public class Score{
    public string name;
    public float score; 

    public Score(string name, float score){
        this.name = name;
        this.score = score;
    }
}