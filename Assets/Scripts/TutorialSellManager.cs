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
    public GameObject[] arrows = new GameObject[5];
    public GameObject dialogueBox;
    [SerializeField] DialogueBox DB;
    private int index = 0;

    public Vector3 downPos;
    public Vector3 upPos;

    private bool isWaiting =false;
    private bool paused = false;

    private float greenFaceMoney = 514.24f;
    private float pinkFaceMoney = 257.12f;
    private int totGreenCount = 8;
    private int totPinkCount = 4;
    private float wallet = 2500;
    private float portfolio;
    private int selectedGreen =0;
    private int greenOverFlow =0;
    private int selectedPink =0;
    private int pinkOverFlow = 0;
    private float totSelectedVal = 0;

    public Button[] faces = new Button[4];
    public Button[] hats = new Button[4];
    public Button[] bodies = new Button[4];

    public sellCardTutorial[] faceCards = new sellCardTutorial[4];
    public sellCardTutorial[] hatCards = new sellCardTutorial[4];
    public sellCardTutorial[] bodyCards = new sellCardTutorial[4];

    public Button sellButton;
    public Button sellAllButton;

    public TextMeshProUGUI totalNumNFTsOwned;
    public TextMeshProUGUI totalPortfolioValue;
    public TextMeshProUGUI totalNumNFTsSelected;
    public TextMeshProUGUI selectedPortfolioValue;
    public TextMeshProUGUI walletValue;

    public TextMeshProUGUI profitLossText;

    public Texture checkMark;
    public Texture checkBox;

    // Selected Color: RGB(0,0,0), 150
    public Color selectedColor;
    public Color goodColor;
    public Color badColor;
    // Inactive Color: RGB(255,255,255), 60
    // public Color inactiveColor;


    bool[] faceSelected = {false, false, false, false};
    bool[] hatSelected = {false, false, false, false};
    bool[] bodySelected = {false, false, false, false};

    [SerializeField] AudioSource soundEffect;

    // Start is called before the first frame update
    void Start()
    {
        dialogues = asset.text.Split('\n');
        hideArrows();
        setPortfolio();
        setWallet();
        updateCards();   
        setSelected();


        // Faces
        faces[0] = faces[0].GetComponent<Button>();
        faces[0].onClick.AddListener(() => selectButton(faceCards[0]));

        faces[1] = faces[1].GetComponent<Button>();
        faces[1].onClick.AddListener(() => selectButton(faceCards[1]));

        faces[2] = faces[2].GetComponent<Button>();
        faces[2].onClick.AddListener(() => selectButton(faceCards[2]));

        faces[3] = faces[3].GetComponent<Button>();
        faces[3].onClick.AddListener(() => selectButton(faceCards[3]));

        // Hats
        hats[0] = hats[0].GetComponent<Button>();
        hats[0].onClick.AddListener(() => selectButton(hatCards[0]));

        hats[1] = hats[1].GetComponent<Button>();
        hats[1].onClick.AddListener(() => selectButton(hatCards[1]));

        hats[2] = hats[2].GetComponent<Button>();
        hats[2].onClick.AddListener(() => selectButton(hatCards[2]));

        hats[3] = hats[3].GetComponent<Button>();
        hats[3].onClick.AddListener(() => selectButton(hatCards[3]));

        //Bodies
        bodies[0] = bodies[0].GetComponent<Button>();
        bodies[0].onClick.AddListener(() => selectButton(bodyCards[0]));

        bodies[1] = bodies[1].GetComponent<Button>();
        bodies[1].onClick.AddListener(() => selectButton(bodyCards[1]));

        bodies[2] = bodies[2].GetComponent<Button>();
        bodies[2].onClick.AddListener(() => selectButton(bodyCards[2]));

        bodies[3] = bodies[3].GetComponent<Button>();
        bodies[3].onClick.AddListener(() => selectButton(bodyCards[3]));

        

        sellAllButton.onClick.AddListener(sellEverything);
        sellButton.onClick.AddListener(sellSelected);

        changeDialogue(index++);
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetMouseButtonDown(0) || Input.GetKeyDown("return") || Input.GetKeyDown("space") && !isWaiting ){
            soundEffect.Play();
            if(new []{8,10,15}.Contains(index)){
                paused = true;
                dialogueBox.SetActive(false);
                // hideArrows();
            }
            if(index == 9 ){
                paused = true;
                selectButton(faceCards[0]);
                paused = false;
            }
            if(index == 11 ){
                paused = true;
                selectButton(faceCards[1]);
                paused = false;
            }
            if(index == 20) return;
            if(!paused) changeDialogue(index++ %dialogues.Length);
        }
    }

    // 0 ang, 1 dis, 2 happy, 3 laugh, 4 sad, 5 spark
    void changeDialogue(int index){
        dialogueText.text = dialogues[index];
        dialogueBox.transform.position = downPos;
        hideArrows();
        DB.changeEmotion(2);

        if(index == 8 || index == 16){
            DB.changeEmotion(5);
        }
        if(index == 12){
            DB.changeEmotion(3);
        }
        if(index == 17){
            DB.changeEmotion(0);
        }
        if(index == 2){
            showArrow(0);
        }
        if(index == 3){
            showArrow(1);
        }
        if(index == 7 || index ==6 || index == 8){
            showArrow(2);
        }
        if(index ==9||index ==10){
            showArrow(3);
        }
        if(index ==11){
            showArrow(4);
        }
        if(index == 13){
            dialogueBox.transform.position = upPos;
            showArrow(5);
            showArrow(6);
        }
        if(index == 15 || index == 19){
            showArrow(7);
        }
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

    void updateCards(){
        for(int i =0;i<4;i++){
            colorchange();
            setEachCard(faceCards[i]);
            setEachCard(bodyCards[i]);
            setEachCard(hatCards[i]);
        }
    }
    void colorchange()
    {
        if(ClickMode.Mode=="Normal"){
            goodColor= new Color (0.38f,0.81f,0.43f,1.0f);
            badColor= new Color (0.81f,0.41f,0.38f,1.0f);
        }
        if(ClickMode.Mode=="ColorBlind"){
            goodColor=new Color(0.047f,0.48f,0.863f,1.0f);
            badColor=new Color(1.0f,0.76f,0.039f,1.0f);
        }
    }

    void setEachCard(sellCardTutorial card){
        int totalCount = card.greenCount + card.pinkCount;
        float totalPrice = card.greenCount*greenFaceMoney/8f + card.pinkCount*pinkFaceMoney/4f;
        card.nftCount.text = "" + totalCount;
        card.nftsTotalPrice.text = String.Format("${0:0.##}", totalPrice);
        if(card.selected) card.checkMarkImg.texture = checkMark;
        else card.checkMarkImg.texture = checkBox;

        if(totalCount == 0 || card.greenCount == card.pinkCount) card.background.color = Color.white;
        else if(card.greenCount > card.pinkCount) card.background.color = goodColor;
        else if(card.greenCount < card.pinkCount) card.background.color = badColor;
    }

    public void selectButton(sellCardTutorial card){
        if(card.greenCount==0 && card.pinkCount==0) return;
        if(!paused) return;
        if(index == 8 && card.greenCount != 8) return;
        if(index == 10 && card.pinkCount != 4) return;
        //was selected, need to subtract
        if(card.selected){
            greenOverFlow -= card.greenCount;
            pinkOverFlow -= card.pinkCount;
            adjustOverflow();
        }else{
            greenOverFlow += card.greenCount;
            pinkOverFlow += card.pinkCount;
            adjustOverflow();
        }
        setSelected();
        setProfitLossText();

        card.selected = !card.selected;
        setEachCard(card);
        if(index == 8 || index == 10) showDialogueBox();
    }

    void sellEverything(){
        if(!paused) return;
        if(index !=15) return;
        wallet += portfolio;
        setWallet();
        totGreenCount =0;
        totPinkCount =0;
        pinkOverFlow =0;
        greenOverFlow=0;
        adjustOverflow();
        setPortfolio();
        setSelected();
        setProfitLossText();
        for(int i =0;i<4;i++){
            sellCard(faceCards[i]);
            sellCard(bodyCards[i]);
            sellCard(hatCards[i]);
        }
        showDialogueBox();
    }

    void sellSelected(){
        if(!paused) return;
        if(index !=15) return;
        wallet += totSelectedVal;
        setWallet();
        totGreenCount -=selectedGreen;
        totPinkCount -= selectedPink;
        int faceGreen = selectedGreen;
        int facePink = selectedPink;
        int bodyGreen = selectedGreen;
        int bodyPink = selectedPink;
        int hatGreen = selectedGreen;
        int hatPink = selectedPink;
        for(int i =0;i<4;i++){
            if(faceCards[i].selected){
                faceGreen -= faceCards[i].greenCount;
                facePink -= faceCards[i].pinkCount;
                sellCard(faceCards[i]);
            } 
            if(bodyCards[i].selected){
                bodyGreen -= bodyCards[i].greenCount;
                bodyPink -= bodyCards[i].pinkCount;
                sellCard(bodyCards[i]);
            } 
            if(hatCards[i].selected){
                hatGreen -= hatCards[i].greenCount;
                hatPink -= hatCards[i].pinkCount;
                sellCard(hatCards[i]);
            } 
        }

        for(int i =0;i<4;i++){
            if(faceGreen != 0 || facePink != 0){
                int GreenToSell;
                int PinkToSell;
                if(faceCards[i].greenCount <= faceGreen) GreenToSell = faceCards[i].greenCount;
                else GreenToSell = faceGreen;

                if(faceCards[i].pinkCount <= facePink) PinkToSell = faceCards[i].pinkCount;
                else PinkToSell = facePink;

                faceGreen -= GreenToSell;
                facePink -= PinkToSell;
                sellCardGreenPink(faceCards[i],GreenToSell,PinkToSell);
            }
            if(bodyGreen != 0 || bodyPink != 0){
                int GreenToSell;
                int PinkToSell;
                if(bodyCards[i].greenCount <= bodyGreen) GreenToSell = bodyCards[i].greenCount;
                else GreenToSell = bodyGreen;

                if(bodyCards[i].pinkCount <= bodyPink) PinkToSell = bodyCards[i].pinkCount;
                else PinkToSell = bodyPink;

                bodyGreen -= GreenToSell;
                bodyPink -= PinkToSell;
                sellCardGreenPink(bodyCards[i],GreenToSell,PinkToSell);
            } 
            if(hatGreen != 0 || hatPink != 0){
                int GreenToSell;
                int PinkToSell;
                if(hatCards[i].greenCount <= hatGreen) GreenToSell = hatCards[i].greenCount;
                else GreenToSell = hatGreen;

                if(hatCards[i].pinkCount <= hatPink) PinkToSell = hatCards[i].pinkCount;
                else PinkToSell = hatPink;

                hatGreen -= GreenToSell;
                hatPink -= PinkToSell;
                sellCardGreenPink(hatCards[i],GreenToSell,PinkToSell);
            }  
        }

        selectedGreen =0;
        selectedPink =0;
        pinkOverFlow = 0;
        greenOverFlow =0;

        adjustOverflow();
        setPortfolio();
        setSelected();
        setProfitLossText();
        showDialogueBox();
    }

    void sellCard(sellCardTutorial card){
        card.selected = false;
        card.greenCount = 0;
        card.pinkCount = 0;
        setEachCard(card);
    }

    void sellCardGreenPink(sellCardTutorial card, int greensold, int pinksold){
        card.selected = false;
        card.greenCount -= greensold;
        card.pinkCount -= pinksold;
        setEachCard(card);
    }

    void adjustOverflow(){
        if(greenOverFlow <0) selectedGreen = 0;
        else if(greenOverFlow>totGreenCount) selectedGreen=totGreenCount;
        else selectedGreen = greenOverFlow;

        if(pinkOverFlow <0) selectedPink = 0;
        else if(pinkOverFlow>totPinkCount) selectedPink=totPinkCount;
        else selectedPink = pinkOverFlow;
    }

    void setWallet(){
        walletValue.text = String.Format("{0:0.##}", wallet);
    }

    void setPortfolio(){
        setPortfolioVal();
        totalNumNFTsOwned.text = ""+(totGreenCount+totPinkCount);
        totalPortfolioValue.text = String.Format("${0:0.##}", portfolio);
    }

    void setPortfolioVal(){
        portfolio = totGreenCount*greenFaceMoney/8f + totPinkCount*pinkFaceMoney/4f;
    }

    void setSelectedVal(){
        totSelectedVal = selectedGreen*greenFaceMoney/8f + selectedPink*pinkFaceMoney/4f;
    }

    void setSelected(){
        setSelectedVal();
        totalNumNFTsSelected.text = ""+(selectedGreen+selectedPink);
        selectedPortfolioValue.text = String.Format("${0:0.##}", totSelectedVal);
    }

    void setProfitLossText(){
        float profitLossValue = selectedGreen*greenFaceMoney/8f - selectedPink*pinkFaceMoney/4f;
        profitLossValue *= 0.2f;
        if(profitLossValue > 0) {
            profitLossText.text = String.Format("Profit ${0:0.##}", profitLossValue);
            profitLossText.color = Color.green;
        } else if(profitLossValue < 0) {
            profitLossText.text = String.Format("Loss ${0:0.##}", -1 * profitLossValue);
            profitLossText.color = Color.red;
        } else {
            profitLossText.text = "";
        }
    }

    void showDialogueBox(){
        paused = false;
        StartCoroutine(wait(0.5f));
    }
    IEnumerator wait(float duration)
    {
        isWaiting = true;
        yield return new WaitForSeconds(duration);
        dialogueBox.SetActive(true);
        changeDialogue(index++ %dialogues.Length);
        isWaiting = false;
    }

    void hideArrows(){
        for(int i =0;i<arrows.Length;i++){
            arrows[i].SetActive(false);
        }
    }

    void showArrow(int index){
        arrows[index].SetActive(true);
    }

}

[System.Serializable]
public class sellCardTutorial {
    public TextMeshProUGUI nftCount;
    public TextMeshProUGUI nftsTotalPrice;
    public int greenCount;
    public int pinkCount;
    public RawImage background;
    public RawImage checkMarkImg;
    public bool selected;
    // public Texture[] imageTextures = new Texture[4];
}
