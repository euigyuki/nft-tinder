using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using TMPro;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class TutorialBuyManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI priceText;

    private double money = 5000;
    private double price;

    public GameObject[] arrows = new GameObject[5];

    public Button green;
    public Button red;

    public Vector3 downPos;
    public Vector3 upPos;

    public TextMeshProUGUI dialogueText;
    public TextAsset asset;
    public String[] dialogues;
    public GameObject dialogueBox;
    public TextMeshProUGUI popUpText;
    public GameObject popUp;
    private int index = 0;
    private bool paused = false;
    private bool isWaiting = false;
    private float tempRecVal = 0.3333f;
    private int buyCount = 5;

    [SerializeField] nftGenerator firstCard;
    [SerializeField] nftGenerator secondCard;

    public Slider recommender;
    public Image Fill;

    [SerializeField] BarManager HypeBar;
    [SerializeField] BarManager TimerBar;

    public Color startColor;
    public Color endColor;
    public RawImage BG1;
    public RawImage BG2;

    private float levelCap = 100f;
    private float timeCap = 7f;

    private float currLevel = 60f;
    private float currTime;

    private float incPerBuy = 5f;
    private float feverTime = 3f;
    private bool isCD = false;

    private bool timer = false;
    
    [SerializeField] AudioSource rightSoundEffect;
    [SerializeField] AudioSource leftSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        changecolorButton();
        changeFillcolor();
        changeColor();
        dialogues = asset.text.Split('\n');
        changeDialogue(index++);
        recommender.value = 0;
        price = Random.Range(10,100);
        popUp.SetActive(false);
        green.enabled = false;
        red.enabled =false;
        
        hideArrows();
        setLevelBar();
        resetTimerBar();
    }

    // Update is called once per frame
    void Update()
    {
        showMoney();
        showPrice();
        if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space") && !isWaiting ){
            if(new []{5, 7, 12, 15, 18}.Contains(index)){
                paused = true;
                green.enabled = true;
                red.enabled =true;
                dialogueBox.SetActive(false);
                hideArrows();
                if(index == 18) StartCoroutine(feverMode());
            }
            if(index == 21) SceneManager.LoadScene("NewestTutorialSell");
            if(!paused && index!=21) changeDialogue(index++ %dialogues.Length);
        }

        if(Input.GetKeyDown("right")){
            buy();
        }
        
        if(Input.GetKeyDown("left")){
            pass();
        }

        if(timer){
            currTime -= Time.deltaTime;
            setTimerBar();
            if(currTime<=0){
                resetTimerBar();
            }
        }
    }

    void changeDialogue(int index){
        dialogueText.text = dialogues[index];
        dialogueBox.transform.position = downPos;
        hideArrows();
        if(index == 3){
            showArrow(5);
        }
        if(new []{9, 10, 11}.Contains(index)){
            showArrow(0);
            showArrow(1);
            dialogueBox.transform.position = upPos;
        }else if(new []{12, 13, 14}.Contains(index)){
            showArrow(2);
            dialogueBox.transform.position = upPos;
        }else if(new []{15, 16, 17}.Contains(index)){
            showArrow(3);
        }else if(index == 19){
            showArrow(4);
            dialogueBox.transform.position = upPos;
            timer = true;
        }else if(index>19){
            timer = false;
            resetTimerBar();
            setTimerBar();
        }
    }

    public void buy(){
        
        rightSoundEffect.Play();

        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        if(isWaiting || !paused) return;
        if(index == 7){
            showPopUp("you should've passed");
            return;
        }
        levelIncrease();
        float currRecVal = recommender.value;
        if(!isCD)        money -= price;
        else money -= price*0.8f;
        firstCard.swipe(false);
        secondCard.moveCard();
        recommender.value = tempRecVal;
        randomGenNFT(firstCard);
        price = Random.Range(10,100);
        swapGen();
        if(index == 5){
                showDialogueBox();
        }
        if(index == 12 || index == 15){
            if(currRecVal < 0.3f) showPopUp("you should've passed");
            else showPopUp("good job!");
            if(index==12 && --buyCount <=0) showDialogueBox();
            if(index==15 && currLevel >99f) showDialogueBox();
        }
    }

    public void pass(){
        
        leftSoundEffect.Play();
        if(firstCard.isCoroutine || secondCard.isCoroutine) return;
        if(isWaiting || !paused) return;
        if(index == 5){
            showPopUp("you should've bought");
            return;
        }
        float currRecVal = recommender.value;
        firstCard.swipe(true);
        secondCard.moveCard();
        recommender.value = tempRecVal;
        randomGenNFT(firstCard);
        price = Random.Range(10,100);
        swapGen();
        if(index == 7){
                showDialogueBox();
        }
        if(index == 12 || index == 15){
            if(currRecVal > 0) showPopUp("you should've bought");
            else showPopUp("good job!");
            if(index==11&&buyCount--<=0) showDialogueBox();
        }
    }

    void showMoney(){
        moneyText.text = String.Format("Money: ${0:0.##}", money);
    }

    void showPrice(){
        if(!isCD)priceText.text = String.Format("${0:0.##}", price);
        else{
            priceText.text = String.Format("${0:0.##} (-20%)", price*0.8f);
        } 
    }

    void randomGenNFT(nftGenerator generator){
        StartCoroutine(waitforanimation(generator));
    }

    IEnumerator waitforanimation(nftGenerator generator){
        yield return new WaitForSeconds(firstCard.duration);
        imageHolder head = generator.imageParts[0];
        int rand = Random.Range(0,2);
        tempRecVal = rand == 0 ? 0.33333f : 0;
        head.imagePart.texture = head.imageTextures[rand];
        for(int i = 1; i<generator.imageParts.Length; i++){
            imageHolder temp = generator.imageParts[i];
            int index = Random.Range(0,temp.imageTextures.Length);
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    void hideArrows(){
        for(int i =0;i<arrows.Length;i++){
            arrows[i].SetActive(false);
        }
    }

    void showArrow(int index){
        arrows[index].SetActive(true);
    }

    void showPopUp(String text){
        popUpText.text = text;
        StartCoroutine(showAndHide(popUp));
    }
    
    void showDialogueBox(){
        paused = false;
        StartCoroutine(wait(0.5f));
    }

    IEnumerator wait(float duration)
    {
        green.enabled = false;
        red.enabled =false;
        isWaiting = true;
        yield return new WaitForSeconds(duration);
        dialogueBox.SetActive(true);
        changeDialogue(index++ %dialogues.Length);
        isWaiting = false;
    }

    IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(firstCard.duration);
        obj.SetActive(false);
    }

    void swapGen(){
        nftGenerator temp = firstCard;
        firstCard = secondCard;
        secondCard = temp;
    }

    public void setLevelBar(){
        currLevel = Cap(currLevel,0,levelCap);
        HypeBar.setXScale(Mathf.Lerp(0f,1f,currLevel/levelCap));
    }

    public void setTimerBar(){
        currTime = Cap(currTime,0,timeCap);
        TimerBar.setXScale(Mathf.Lerp(0f,1f,currTime/timeCap));
        TimerBar.setBarColor(Color.Lerp(endColor,startColor,currTime/timeCap));
    }

    public void resetLevelBar(){
        currLevel = 0f;
        setLevelBar();
    }

    public void resetTimerBar(){
        currTime = timeCap;
        setTimerBar();
    }

    public void levelIncrease(){
        if(!isCD){
            currLevel += incPerBuy;
            setLevelBar();
        }
    }

    IEnumerator feverMode(){
        isCD = true;
        float time = 0;
        float ogDuration =firstCard.duration;
        firstCard.duration /= 4;
        secondCard.duration /=4;
        while(time<feverTime){
            time += Time.deltaTime;
            currLevel  = Mathf.SmoothStep(levelCap,0,time/feverTime);
            setLevelBar();
            yield return null;
        }
        firstCard.duration = ogDuration;
        secondCard.duration = ogDuration;
        isCD = false;
        showDialogueBox();
    }

    private float Cap(float curr, float min, float max){
        if(curr<min) return min;
        else if(curr>max) return max;
        return curr;
    }

    void changeColor()
    {
         if(ClickMode.Mode=="Normal"){
            BG1.color= new Color (0.38f,0.81f,0.43f,1.0f);
            BG2.color= new Color (0.81f,0.41f,0.38f,1.0f);
        }
        if(ClickMode.Mode=="ColorBlind"){
            BG1.color=new Color(0.047f,0.48f,0.863f,1.0f);
            BG2.color=new Color(1.0f,0.76f,0.039f,1.0f);
        }
    }

    void changecolorButton()
    {
        if(ClickMode.Mode=="Normal"){
            green.GetComponent<Image>().color= new Color (0.38f,0.81f,0.43f,1.0f);
            red.GetComponent<Image>().color= new Color (0.81f,0.41f,0.38f,1.0f);
        }
        if(ClickMode.Mode=="ColorBlind"){
            green.GetComponent<Image>().color=new Color(0.047f,0.48f,0.863f,1.0f);
            red.GetComponent<Image>().color=new Color(1.0f,0.76f,0.039f,1.0f);
        }

    }
    void changeFillcolor()
    {
         if(ClickMode.Mode=="Normal"){
            Fill.color=new Color (0.38f,0.81f,0.43f,1.0f);
         }
         if(ClickMode.Mode=="ColorBlind"){
            Fill.color=new Color (0.047f,0.48f,0.863f,1.0f);
         }

    }
}