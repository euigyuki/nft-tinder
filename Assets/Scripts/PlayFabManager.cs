using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject rowPrefab;
    public Transform rowsParent;
    void Start()
    {
        
    }

    void OnLeaderboardGet(GetLeaderboardResult result) {
        foreach(var item in result.Leaderboard) {
            GameObject newGo = Instantiate(rowPrefab,rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = item.Position.ToString();
            texts[1].text = item.PlayFabId;
            texts[2].text = item.StatValue.ToString();
            Debug.Log(string.Format("Place:{0}|ID:{1}|VALUE:{2}",
            item.Position,item.PlayFabId,item.StatValue));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
