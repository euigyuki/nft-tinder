using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;


public class TutorialSellManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextAsset asset;
    public String[] dialogues;
    private int index = 0;

    private bool isWaiting =false;
    private bool paused = false;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = asset.text.Split('\n');
        changeDialogue(index++);
        
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space") && !isWaiting ){
            // if(new []{4, 6, 11, 14, 17}.Contains(index)){
            //     paused = true;
            //     green.enabled = true;
            //     red.enabled =true;
            //     dialogueBox.SetActive(false);
            //     hideArrows();
            //     if(index == 17) StartCoroutine(feverMode());
            // }
            // if(index == 20) SceneManager.LoadScene("NewTutorialSell");
            if(!paused) changeDialogue(index++ %dialogues.Length);
        }
    }

    void changeDialogue(int index){
        dialogueText.text = dialogues[index];
        // dialogueBox.transform.position = downPos;
        // hideArrows();
        // if(new []{8, 9, 10}.Contains(index)){
        //     showArrow(0);
        //     showArrow(1);
        //     dialogueBox.transform.position = upPos;
        // }else if(new []{11, 12, 13}.Contains(index)){
        //     showArrow(2);
        //     dialogueBox.transform.position = upPos;
        // }else if(new []{14, 15, 16}.Contains(index)){
        //     showArrow(3);
        // }else if(index == 18){
        //     showArrow(4);
        //     dialogueBox.transform.position = upPos;
        //     timer = true;
        // }else if(index>18){
        //     timer = false;
        //     resetTimerBar();
        //     setTimerBar();
        // }
    }
}
