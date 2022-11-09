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

    [SerializeField] BotManager bot; 
    [SerializeField] PopWindows popWindow;

    [SerializeField] nftGenerator firstCard;
    [SerializeField] nftGenerator secondCard;

    [SerializeField] AudioSource rightSoundEffect;
    [SerializeField] AudioSource leftSoundEffect;

    public Button redButton;
    public Button greenButton;
    public Button skipButton;
    

    // [SerializeField] MoneyBar mb;

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
        PriceManager.passNft();
        firstCard.setNftPic();
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
        if (timer.IsPaused)
        {
            redButton.enabled = false;
            greenButton.enabled = false;
            skipButton.enabled = false;
            return;
        }
        redButton.enabled = true;
        greenButton.enabled = true;
        skipButton.enabled = true;
        if (Input.GetKeyDown("right")){
            phoneBuy();
        }

        if(Input.GetKeyDown("left")){
            phonePass();
        }
    }
    public void changeColor()
    {
        if(ClickMode.Mode=="Normal"){
            redButton.GetComponent<Image>().color=new Color (0.81f,0.41f,0.38f,1.0f);
            greenButton.GetComponent<Image>().color=new Color (0.38f,0.81f,0.43f,1.0f);
        }
        if(ClickMode.Mode=="ColorBlind"){
            greenButton.GetComponent<Image>().color=new Color(0.047f,0.48f,0.863f,1.0f);
            redButton.GetComponent<Image>().color=new Color(1.0f,0.76f,0.039f,1.0f);
        }
    }

    public void phonePass(){
        // StartCoroutine(showAndHide(pass));
        // PriceManager.instance.passItem();
        // PriceManager.instance.passNft();
        // generator.randomGen();
        
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        StaticAnalytics.leftPressIncrement();
        if(PriceManager.buyProb()>0.4f){
            bot.BotBuy();
        }
        PriceManager.passNft();
        
        firstCard.swipe(true);
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        leftSoundEffect.Play();
        swapGen();
        hlMang.resetTimerBar();
    }

    public void phoneBuy(){
        // StartCoroutine(showAndHide(buy));
        // PriceManager.instance.SubstractMoney();
        // PriceManager.instance.buyNft();
        // generator.randomGen();
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        if(PriceManager.getCurrentNftPrice()>PriceManager.walletValue){
             popWindow.showmessage();
             return;
         }
        StaticAnalytics.rightPressIncrement();
        if(PriceManager.sellProb()>0.4f){
            bot.BotPass();
        }
        PriceManager.buyNft();
        // mb.ShowMoney();

        firstCard.swipe(false);
        secondCard.setNftPic();
        secondCard.moveCard();
        
        // firstCard.setNftPic();
        rightSoundEffect.Play();
        swapGen();
        hlMang.resetTimerBar();
        hlMang.levelIncrease();
    }

    public void setDuration(float newDuration){
        firstCard.duration = newDuration;
        secondCard.duration = newDuration;
        bot.duration = newDuration;
    }

    public float getDuration(){
        return firstCard.duration;
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
