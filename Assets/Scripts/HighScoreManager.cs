using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
// using PlayFab;
// using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public  TMP_InputField MemberID;
    float PlayerScore;
    public int ID;
    int MaxScores = 10;
    public TextMeshProUGUI scoreText;
    private ScoreData sd;
    
    private void Awake() {
        PlayerScore = (float)PriceManager.walletValue;
        var json = PlayerPrefs.GetString("scores");
        sd = JsonUtility.FromJson<ScoreData>(json);
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

        var json = JsonUtility.ToJson(sd);
        PlayerPrefs.SetString("scores", json);
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