
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneBehavior : MonoBehaviour
{
    public GameObject buy;
    public GameObject pass;
    public int fadeTime;
    [SerializeField] HypeLevelManager hlMang;
<<<<<<< Updated upstream
    [SerializeField] nftGenerator generator;
=======
    [SerializeField] BotManager bot; 
    [SerializeField] nftGenerator firstCard;
    [SerializeField] nftGenerator secondCard;

    [SerializeField] MoneyBar mb;

    private Timer timer;


>>>>>>> Stashed changes

    // Start is called before the first frame update
    void Start()
    {
        buy.SetActive(false);
        pass.SetActive(false);
        MoneyBar.instance.subText.SetActive(false);
        MoneyBar.instance.addText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right")){
            phoneBuy();
        }

        if(Input.GetKeyDown("left")){
            phonePass();
        }
    }

    public void phonePass(){
        StartCoroutine(showAndHide(pass));
        StartCoroutine(showAndHide(MoneyBar.instance.subText));
        // PriceManager.instance.passItem();
<<<<<<< Updated upstream
        PriceManager.instance.passNft();
        generator.randomGen();
=======
        // PriceManager.instance.passNft();
        // generator.randomGen();
        
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        StaticAnalytics.leftPressIncrement();
        PriceManager.passNft();
        
        firstCard.swipe(true);
        if(PriceManager.buyProb()>0.4f){
            bot.BotBuy();
        }
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        swapGen();
>>>>>>> Stashed changes
        hlMang.resetTimerBar();
    }

    public void phoneBuy(){
        StartCoroutine(showAndHide(buy));
        StartCoroutine(showAndHide(MoneyBar.instance.addText));
        // PriceManager.instance.SubstractMoney();
<<<<<<< Updated upstream
        PriceManager.instance.buyNft();
        generator.randomGen();
=======
        // PriceManager.instance.buyNft();
        // generator.randomGen();
        mb.ShowMoney();
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        StaticAnalytics.rightPressIncrement();
        PriceManager.buyNft();

        firstCard.swipe(false);
        if(PriceManager.sellProb()>0.4f){
            bot.BotPass();
        }
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        swapGen();
>>>>>>> Stashed changes
        hlMang.resetTimerBar();
        hlMang.levelIncrease();
    }

<<<<<<< Updated upstream
=======
    public void setDuration(float newDuration){
        firstCard.duration = newDuration;
        secondCard.duration = newDuration;
        bot.duration = newDuration;
    }

    public float getDuration(){
        return firstCard.duration;
    }

>>>>>>> Stashed changes
    IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        obj.SetActive(false);
    }

    public void disablePhone(){
        enabled =false;
    }
     IEnumerator ExecuteAfterTime(float time)
 {
     yield return new WaitForSeconds(time);
 
     
 }
}
