using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneBehavior : MonoBehaviour
{
    public GameObject buy;
    public GameObject pass;
    public int fadeTime;
    public static int setCount = 0;

    [SerializeField] HypeLevelManager hlMang;

    [SerializeField] nftGenerator firstCard;
    [SerializeField] nftGenerator secondCard;

    [SerializeField] MoneyBar mb;

    private Timer timer;



    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        buy.SetActive(false);
        pass.SetActive(false);
        if (setCount == 0) {
            PriceManager.setUp();
            setCount += 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsPaused)
        {
            return;
        }
        if (Input.GetKeyDown("right")){
            phoneBuy();
        }

        if(Input.GetKeyDown("left")){
            phonePass();
        }
    }

    public void phonePass(){
        // StartCoroutine(showAndHide(pass));
        // PriceManager.instance.passItem();
        // PriceManager.instance.passNft();
        // generator.randomGen();
        
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        StaticAnalytics.leftPressIncrement();
        PriceManager.passNft();
        
        firstCard.swipe(true);
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        swapGen();
        hlMang.resetTimerBar();
        hlMang.resetLevelBar();
    }

    public void phoneBuy(){
        // StartCoroutine(showAndHide(buy));
        // PriceManager.instance.SubstractMoney();
        // PriceManager.instance.buyNft();
        // generator.randomGen();
        mb.ShowMoney();
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        StaticAnalytics.rightPressIncrement();
        PriceManager.buyNft();

        firstCard.swipe(false);
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        swapGen();
        hlMang.resetTimerBar();
        hlMang.levelIncrease();
    }

    IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        obj.SetActive(false);
    }

    public void swapGen(){
        nftGenerator temp = firstCard;
        firstCard = secondCard;
        secondCard = temp;
    }

    public void disablePhone(){
        enabled =false;
    }
}
