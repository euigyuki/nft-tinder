using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameOverMenuDemo : MonoBehaviour
{
    public Text textMesh;
    public double profit;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setScoreText();
        
    }

    public void GoToMenu() {
        Debug.Log("Menu Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame() {
        Debug.Log("Restart Button Clicked");
        PriceManager.resetEverything();
        phoneBehavior.setCount = 0;
        // SceneManager.LoadScene("trendingDerrick");
        SceneManager.LoadScene("MidtermBuyPage");

    }

    public void QuitGame() {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
     void setScoreText() {
        Debug.Log("Money"+PriceManager.walletValue);
        profit = PriceManager.walletValue-5000;
        if(profit>0)
        {
            Debug.Log("Positive profit" + profit);
            textMesh.text="Congratulations you have made an overall profit of " + string.Format("{0:0.##}", profit + " $");
        }
        else
        {
            Debug.Log("Negative loss" + Math.Abs(profit));
            textMesh.text="So Sorry you have encurred an overall loss of " + string.Format("{0:0.##}",  Math.Abs(profit) + " $");

        }
        // textMesh.text = string.Format("{0:0.##}", PriceManager.walletValue);
    }

}
