using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
// using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField MemberID;
    double PlayerScore = PriceManager.walletValue;
    public int ID;
    int MaxScores = 10;
    public TextMeshProUGUI scoreText;
    
    private void Awake() {
        //scoreText.text = "Enter Score..." + PriceManager.walletValue;
        // LootLockerSDKManager.StartGuestSession((response)=>{
        //     if(response.success) {
        //         Debug.Log("success");
        //     }
        //     else {
        //         Debug.Log("fail");
        //     }
        // });
    }
    public void Play() {
        Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
    public void GoToPage2() {
        // LootLockerSDKManager.SubmitScore(MemberID.text, (int)PlayerScore,ID,(response)=>{
        //     if(response.success) {
        //         Debug.Log("success");
        //     }
        //     else {
        //         Debug.Log("fail");
        //     }
        // });
        SceneManager.LoadScene("HighScorePage2");
        
    }
    
   
    public void GoToMenu() {
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }

    

   
} 