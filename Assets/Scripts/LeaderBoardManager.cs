using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class LeaderBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    double PlayerScore = PriceManager.walletValue;
    public int ID;
    int MaxScores = 10;
    public TextMeshProUGUI[] Entries;
    public TextMeshProUGUI scoreText;
    
    void  Awake() 
    {
        LootLockerSDKManager.GetScoreList(ID,MaxScores,(response)=>{
            if(response.success) {
                LootLockerLeaderboardMember[] scores = response.items;
                for(int i =0;i<scores.Length;i++) {
                    //Entries[i].text = (scores[i].rank + scores[i].member_id + "                                  " +scores[i].score);
                    Entries[i].text = scores[i].rank.ToString() + "." + scores[i].member_id;
                    for(int j=0;j<65-scores[i].member_id.Length-scores[i].score.ToString().Length-scores[i].rank.ToString().Length;j++) {
                        Entries[i].text = Entries[i].text + " ";
                    }
                    Entries[i].text = Entries[i].text + scores[i].score;
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
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GoToMenu() {
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }
     public void Play() {
        Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
}
