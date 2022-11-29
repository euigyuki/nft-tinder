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

public class LeaderBoardManager : MonoBehaviour
{
    // Start is called before the first frame update
    public int ID;
    int MaxScores = 10;
    public TextMeshProUGUI[] Entries1;
    public TextMeshProUGUI[] Entries2;
    public TextMeshProUGUI scoreText;
    public static string str;
    
    
    
    void  Awake() 
    {
       
       StaticAnalytics.getScores<UserScore>((users) => {
            Debug.Log("In pricing manager shit");
            Debug.Log(users);
            Debug.Log(str);
            
            var scoresList = new List<KeyValuePair<string, double>>();

            foreach (KeyValuePair<string, UserScore> kvp in users)
            {
                scoresList.Add(new KeyValuePair<string, double>(kvp.Key, kvp.Value.score));
            }
            Debug.Log(scoresList.Count);
            scoresList.Sort((y, x) => (x.Value.CompareTo(y.Value)));
            // foreach (KeyValuePair<string, double> kvp in scoresList) {
            //     Debug.Log(kvp.Key + ":" + kvp.Value);
            // }

            // find idx of test user

            //int idx = 0;
            //for (int i = 0; i < scoresList.Count; i++)
            //{
                
                //if (string.Equals(scoresList[i].Key, str)) {
                    //idx = i;
                    //break;
                //}
            //}

            //Debug.Log("idx: " + idx);
            // find 10 possible scores around it

            //int startIdx = Math.Max(0, idx - 5);
            //int endIdx = Math.Min(scoresList.Count, idx + 4);

            // var scoresToShow = new List<KeyValuePair<string, double>>();

            //for (int i = startIdx; i <= endIdx; i++) {
                // scoresToShow.Add(scoresList[i]);
                //Debug.Log(scoresList[i].Key + ":" + scoresList[i].Value);
            //}
            for(int i=0;i<Math.Min(11,scoresList.Count);i++) {
                Entries1[i].text = (i+1)+". "+scoresList[i].Key;
                Entries2[i].text = scoresList[i].Value.ToString();
            }

        }); 

        // LootLockerSDKManager.GetScoreList(ID,MaxScores,(response)=>{
        //     if(response.success) {
        //         LootLockerLeaderboardMember[] scores = response.items;
        //         for(int i =0;i<scores.Length;i++) {
        //             //Entries[i].text = (scores[i].rank + scores[i].member_id + "                                  " +scores[i].score);
        //             Entries[i].text = scores[i].rank.ToString() + "." + scores[i].member_id;
        //             for(int j=0;j<65-scores[i].member_id.Length-scores[i].score.ToString().Length-scores[i].rank.ToString().Length;j++) {
        //                 Entries[i].text = Entries[i].text + " ";
        //             }
        //             Entries[i].text = Entries[i].text + scores[i].score;
        //         }
        //         if(scores.Length<MaxScores) {
        //             for(int i=scores.Length;i<MaxScores;i++) {
        //                 Entries[i].text = (i+1).ToString() + ".  none";
        //             }
        //         }
        //     }
        //     else {
        //         Debug.Log("fail");
        //     }
        // });
        // PriceManager.resetEverything();
        // phoneBehavior.setCount = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // void OnError(PlayFabError error) {
    //     Debug.Log("Error while getting data");
    //     Debug.Log(error.GenerateErrorReport());
    // }

    public void GetLeaderBoard() {
        
    }
    //void OnLeaderboardGet(GetLeaderboardResult result) {
        //int i =0;
        //foreach (var item in result.Leaderboard) {
          //  Entries[i].text = item.Position + "." + item.Profile.DisplayName + "                                                     " + item.StatValue;
        //    i++;
      //  }
        
    //}
    public void GoToMenu() {
        SceneManager.LoadScene("MenuScene");
    }
     public void Play() {
        Debug.Log("Restart Button Clicked");
        SceneManager.LoadScene("ObjectiveBuyDerrick");
    }
}