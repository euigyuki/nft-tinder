using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random=UnityEngine.Random;
using System;
using TMPro;
using UnityEngine.UI;
using System.Linq;

public class PriceManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Price;
    public TextMeshProUGUI BreakingNewsPositive;
    public TextMeshProUGUI BreakingNewsNegative;
    public TextMeshProUGUI CurrentTrendsPositive;
    public TextMeshProUGUI CurrentTrendsNegative;
    public TextMeshProUGUI FutureTrendsPositive;
    public TextMeshProUGUI FutureTrendsNegative;
    public TextMeshProUGUI PortfolioValueUiText;
    public TextMeshProUGUI NftNameUiText;
    public TextMeshProUGUI NftCategoryUiText;
    public TextMeshProUGUI NftsOwnedUiText;

    public int price = 2;
    public double walletValue = 30000;
    public double portfolioValue = 0;
    public int currentNftIdx = 0;

    public static PriceManager instance {get; private set;}
    // public List<string> nftsOwned = new List<string>();
    public HashSet<string> nftsOwned = new HashSet<string>();
    // public HashSet<string> nftsOwned;
    // trends
    public Dictionary<string, List<string>> currentTrends = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> prevFutureTrends = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> futureTrends = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> breakingNews = new Dictionary<string, List<string>>();

    // nfts to show
    public int totalNftsToPick = 50;
    // public string[] nftsToShow = new string[totalNftsToPick];
    public List<string> nftsToShow = new List<string>();

    private void Awake() {
        Debug.Log("In Awake for PriceManager");

        if (instance == null) {
            Debug.Log("Assigning to new instance");
            instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(NftsOwnedUiText);
        }
        else {
            Debug.Log("Destroying instance");
            Destroy(gameObject);
            // Destroy(this.NftsOwnedUiText)
        }

        // All the steps needed for each day/level
        populateFromJson();
        pickNftsToShow();
        changePrice();
        doStartUp();
        currentNftIdx = 0;
    }

    void Start() {
        Debug.Log("In start of PriceManager.");
        // populateFromJson();
        // Money.text = "Money: $"+walletValue.ToString();
        // genPrice();s
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    double getPortfolioValue() {
        double totalVal = 0.0;
        foreach (string key in nftsOwned) {
            totalVal += getNftPrice(key);
        }
        return totalVal;
        // return portfolioValue;
    }

    string computeStrFromList(List<string> strList) {
        return string.Join(",", strList.ToArray());
    }

    string getPosFutureTrends() {
        return computeStrFromList(futureTrends["Pos"]);
    }

    string getNegFutureTrends() {
        return computeStrFromList(futureTrends["Neg"]);
    }

    string getPosCurrentTrends() {
        return computeStrFromList(currentTrends["Pos"]);
    }

    string getNegCurrentTrends() {
        return computeStrFromList(currentTrends["Neg"]);
    }

    string getPosBreakingNews() {
        return computeStrFromList(breakingNews["Pos"]);
    }

    string getNegBreakingNews() {
        return computeStrFromList(breakingNews["Neg"]);
    }

    double getNftPrice(string nftId) {
        Nft nft = nftsDict[nftId];
        return nft.price;
    }

    void setWalletValueOnUi() {
        Money.text = "Money: $"+walletValue.ToString();
    }

    Nft getCurrentNft() {
        if (currentNftIdx == nftsToShow.Count) {
            Debug.Log("Run out of nfts to show.");
            // Call sell page
            string nftId = nftsToShow[currentNftIdx-1];
            return nftsDict[nftId];
        }

        string nftIdString = nftsToShow[currentNftIdx];
        return nftsDict[nftIdString];
    }

    public void buyNft() {
        Nft currentNft = getCurrentNft();
        string nftId = currentNft.nftId;
        double nftPrice = currentNft.price;
        if (walletValue - nftPrice < 0) {
            Debug.Log("Not enough money to buy Nft");
            return;
        }
        nftsOwned.Add(nftId);
        walletValue -= nftPrice;
        portfolioValue += nftPrice;
        setWalletValueOnUi();
        displayPortfolioValue();
        displayNftsOwnedOnUi();
        // get next to nft to show
        Nft nextNftToShow = getNextNftToShow();
        displayNftDetailsOnUi(nextNftToShow);
        // Debug.Log("Successfully bought: " + nftId);
    }

    void sellNft() {
        // double nftPrice = getNftPrice(nftId);
        Nft currentNft = getCurrentNft();
        string nftId = currentNft.nftId;
        double nftPrice = currentNft.price;
        nftsOwned.Remove(nftId);
        walletValue += nftPrice;
        setWalletValueOnUi();
    }

    Nft getNextNftToShow() {
        if (currentNftIdx == nftsToShow.Count) {
            Debug.Log("Run out of nfts to show.");
            // Call sell page
            string nftId = nftsToShow[currentNftIdx-1];
            return nftsDict[nftId];
        }

        string nftIdString = nftsToShow[currentNftIdx++];
        return nftsDict[nftIdString];
    }

    List<string> getRandomCategoriesForCurrentTrends(string type, int count) {
        // List<string> pickedCategories = new List<string>();
        HashSet<string> pickedCategories = new HashSet<string>();
        List<string> categoryOptions;
        int pickedCount = 0;
        if (type == "Pos") {
            // Pos case
            categoryOptions = new List<string>(prevFutureTrends["Pos"]);
            categoryOptions = categoryOptions.Union(breakingNews["Pos"]).ToList();

        } 
        else {
            // Neg case
            categoryOptions = new List<string>(prevFutureTrends["Neg"]);
            categoryOptions = categoryOptions.Union(breakingNews["Neg"]).ToList();
        }
        
        // might want to add check to remove same nfts picked multiple times
        int maxLen = categoryOptions.Count;
        if (categoryOptions.Count == 0) {
            Debug.Log("prevFutureTrends were empty");
            return new List<string>();
        }
        while (pickedCount < count) {
            int randomIdx = Random.Range(0, maxLen-1);
            string nftId = categoryOptions[randomIdx];
            pickedCategories.Add(nftId);
            pickedCount += 1;
        }
        currentTrends[type] = pickedCategories.ToList();
        displayCurrentTrends();
        return pickedCategories.ToList();
    }

    void displayCurrentTrends() {
        Debug.Log("Will display current trends here.");
        CurrentTrendsNegative.text = "-VE: " + getNegCurrentTrends();
        CurrentTrendsPositive.text = "+VE: " + getPosCurrentTrends();
    }

    void buildNewFutureTrends(int count) {
        List<string> posPrevFutureTrends = prevFutureTrends["Pos"].Union(futureTrends["Pos"]).ToList();
        List<string> negPrevFutureTrends = prevFutureTrends["Neg"].Union(futureTrends["Neg"]).ToList();

        
        prevFutureTrends["Pos"] = posPrevFutureTrends;
        prevFutureTrends["Neg"] = negPrevFutureTrends;

        // Maybe convert this to hashMap later
        int pickedCount = 0;
        // int maxLen = nftsDict.Count;
        // string[] nftIdsList = nftsDict.Keys.ToArray();
        int maxLen = subCategories.Length;

        List<string> pickedSubCategories = new List<string>();

        // set pos values
        while(pickedCount < count) { 
            int randomIdx = Random.Range(0, maxLen-1);
            // string nftId = nftIdsList[randomIdx];
            string subCategoryId = subCategories[randomIdx];
            if (pickedSubCategories.Contains(subCategoryId)) {
                continue;
            }

            pickedCount += 1;
            pickedSubCategories.Add(subCategoryId);
        }

        futureTrends["Pos"] = pickedSubCategories;

        // set neg values
        pickedSubCategories = new List<string>();
        pickedCount = 0;

        while(pickedCount < count) { 
            int randomIdx = Random.Range(0, maxLen-1);
            string subCategoryId = subCategories[randomIdx];
            
            if (pickedSubCategories.Contains(subCategoryId)) {
                continue;
            }

            if (futureTrends["Pos"].Contains(subCategoryId)) {
                continue;
            }

            pickedCount += 1;
            pickedSubCategories.Add(subCategoryId);
        }

        futureTrends["Neg"] = pickedSubCategories;
        displayFutureTrends();
    }

    void displayFutureTrends() {
        FutureTrendsNegative.text = "-VE: " + getNegFutureTrends();
        FutureTrendsPositive.text = "+VE: " + getPosFutureTrends();
    }

    double getPriceIncFactor(string nftId) {
        // Can implement changes to how the factor to increase the prices
        double factor = Random.Range(0, 100) / 100;
        return factor;
    }

    void changePricesForNfts(List<string> nftCategories, string operation) {

        foreach (string category in nftCategories) {
            foreach(string nftId in subCategoryNftMapper[category]){
                double currentNftPrice = nftsDict[nftId].price;
                string tempString = String.Format("Nft: {0} previous price: {1}", nftId, currentNftPrice);
                Debug.Log(tempString);
                double priceIncFactor = getPriceIncFactor(nftId);
                if (operation == "+") {
                    currentNftPrice += (currentNftPrice * priceIncFactor);
                }
                else {
                    currentNftPrice -= (currentNftPrice * priceIncFactor);
                }
                tempString = String.Format("Nft: {0} new price: {1}", nftId, currentNftPrice);
                Debug.Log(tempString);
                nftsDict[nftId].price = currentNftPrice;
            }
        }
    }

    void changePrice() {
        // copy futureTrends into currentTrends
        buildNewFutureTrends(3);

        // May change to more than one categpory later
        List<string> nftCategoryToIncPrice = getRandomCategoriesForCurrentTrends("Pos", 1);
        List<string> nftCategoryToDecPrice = getRandomCategoriesForCurrentTrends("Neg", 1);

        changePricesForNfts(nftCategoryToIncPrice, "+");
        changePricesForNfts(nftCategoryToIncPrice, "-");

    }

    void pickNftsToShow() {
        int pickedNftsCount = 0;
        HashSet<string> nftsPickedSet = new HashSet<string>();

        int noOfCategories = subCategoryNftMapper.Count;
        int eachCategoryCount = Convert.ToInt32(noOfCategories * 0.2);

        foreach (string key in subCategoryNftMapper.Keys) {
            int nftCountForCategory = 0;
            int prevNftCountForCategory = 0;
            int noChangeCount = 0;
            // string[] nftsForCategory = subCategoryNftMapper[key].
            int totalNftsInCategory = subCategoryNftMapper[key].Count;
            // Debug.Log(String.Format("totalNftsInCategory: {0}", totalNftsInCategory));
            int totalToPick = Convert.ToInt32(totalNftsInCategory * 0.2);
            // Debug.Log(String.Format("Total to pick: {0}", totalToPick));
            while (nftCountForCategory < totalToPick) {
                int randomIdx = Random.Range(0, totalNftsInCategory-1);
                string nftId = subCategoryNftMapper[key][randomIdx];
                
                if (nftsPickedSet.Contains(nftId)) {
                    continue;
                }

                if (nftsOwned.Contains(nftId)) {
                    continue;
                }

                nftCountForCategory += 1;

                if (nftCountForCategory == prevNftCountForCategory) {
                    noChangeCount += 1;
                }

                if (noChangeCount >= 100) {
                    break;
                }

                prevNftCountForCategory = nftCountForCategory;
                nftsPickedSet.Add(nftId);
            }
        }

        nftsToShow = nftsPickedSet.ToList();
        currentNftIdx = 0;
    }

    void populateFromJson() {

        // populating mainCategories and subCategories

        mainCategories = JsonUtility.FromJson<MainCategory>(CategoryJson.text);
        int reqLen = 0;
        reqLen += mainCategories.mainCategory1.Length;
        reqLen += mainCategories.mainCategory2.Length;
        reqLen += mainCategories.mainCategory3.Length;
        reqLen += mainCategories.mainCategory4.Length;
        reqLen += mainCategories.mainCategory5.Length;
        reqLen += mainCategories.mainCategory6.Length;
        reqLen += mainCategories.mainCategory7.Length;
        reqLen += mainCategories.mainCategory8.Length;
        reqLen += mainCategories.mainCategory9.Length;
        reqLen += mainCategories.mainCategory10.Length;
        
        Debug.Log($"The reqLen is : {reqLen}");

        // create an array of size reqLen for subCategories
        subCategories = new string[reqLen];
        int curIdx = 0;
        for (int i=0; i < mainCategories.mainCategory1.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory1[i];
        }

        for (int i=0; i < mainCategories.mainCategory2.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory2[i];
        }

        for (int i=0; i < mainCategories.mainCategory3.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory3[i];
        }

        for (int i=0; i < mainCategories.mainCategory4.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory4[i];
        }

        for (int i=0; i < mainCategories.mainCategory5.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory5[i];
        }

        for (int i=0; i < mainCategories.mainCategory6.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory6[i];
        }

        for (int i=0; i < mainCategories.mainCategory7.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory7[i];
        }

        for (int i=0; i < mainCategories.mainCategory8.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory8[i];
        }

        for (int i=0; i < mainCategories.mainCategory9.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory9[i];
        }

        for (int i=0; i < mainCategories.mainCategory10.Length; i++) {
            subCategories[curIdx++] = mainCategories.mainCategory10[i];
        }

        // populating NFTs

        nftsList = JsonUtility.FromJson<Nfts>(NftsJson.text);

        for (int i=0; i<nftsList.nfts.Length; i++) {
            Nft nft = nftsList.nfts[i];
            nftsDict.Add(nft.nftId, nft);
            addNftToMappers(nft);
        }

        // Debug.Log("The following are the (key, value) pairs in mainCategoryNftMapper");
        // foreach (string key in mainCategoryNftMapper.Keys)
        // {
        //     Debug.Log(key);
        // }

        // Debug.Log("The following are the (key, value) pairs in subCategoryNftMapper");

        // foreach (string key in subCategoryNftMapper.Keys)
        // {
        //     Debug.Log(key);
        // }

        // populate all trends
        if (!currentTrends.ContainsKey("Pos")) {
            currentTrends.Add("Pos", new List<string>());
        }

        if (!currentTrends.ContainsKey("Neg")) {
            currentTrends.Add("Neg", new List<string>());
        }

        if (!futureTrends.ContainsKey("Pos")) {
            futureTrends.Add("Pos", new List<string>());
        }
        
        if (!futureTrends.ContainsKey("Neg")) {
            futureTrends.Add("Neg", new List<string>());
        }

        if (!breakingNews.ContainsKey("Pos")) {
            breakingNews.Add("Pos", new List<string>());
        }

        if (!breakingNews.ContainsKey("Neg")) {
            breakingNews.Add("Neg", new List<string>());
        }

        if (!prevFutureTrends.ContainsKey("Pos")) {
            prevFutureTrends.Add("Pos", new List<string>());
        }

        if (!prevFutureTrends.ContainsKey("Neg")) {
            prevFutureTrends.Add("Neg", new List<string>());
        }
        
        // if (nftsOwned == null) {
        //     nftsOwned = new HashSet<string>();
        // }
        
    }

    void addNftToMappers(Nft nft) {
        string mainCategoryKey = nft.mainCategory;
        string subCategoryKey = nft.subCategory;
        // Debug.Log(mainCategoryKey);
        // Debug.Log(subCategoryKey);
        
        // ADD TO MAIN CATEGORY MAPPER
        if (mainCategoryNftMapper.ContainsKey(mainCategoryKey)) {
            // Debug.Log("mainCategoryKey already contains the key.");
            List<string> list = mainCategoryNftMapper[mainCategoryKey];
            list.Add(nft.nftId);
        }
        else {
           
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            // Debug.Log("mainCategoryKey creating the key.");
            mainCategoryNftMapper.Add(mainCategoryKey, list);
        }

        // ADD TO SUB CATEGORY MAPPER
        if (subCategoryNftMapper.ContainsKey(subCategoryKey)) {
            // Debug.Log("subCategoryNftMapper already contains the key.");
            List<string> list = subCategoryNftMapper[subCategoryKey];

            list.Add(nft.nftId);
        }
        else {
            // Debug.Log("subCategoryNftMapper creating the key.");
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            subCategoryNftMapper.Add(subCategoryKey, list);
        }
    }

    public void passNft() {
        // genPrice();
        Nft nextNftToShow = getNextNftToShow();
        displayNftDetailsOnUi(nextNftToShow);
    }

    public void displayNftDetailsOnUi(Nft nft) {
        displayNftPriceOnUi(nft.price);
        displayNftNameOnUi(nft.nftId);
        displayNftCategoryOnUi(nft.subCategory);
    }

    public void showFirstNftDetails() {
        passNft();
    }
    
    public void doStartUp() {
        showFirstNftDetails();
        displayPortfolioValue();
        displayNftsOwnedOnUi();
    }

    public void displayNftPriceOnUi(double price) {
        Price.text = "$"+price.ToString();
    }

    public void displayNftsOwnedOnUi() {
        Debug.Log("displayNftsOwnedOnUi: " + computeStrFromList(nftsOwned.ToList()));
        NftsOwnedUiText.text = "Buys: " + computeStrFromList(nftsOwned.ToList());
    }

    public void displayPortfolioValue() {
        PortfolioValueUiText.text = "Portfolio: $"+getPortfolioValue().ToString();
    }

    public void displayNftNameOnUi(string name) {
        NftNameUiText.text = name;
    }

    public void displayNftCategoryOnUi(string category) {
        NftCategoryUiText.text = category;
    }
    
    // public void SubstractMoney() {
        // walletValue-=price;
        // Money.text = "Money: $"+walletValue.ToString();
        // genPrice();
    // }

    public void genPrice(){
        price = Random.Range(500,3000);
        Price.text = "$"+price.ToString();
    }

    // JSON Serialization

    
    public TextAsset CategoryJson;
    // all vars related to categories
    public string[] subCategories;
    public MainCategory mainCategories = new MainCategory();

    [System.Serializable]
    public class MainCategory {
        public string[] mainCategory1;
        public string[] mainCategory2;
        public string[] mainCategory3;
        public string[] mainCategory4;
        public string[] mainCategory5;
        public string[] mainCategory6;
        public string[] mainCategory7;
        public string[] mainCategory8;
        public string[] mainCategory9;
        public string[] mainCategory10;
    }

    public Nfts nftsList = new Nfts();
    public TextAsset NftsJson;
    
    // All vars related to NFTs
    public Dictionary<string, Nft> nftsDict = new Dictionary<string, Nft>();
    public Dictionary<string, List<string>> mainCategoryNftMapper = new Dictionary<string, List<string>>();
    public Dictionary<string, List<string>> subCategoryNftMapper = new Dictionary<string, List<string>>();

    [System.Serializable]
    public class Nft {
        public string nftId;
        public double price;
        public int rank;
        public int noOfBuyers;
        public string mainCategory;
        public string subCategory;
    }

    [System.Serializable]
    public class Nfts {
        public Nft[] nfts; 
    }
}
