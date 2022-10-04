using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text playerText;
    public TMP_Text enemyText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerScore(int score)
    {
        playerText.text = "My Score: " + score; 
    }

    public void SetEnemyScore(int score)
    {
        enemyText.text = "Enemy Score: " + score;
    }
}
