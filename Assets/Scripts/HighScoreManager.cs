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
    double PlayerScore = PriceManager.walletValue;
    public int ID;
    int MaxScores = 10;
    public TextMeshProUGUI scoreText;
    
    
    private void Awake() {
         
       
        
    }
    
    public void Play() {
        Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
    
    public void GoToPage2() {
         StartCoroutine(coroutineA());
         
        
        
    }
   
    public void GoToMenu() {
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }
    IEnumerator coroutineA()
    {
        // wait for 1 second
        StaticAnalytics.uploadScore(MemberID.text ,PriceManager.walletValue);
        yield return new WaitForSeconds(1.0f);
         PriceManager.resetEverything();
         phoneBehavior.setCount = 0; 
         LeaderBoardManager.str = MemberID.text;
         yield return new WaitForSeconds(1.0f);
         SceneManager.LoadScene("HighscoreBoard3");
       
    }
   

   
}