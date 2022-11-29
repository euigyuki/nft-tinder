using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;


public class HighScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public  TMP_InputField MemberID;
    float PlayerScore;
    public TextMeshProUGUI scoreText;
    private ScoreData sd;
    private int scoreCount;
    
    private void Awake() {
        PlayerScore = (float)PriceManager.walletValue;
        sd = new ScoreData();
        scoreCount = PlayerPrefs.GetInt("score_count");
        for(int i = 0; i<scoreCount; i++){
            string tempName = PlayerPrefs.GetString("scores_name_" + i);
            float tempScore = PlayerPrefs.GetFloat("scores_score_" + i);
            Score tempScoreData = new Score(tempName,tempScore);
            sd.scores.Add(tempScoreData);
        }
    }
    
    public void Play() {
        // Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
    
    public void GoToPage2() {
        //  StartCoroutine(coroutineA());

        AddScore();
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("HighscoreBoard3");
    }
   
    public void GoToMenu() {
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }
    public void AddScore(){
        Score score = new Score(MemberID.text, PlayerScore);
        sd.scores.Add(score);
        scoreCount = sd.scores.Count;

        PlayerPrefs.SetInt("score_count",scoreCount);
        PlayerPrefs.SetString("scores_name_" + (scoreCount-1),score.name);
        PlayerPrefs.SetFloat("scores_score_" + (scoreCount-1),score.score);
    }
    // IEnumerator coroutineA()
    // {
    //     // wait for 1 second
    //     // StaticAnalytics.uploadScore(MemberID.text ,PriceManager.walletValue);
    //     AddScore();
    //     yield return new WaitForSeconds(1.0f);
    //      PriceManager.resetEverything();
    //      phoneBehavior.setCount = 0; 
    //      LeaderBoardManager.str = MemberID.text;
    //      yield return new WaitForSeconds(1.0f);
    //      SceneManager.LoadScene("HighscoreBoard3");
       
    // }
   

   
}