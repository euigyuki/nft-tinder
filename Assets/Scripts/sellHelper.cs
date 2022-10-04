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

    public Button sellButton;

    public TextMeshProUGUI totalNumNFTsOwned;
    public TextMeshProUGUI totalPortfolioValue;
    public TextMeshProUGUI totalNumNFTsSelected;
    public TextMeshProUGUI selectedPortfolioValue;

    // Selected Color: RGB(0,0,0), 150
    public Color selectedColor;
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

    // Start is called before the first frame update
    void Start() {

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

    }

    // Update is called once per frame
    void Update()
    {
        setTotalNumNFTsOwned();
        setTotalPortfolioValue();
        setTotalNumNFTsSelected();
        setSelectedPortfolioValue();

        if(toSellNFTIds.Count > 0) {
            sellButton.GetComponent<Button>().interactable = true;
        } else {
            sellButton.GetComponent<Button>().interactable = false;
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
            }
            if(hatIdMap[i].Count == 0) {
                hats[i].GetComponent<Button>().interactable = false;
            }
            if(bodyIdMap[i].Count == 0) {
                bodies[i].GetComponent<Button>().interactable = false;
            }
        }

    }

    void SellSelectedNFTs() {
        PriceManager.sellNftsFromList(toSellNFTIds);
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

            // faceSelected[i] = false;
            // faces[i].GetComponent<Image>().color = Color.white;
            // hatSelected[i] = false;
            // hats[i].GetComponent<Image>().color = Color.white;
            // bodySelected[i] = false;
            // bodies[i].GetComponent<Image>().color = Color.white;
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
                faces[k].GetComponent<Image>().color = selectedColor;
            }
            else {
                faces[k].GetComponent<Image>().color = Color.white;
            }
        } else if(type.Equals("hat")) {
            hatSelected[k] = !hatSelected[k];
            if(hatSelected[k]){
                hats[k].GetComponent<Image>().color = selectedColor;
            } else {
                hats[k].GetComponent<Image>().color = Color.white;
            }
        } else if(type.Equals("body")) {
            bodySelected[k] = !bodySelected[k];
            if(bodySelected[k]){
                bodies[k].GetComponent<Image>().color = selectedColor;
            } else {
                bodies[k].GetComponent<Image>().color = Color.white;
            }
        }
        
        UpdateNftsToSell();

    }

    void setTotalNumNFTsOwned() {
        totalNumNFTsOwned.text = "Number of NFTs Owned: " + nftsOwned.Count;
    }

    void setTotalPortfolioValue() {
        totalPortfolioValue.text = String.Format("Total Value of Portfolio: ${0:0.##}", PriceManager.getPortfolioValue());
    }

    void setTotalNumNFTsSelected() {
        totalNumNFTsSelected.text = "Number of NFTs Selected: " + toSellNFTIds.Count;
    }

    void setSelectedPortfolioValue() {
        selectedPortfolioValue.text = String.Format("Value of Selected NFTs: ${0:0.##}", PriceManager.getSelectedPortfolioValue(toSellNFTIds));
    }

    // public TextMeshProUGUI totalNumNFTsOwned;
    // public TextMeshProUGUI totalPortfolioValue;
    // public TextMeshProUGUI totalNumNFTsSelected;
    // public TextMeshProUGUI selectedPortfolioValue;

}
