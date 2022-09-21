using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuDemo : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToMenu() {
        Debug.Log("Menu Button Clicked");
        SceneManager.LoadScene("MenuScene");
    }

    public void RestartGame() {
        Debug.Log("Restart Button Clicked");
        SceneManager.LoadScene("pricingManagerHype");
    }

    public void QuitGame() {
        Debug.Log("Quit Button Clicked");
        Application.Quit();
    }
}
