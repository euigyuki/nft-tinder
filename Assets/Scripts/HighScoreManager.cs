using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_InputField MemberID,PlayerScore;
    public int ID;
    int MaxScores = 3;
    public TextMeshProUGUI[] Entries;
    public TextMeshProUGUI scoreText;
    
    private void start() {
        scoreText.text = "Enter Score..." + PriceManager.walletValue;
        LootLockerSDKManager.StartGuestSession((response)=>{
            if(response.success) {
                Debug.Log("success");
            }
            else {
                Debug.Log("fail");
            }
        });
    }

    public void ShowScores() {
        LootLockerSDKManager.GetScoreList(ID,MaxScores,(response)=>{
            if(response.success) {
                LootLockerLeaderboardMember[] scores = response.items;
                for(int i =0;i<scores.Length;i++) {
                    Entries[i].text = (scores[i].rank +".place is  "+ scores[i].member_id +"\r\n" + " Wallet Value = $" +scores[i].score);
                }
                if(scores.Length<MaxScores) {
                    for(int i=scores.Length;i<MaxScores;i++) {
                        Entries[i].text = (i+1).ToString() + ".  none";
                    }
                }
            }
            else {
                Debug.Log("fail");
            }
        });
    }
    public void SubmitScore() {
        LootLockerSDKManager.SubmitScore(MemberID.text, int.Parse(PlayerScore.text),ID,(response)=>{
            if(response.success) {
                Debug.Log("success");
            }
            else {
                Debug.Log("fail");
            }
        });
    }

    

    public void connectToDatabase() {
            LootLockerSDKManager.StartGuestSession((response)=>{
            if(response.success) {
                Debug.Log("success");
            }
            else {
                Debug.Log("fail");
            }
        });
    }
} 