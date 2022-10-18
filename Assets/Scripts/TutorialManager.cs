using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialCards = new GameObject[4];
    private int index=0; 
    // Start is called before the first frame update
    void Start()
    {
        for(int i =1; i<tutorialCards.Length;i++){
            tutorialCards[i].SetActive(false);
        }
        tutorialCards[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right")){
            nextCard();
        }

        if(Input.GetKeyDown("left")){
            prevCard();
        }   
    }

    public void nextCard(){
        if(index<tutorialCards.Length-1){
            tutorialCards[index++%tutorialCards.Length].SetActive(false);
            tutorialCards[index%tutorialCards.Length].SetActive(true);
        }
    }

    public void prevCard(){
        if(index>0){
            tutorialCards[index--%tutorialCards.Length].SetActive(false);
            tutorialCards[index%tutorialCards.Length].SetActive(true);
        }
    }

    public void startGame(){
        SceneManager.LoadScene("MidtermBuyPage");
    }
}
