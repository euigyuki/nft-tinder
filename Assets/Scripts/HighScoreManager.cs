using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using PlayFab;
using PlayFab.ClientModels;
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
        
       
        
    }
    IEnumerator ExampleCoroutine()
    {
        
         Login();
         yield return new WaitForSeconds(1);
         SetUserDisplayName(MemberID.text);
         yield return new WaitForSeconds(1);
         SendLeaderboard((int)PriceManager.walletValue);
         yield return new WaitForSeconds(1);
         PriceManager.resetEverything();
         phoneBehavior.setCount = 0;
         SceneManager.LoadScene("HighScorePage2");
    }
    public void Play() {
        Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
     public static void SetUserDisplayName(string name)
    {
        

        PlayFabClientAPI.UpdateUserTitleDisplayName(
            // Request
            new UpdateUserTitleDisplayNameRequest
            {
                DisplayName = name
            },
            // Success
            (UpdateUserTitleDisplayNameResult result) =>
            {
                Debug.Log("UpdateUserTitleDisplayName completed.");
            },
            // Failure
            (PlayFabError error) =>
            {
                Debug.LogError("UpdateUserTitleDisplayName failed.");
                Debug.LogError(error.GenerateErrorReport());
            }
            );
    }
    public void Login() {
      
        
        var request = new LoginWithCustomIDRequest{
            CustomId = MemberID.ToString(), CreateAccount = true
        };
        PlayFabClientAPI.LoginWithCustomID(request,OnSuccess,OnError);
    }
    void OnSuccess(LoginResult result) {
        Debug.Log("Successful Login/Account Create! ");
    }
    void OnError(PlayFabError error) {
        Debug.Log("Error while login in /ceating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    
    public void OnDisplayNameUpdate(UpdateUserTitleDisplayNameRequest result) {
        Debug.Log("Updated display name");
        
    }
    public void SendLeaderboard(int score) {
        var request = new UpdatePlayerStatisticsRequest{
            Statistics = new List<StatisticUpdate>{
                new StatisticUpdate{
                    StatisticName = "highScore",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request,OnLeaderboardUpdate,OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result) {
        Debug.Log("Successful Leaderboard sent");
    }

    public void GoToPage2() {
        StartCoroutine(ExampleCoroutine());
        
        
    }
    
   
    public void GoToMenu() {
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }

    

   
} 