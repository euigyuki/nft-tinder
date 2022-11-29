using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class sellHelper : MonoBehaviour
{
    
    public Button[] faces = new Button[4];
    public Button[] hats = new Button[4];
    public Button[] bodies = new Button[4];

    public sellCard[] faceCards = new sellCard[4];
    public sellCard[] hatCards = new sellCard[4];
    public sellCard[] bodyCards = new sellCard[4];

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

    // Button face1Btn;
    Dictionary<string, PriceManager.Nft> nftDict;
    List<string> nftsOwned = new List<string>();

    Dictionary<int, List<string>> faceIdMap = new Dictionary<int, List<string>>();
    Dictionary<int, List<string>> hatIdMap = new Dictionary<int, List<string>>();
    Dictionary<int, List<string>> bodyIdMap = new Dictionary<int, List<string>>();

    List<string> toSellNFTIds = new List<string>();

    public static int totalSells = 0;
    public static int posSells = 0;
    public static int negSells = 0;

    public static double totalProfitLoss = 0.0;

    // Start is called before the first frame update
    void Start() {
        totalSells = 0;
        posSells = 0;
        negSells = 0;
        HypeLevelManager.countdownTimerStr = "";
        PriceManager.setUp();

        // Faces
        faces[0] = faces[0].GetComponent<Button>();
        faces[0].onClick.AddListener(() => ButtonSelected("face", 0));

        faces[1] = faces[1].GetComponent<Button>();
        faces[1].onClick.AddListener(() => ButtonSelected("face", 1));

        faces[2] = faces[2].GetComponent<Button>();
        faces[2].onClick.AddListener(() => ButtonSelected("face", 2));

        faces[3] = faces[3].GetComponent<Button>();
        faces[3].onClick.AddListener(() => ButtonSelected("face", 3));

        // Hats
        hats[0] = hats[0].GetComponent<Button>();
        hats[0].onClick.AddListener(() => ButtonSelected("hat", 0));

        hats[1] = hats[1].GetComponent<Button>();
        hats[1].onClick.AddListener(() => ButtonSelected("hat", 1));

        hats[2] = hats[2].GetComponent<Button>();
        hats[2].onClick.AddListener(() => ButtonSelected("hat", 2));

        hats[3] = hats[3].GetComponent<Button>();
        hats[3].onClick.AddListener(() => ButtonSelected("hat", 3));

        //Bodies
        bodies[0] = bodies[0].GetComponent<Button>();
        bodies[0].onClick.AddListener(() => ButtonSelected("body", 0));

        bodies[1] = bodies[1].GetComponent<Button>();
        bodies[1].onClick.AddListener(() => ButtonSelected("body", 1));

        bodies[2] = bodies[2].GetComponent<Button>();
        bodies[2].onClick.AddListener(() => ButtonSelected("body", 2));

        bodies[3] = bodies[3].GetComponent<Button>();
        bodies[3].onClick.AddListener(() => ButtonSelected("body", 3));

        

        sellButton.onClick.AddListener(SellSelectedNFTs);
        sellAllButton.onClick.AddListener(SellAllNFTs);

        for(int i=0;i<4;i++) {
            faceCards[i].checkMarkImg.texture = checkBox;
            hatCards[i].checkMarkImg.texture = checkBox;
            bodyCards[i].checkMarkImg.texture = checkBox;
        }

    }

    public static void pushSellStats() {
        // Debug.Log("Positive Sells Count: " + posSells);
        // Debug.Log("Negative Sells Count: " + negSells);
        // Debug.Log("Total Sells Count: " + totalSells);
        Debug.Log("sellHelper pushingSellStats");
        StaticAnalytics.postEachLevelSellData(totalSells, posSells, negSells);
        StaticAnalytics.toJson();
    }

    // Update is called once per frame
    void Update()
    {
        setTotalNumNFTsOwned();
        setTotalPortfolioValue();
        setTotalNumNFTsSelected();
        setSelectedPortfolioValue();
        setWalletValue();

        if(toSellNFTIds.Count > 0) {
            sellButton.GetComponent<Button>().interactable = true;
        } else {
            sellButton.GetComponent<Button>().interactable = false;
        }

        if(nftsOwned.Count > 0) {
            sellAllButton.GetComponent<Button>().interactable = true;
        } else {
            sellAllButton.GetComponent<Button>().interactable = false;
        }

        faceIdMap = new Dictionary<int, List<string>>();
        hatIdMap = new Dictionary<int, List<string>>();
        bodyIdMap = new Dictionary<int, List<string>>();

        for(int i=0;i<4;i++) {
            faceIdMap.Add(i,new List<string>());
            hatIdMap.Add(i,new List<string>());
            bodyIdMap.Add(i,new List<string>());
        }

        nftDict = PriceManager.getNftsDict();
        nftsOwned = PriceManager.getNftsOwnedAsList();

        int faceIndex = 0;
        int hatIndex = 2;
        int bodyIndex = 3;


        foreach (string nftID in nftsOwned)
        {
            int[] imgIndices = nftDict[nftID].imagePics;

            faceIdMap[imgIndices[faceIndex]].Add(nftID);
            hatIdMap[imgIndices[hatIndex]].Add(nftID);
            bodyIdMap[imgIndices[bodyIndex]].Add(nftID);
        }

        for(int i=0;i<4;i++) {
            
            if(faceIdMap[i].Count == 0) {
                faces[i].GetComponent<Button>().interactable = false;
                faceCards[i].background.color = Color.gray;
            }
            if(hatIdMap[i].Count == 0) {
                hats[i].GetComponent<Button>().interactable = false;
                hatCards[i].background.color = Color.gray;
            }
            if(bodyIdMap[i].Count == 0) {
                bodies[i].GetComponent<Button>().interactable = false;
                bodyCards[i].background.color = Color.gray;
            }
        }

        refreshCards();

    }
    public void changeColorSell()
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

    void SellAllNFTs() {

        foreach(string nftId in nftsOwned) {
            int[] nftDetails = nftDict[nftId].imagePics;
            // Face
            totalSells += 1;
            if(nftDetails[0] == PriceManager.currentTrendingFacePos || nftDetails[2] == PriceManager.currentTrendingHatPos || nftDetails[3] == PriceManager.currentTrendingBodyPos) {
                posSells += 1;
                
            } else {
                negSells += 1;
            }
        }

        totalProfitLoss += getProfitLossValue(nftsOwned);

        PriceManager.sellNftsFromList(nftsOwned);
        nftsOwned = PriceManager.getNftsOwnedAsList();

        for(int i=0;i<4;i++) {
            if(faceSelected[i]==true) {
                ButtonSelected("face", i);
            }
            if(hatSelected[i]==true) {
                ButtonSelected("hat", i);
            }
            if(bodySelected[i]==true) {
                ButtonSelected("body", i);
            }
        }

        UpdateNftsToSell();
    }

    void SellSelectedNFTs() {
        foreach(string nftId in toSellNFTIds) {
            int[] nftDetails = nftDict[nftId].imagePics;
            // Face
            totalSells += 1;
            if(nftDetails[0] == PriceManager.currentTrendingFacePos || nftDetails[2] == PriceManager.currentTrendingHatPos || nftDetails[3] == PriceManager.currentTrendingBodyPos) {
                posSells += 1;
                
            } else {
                negSells += 1;
            }
        }

        totalProfitLoss += getProfitLossValue(toSellNFTIds);
        // Debug.Log(getProfitLossValue());
        // Debug.Log(String.Format("Total Profit or Loss ${0:0.##}", totalProfitLoss));

        PriceManager.sellNftsFromList(toSellNFTIds);
        
        for(int i=0;i<4;i++) {
            if(faceSelected[i]==true) {
                ButtonSelected("face", i);
            }
            if(hatSelected[i]==true) {
                ButtonSelected("hat", i);
            }
            if(bodySelected[i]==true) {
                ButtonSelected("body", i);
            }
        }

        
        UpdateNftsToSell();

    }

    void UpdateNftsToSell() {

        // Doing OR operations to get all nft_ids across all categories

        toSellNFTIds = new List<string>();

        for(int i=0;i<4;i++) {
            if(faceSelected[i]==true) {
                toSellNFTIds.AddRange(faceIdMap[i]);
            }

            if(hatSelected[i]==true) {
                toSellNFTIds.AddRange(hatIdMap[i]);
            }

            if(bodySelected[i]==true) {
                toSellNFTIds.AddRange(bodyIdMap[i]);
            }
        }

        toSellNFTIds = new HashSet<string>(toSellNFTIds).ToList();

    }

    void ButtonSelected(string type, int k) {
        if(type.Equals("face")) {
            faceSelected[k] = !faceSelected[k];
            if(faceSelected[k]) {
                faceCards[k].checkMarkImg.texture = checkMark;
            }
            else {
                faceCards[k].checkMarkImg.texture = checkBox;
            }
        } else if(type.Equals("hat")) {
            hatSelected[k] = !hatSelected[k];
            if(hatSelected[k]){
                hatCards[k].checkMarkImg.texture = checkMark;
            } else {
                hatCards[k].checkMarkImg.texture = checkBox;
            }
        } else if(type.Equals("body")) {
            bodySelected[k] = !bodySelected[k];
            if(bodySelected[k]){
                bodyCards[k].checkMarkImg.texture = checkMark;
            } else {
                bodyCards[k].checkMarkImg.texture = checkBox;
            }
        }
        
        UpdateNftsToSell();
        setProfitLossText();

    }

    int checkForProfit(List<string> nftIds) {
        double costPrice = PriceManager.getBuyPricePortfolioValueFromList(nftIds);
        double sellPrice = PriceManager.getSelectedPortfolioValue(nftIds);

        if((sellPrice - costPrice) > 0) {
            return 1;
        } else if((sellPrice - costPrice) < 0) {
            return -1;
        } else {
            return 0;
        }
    }

    void refreshCards() {
        changeColorSell();

        for(int i=0;i<4;i++) {

            // Debug.Log("Index: " + i);

            // Card Background color check
            int faceProfitStatus = checkForProfit(faceIdMap[i]);
            if(faceProfitStatus == 1) {
                faceCards[i].background.color = goodColor;
            } else if(faceProfitStatus == -1) {
                faceCards[i].background.color = badColor;
            } else {
                faceCards[i].background.color = Color.white;
            }
            // Card Background color check
            int hatProfitStatus = checkForProfit(hatIdMap[i]);
            if(hatProfitStatus == 1) {
                hatCards[i].background.color = goodColor;
            } else if(hatProfitStatus == -1) {
                hatCards[i].background.color = badColor;
            } else {
                hatCards[i].background.color = Color.white;
            }
            // Card Background color check
            int bodyProfitStatus = checkForProfit(bodyIdMap[i]);
            if(bodyProfitStatus == 1) {
                bodyCards[i].background.color = goodColor;
            } else if(bodyProfitStatus == -1) {
                bodyCards[i].background.color = badColor;
            } else {
                bodyCards[i].background.color = Color.white;
            }

            faceCards[i].nftCount.text = "" + faceIdMap[i].Count;
            hatCards[i].nftCount.text = "" + hatIdMap[i].Count;
            bodyCards[i].nftCount.text = "" + bodyIdMap[i].Count;

            // Debug.Log(faceIdMap[i].Count);
            // Debug.Log(hatIdMap[i].Count);
            // Debug.Log(bodyIdMap[i].Count);

            double faceNftsPrice = 0;
            double hatNftsPrice = 0;
            double bodyNftsPrice = 0;

            foreach (string nftID in faceIdMap[i]) {
                faceNftsPrice += nftDict[nftID].price;
            }
            faceCards[i].nftsTotalPrice.text = String.Format("${0:0.##}", faceNftsPrice);

            foreach (string nftID in hatIdMap[i]) {
                hatNftsPrice += nftDict[nftID].price;
            }
            hatCards[i].nftsTotalPrice.text = String.Format("${0:0.##}", hatNftsPrice);

            foreach (string nftID in bodyIdMap[i]) {
                bodyNftsPrice += nftDict[nftID].price;
            }
            bodyCards[i].nftsTotalPrice.text = String.Format("${0:0.##}", bodyNftsPrice);

        }


        // face1Card.nftCount.text = "" + faceIdMap[0].Count;
        // double nftsPrice = 0;
        // foreach (string nftID in faceIdMap[0]) {
        //     nftsPrice += nftDict[nftID].price;
        // }

        // face1Card.nftsTotalPrice.text = String.Format("${0:0.##}", nftsPrice);
        // // Green
        // face1Card.background.color = goodColor;
        // face1Card.background.color = Color.green;
    }

    double getProfitLossValue(List<string> toSellNFTIds) {
        double costPrice = PriceManager.getBuyPricePortfolioValueFromList(toSellNFTIds);
        double sellPrice = PriceManager.getSelectedPortfolioValue(toSellNFTIds);

        return sellPrice - costPrice;

    }
   

    void setProfitLossText() {
        changeColorSell();

        double costPrice = PriceManager.getBuyPricePortfolioValueFromList(toSellNFTIds);
        double sellPrice = PriceManager.getSelectedPortfolioValue(toSellNFTIds);

        double profitLossValue = getProfitLossValue(toSellNFTIds);

        if(profitLossValue > 0) {
            profitLossText.text = String.Format("Profit ${0:0.##}", profitLossValue);
            profitLossText.color = goodColor;
        } else if(profitLossValue < 0) {
            profitLossText.text = String.Format("Loss ${0:0.##}", -1 * profitLossValue);
            profitLossText.color = badColor;
        } else {
            profitLossText.text = "";
        }
        
    }

    void setTotalNumNFTsOwned() {
        totalNumNFTsOwned.text = "" + nftsOwned.Count;
    }

    void setTotalPortfolioValue() {
        totalPortfolioValue.text = String.Format("{0:0.##}", PriceManager.getPortfolioValue());
    }

    void setTotalNumNFTsSelected() {
        totalNumNFTsSelected.text = "" + toSellNFTIds.Count;
    }

    void setSelectedPortfolioValue() {
        selectedPortfolioValue.text = String.Format("{0:0.##}", PriceManager.getSelectedPortfolioValue(toSellNFTIds));
    }

    void setWalletValue() {
        walletValue.text = String.Format("{0:0.##}", PriceManager.walletValue);
    }

}

[System.Serializable]
public class sellCard {
    public TextMeshProUGUI nftCount;
    public TextMeshProUGUI nftsTotalPrice;
    public RawImage imagePart;
    public RawImage background;
    public RawImage checkMarkImg;
    public bool selected;
    // public Texture[] imageTextures = new Texture[4];
}