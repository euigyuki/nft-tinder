using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject botText;
    public GameObject botText2;
    public float duration;

    public Image imagePart;
    public Sprite[] imageSprites = new Sprite[6];

    public float currTime = 0f;

    bool isCoroutine = false;

    void Start()
    {
        botText.SetActive(false);
        botText2.SetActive(false);
        changeEmotion(2);
    }

    void Update()
    {
        currTime += Time.deltaTime;
        if(currTime >= 4){
            randomEmoji();
        }
    }


    public void BotBuy() {
        if(!isCoroutine) StartCoroutine(showAndHide());
    }

    public void BotPass() {
        if(!isCoroutine) StartCoroutine(showAndHide2());
    }
    
    public void changeDuration(float newDuration){
        duration = newDuration;
    }

    public void randomEmoji(){
        int index = Random.Range(0,imageSprites.Length);
        imagePart.sprite = imageSprites[index];
        currTime = 0;
    }

    // 0 ang, 1 dis, 2 happy, 3 laugh, 4 sad, 5 spark
    public void changeEmotion(int index){
        imagePart.sprite = imageSprites[index];
        currTime = 0;
    }

    IEnumerator showAndHide()
    {
        isCoroutine = true;
        changeEmotion(5);
        botText.SetActive(true);
        yield return new WaitForSeconds(duration);
        botText.SetActive(false);
        randomEmoji();
        isCoroutine = false;
    }

    IEnumerator showAndHide2()
    {
        isCoroutine = true;
        changeEmotion(3);
        botText2.SetActive(true);
        yield return new WaitForSeconds(duration);
        botText2.SetActive(false);
        randomEmoji();
        isCoroutine = false;
    }
}
