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
    
    // public TextMeshProUGUI Money;
    // public TextMeshProUGUI Price;

    public static int price = 2;
    public static double walletValue = 10000;
    public static double portfolioValue = 0;
    public static int currentNftIdx = 0;
    public static bool jsonLoaded = false;
    public static int currentDay = 0;
    
    public static bool offerDiscount = false;

    public static PriceManager instance {get; private set;}
    public static HashSet<string> nftsOwned = new HashSet<string>();
    public static Dictionary<string, double> nftsBuyPrice = new Dictionary<string, double>();
    
    // trends
    // public Dictionary<string, List<string>> currentTrends = new Dictionary<string, List<string>>();
    // public Dictionary<string, List<string>> prevFutureTrends = new Dictionary<string, List<string>>();
    // public Dictionary<string, List<string>> futureTrends = new Dictionary<string, List<string>>();
    // public Dictionary<string, List<string>> breakingNews = new Dictionary<string, List<string>>();

    public static int futureTrendingFacePos = -1;
    public static int futureTrendingFaceNeg = -1;
    public static int futureTrendingBodyPos = -1;
    public static int futureTrendingBodyNeg = -1;
    public static int futureTrendingHatPos = -1;
    public static int futureTrendingHatNeg = -1;

    public static int currentTrendingFacePos = -1;
    public static int currentTrendingFaceNeg = -1;
    public static int currentTrendingBodyPos = -1;
    public static int currentTrendingBodyNeg = -1;
    public static int currentTrendingHatPos = -1;
    public static int currentTrendingHatNeg = -1;

    public static List<int> prevFutureTrendingFacePos = new List<int>();
    public static List<int> prevFutureTrendingFaceNeg = new List<int>();
    public static List<int> prevFutureTrendingBodyPos = new List<int>();
    public static List<int> prevFutureTrendingBodyNeg = new List<int>();
    public static List<int> prevFutureTrendingHatPos = new List<int>();
    public static List<int> prevFutureTrendingHatNeg = new List<int>();

    // nfts to show
    public static int totalNftsToPick = 50;
    // public string[] nftsToShow = new string[totalNftsToPick];
    public static List<string> nftsToShow = new List<string>();

    private void Awake() {
        // Debug.Log("In Awake for PriceManager");

        // if (instance == null) {
        //     Debug.Log("Assigning to new instance");
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else {
        //     Debug.Log("Destroying instance");
        //     Destroy(gameObject);
        //     // Destroy(this.NftsOwnedUiText)
        // }

        // // All the steps needed for each day/level
        // populateFromJson();
        // pickNftsToShow();
        // changePrice();
        // currentNftIdx = 0;
    }

    void Start() {
        
    }

    public static void resetEverything() {
        price = 2;
        walletValue = 10000;
        portfolioValue = 0;
        currentNftIdx = 0;
        // jsonLoaded = false;
        currentDay = 0;

        nftsOwned = new HashSet<string>();
        futureTrendingFacePos = -1;
        futureTrendingFaceNeg = -1;
        futureTrendingBodyPos = -1;
        futureTrendingBodyNeg = -1;
        futureTrendingHatPos = -1;
        futureTrendingHatNeg = -1;

        currentTrendingFacePos = -1;
        currentTrendingFaceNeg = -1;
        currentTrendingBodyPos = -1;
        currentTrendingBodyNeg = -1;
        currentTrendingHatPos = -1;
        currentTrendingHatNeg = -1;

        prevFutureTrendingFacePos = new List<int>();
        prevFutureTrendingFaceNeg = new List<int>();
        prevFutureTrendingBodyPos = new List<int>();
        prevFutureTrendingBodyNeg = new List<int>();
        prevFutureTrendingHatPos = new List<int>();
        prevFutureTrendingHatNeg = new List<int>();

        nftsToShow = new List<string>();

        // nftsList = new Nfts();
        // NftsJson = Resources.Load<TextAsset>("nfts");
        
        // nftsDict = new Dictionary<string, Nft>();
        
        // faceNftMapper = new Dictionary<int, List<string>>();
        // bgNftMapper = new Dictionary<int, List<string>>();
        // headNftMapper = new Dictionary<int, List<string>>();
        // bodyNftMapper = new Dictionary<int, List<string>>();
        // facePropNftMapper = new Dictionary<int, List<string>>();

        pickNftsToShow();

    }

    public static string recommendToBuy() {
        Nft currentNft = getCurrentNft();
        if (futureTrendingFacePos == currentNft.imagePics[0] || futureTrendingHatPos == currentNft.imagePics[3] || futureTrendingBodyPos == currentNft.imagePics[2]) {
            return "May lead to profits!!";
        }

        if (futureTrendingFaceNeg == currentNft.imagePics[0] || futureTrendingHatNeg == currentNft.imagePics[3] || futureTrendingBodyNeg == currentNft.imagePics[2]) {
            return "May lead to losses!!";
        }

        return "";
    }

    public static double buyProb() {
        Nft currentNft = getCurrentNft();

        double total = 0;

        if (futureTrendingFacePos == currentNft.imagePics[0]) {
            total += 1;
        }

        if (futureTrendingHatPos == currentNft.imagePics[2]) {
            total += 1;
        }

        if (futureTrendingBodyPos == currentNft.imagePics[3]) {
            total += 1;
        }
        return total / 3.00;
    }

    public static double sellProb() {
        Nft currentNft = getCurrentNft();

        double total = 0;

        if (futureTrendingFaceNeg == currentNft.imagePics[0]) {
            total += 1;
        }

        if (futureTrendingHatNeg == currentNft.imagePics[2]) {
            total += 1;
        }

        if (futureTrendingBodyNeg == currentNft.imagePics[3]) {
            total += 1;
        }
        return total / 3.00;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public static void setUp() {
        Debug.Log("SetUp called now!");
        currentDay += 1;
        populateFromJson();
        pickNftsToShow();
        changePrice();
        currentNftIdx = 0;
    }

    public static double getPortfolioValue() {
        double totalVal = 0.0;
        foreach (string key in nftsOwned) {
            totalVal += getNftPrice(key);
        }
        return totalVal;
    }

    public static double getSelectedPortfolioValue(List<string> nftIds) {
        double totalVal = 0.0;
        foreach (string id in nftIds) {
            totalVal += getNftPrice(id);
        }
        return totalVal;
    }

    public static double getBuyPricePortfolioValueFromList(List<string> nftIds) {
        double totalVal = 0.0;
        foreach (string key in nftIds) {
            totalVal += getNftBuyPrice(key);
        }
        return totalVal;
    }

    static string computeStrFromList(List<string> strList) {
        return string.Join(",", strList.ToArray());
    }

    public static double getCurrentNftPrice() {
        if (nftsToShow.Count == 0) {
            return 0;
        }
        Nft currentNft = getCurrentNft();
        return currentNft.price;
    }

    static double getNftPrice(string nftId) {
        Nft nft = nftsDict[nftId];
        if (offerDiscount) {
            return nft.price * 0.8;
        }
        return nft.price;
    }

    static double getNftBuyPrice(string nftId) {
        if (nftsBuyPrice.ContainsKey(nftId)) {
            return nftsBuyPrice[nftId];
        }
        return 0;
    }

    static void setWalletValueOnUi() {
        // Money.text = "Money: $"+walletValue.ToString();
    }

    public static Nft getCurrentNft() {
        if (currentNftIdx == nftsToShow.Count) {
            Debug.Log("Run out of nfts to show.");
            // Call sell page
            string nftId = nftsToShow[currentNftIdx-1];
            return nftsDict[nftId];
        }

        string nftIdString;
        if (currentNftIdx == 0) {
            nftIdString = nftsToShow[currentNftIdx];
        }
        else {
            nftIdString = nftsToShow[currentNftIdx-1];
        }
        return nftsDict[nftIdString];
    }

    public static string getNftId() {
        if (nftsToShow.Count == 0) {
            return "";
        }
        return getCurrentNft().nftId;
    }

    static public int[] getNftPicsIdxs() {
        Nft currentNft = getCurrentNft();
        return currentNft.imagePics;
    }

    public static void sendBuyTrendsToFirebase() {
        string recomm = recommendToBuy();
        if (recomm == "May lead to profits!!") {
            StaticAnalytics.incPosTrendingBuy();
        }
        else if (recomm == "May lead to losses!!") {
            StaticAnalytics.incNegTrendingBuy();
        }
        else {
            StaticAnalytics.incNeutralTrendingBuy();
        }
    }

    public static void buyNft() {
        Nft currentNft = getCurrentNft();
        string nftId = currentNft.nftId;
        double nftPrice = currentNft.price;
        // Debug.Log("Buying the nft: " + nftId);
        // TODO: Uncomment Weyei code once confirming that the scene is the latest one
        if (walletValue - nftPrice < 0) {
            Debug.Log("Not enough money to buy Nft");
            return;
        }
        sendBuyTrendsToFirebase();
        nftsOwned.Add(nftId);
        walletValue -= nftPrice;
        portfolioValue += nftPrice;
        // RectTransform transform = MoneyBar.instance.picture;
        // transform.anchoredPosition =  new Vector2(transform.anchoredPosition.x - (float)(walletValue/6000), transform.anchoredPosition.y);
        
        if (nftsBuyPrice.ContainsKey(nftId)) {
            nftsBuyPrice[nftId] = nftPrice;
        }
        else {
            nftsBuyPrice.Add(nftId, nftPrice);
        }

        // get next to nft to show
        Nft nextNftToShow = getNextNftToShow();
        // Debug.Log("Successfully bought: " + nftId);
        // Debug.Log("Buy prob: " + buyProb().ToString());
        // Debug.Log("Sell prob: " + sellProb().ToString());
    }

    public static void sellNft(string nftId) {
        double nftPrice = getNftPrice(nftId);
        nftsOwned.Remove(nftId);
        if (nftsBuyPrice.ContainsKey(nftId)) {
            nftsBuyPrice.Remove(nftId);
        }
        walletValue += nftPrice;
    }

    public static void sellNftsFromList(List<string> nftsToSell) {
        foreach (string nftIdStr in nftsToSell) {
            sellNft(nftIdStr);
        }
    }

    public static List<string> getNftsOwnedAsList() {
        return nftsOwned.ToList();
    }

    public static Dictionary<string, Nft> getNftsDict() {
        return nftsDict;
    }

    static Nft getNextNftToShow() {
        if (currentNftIdx == nftsToShow.Count) {
            Debug.Log("Run out of nfts to show.");
            // Call sell page
            string nftId = nftsToShow[currentNftIdx-1];
            return nftsDict[nftId];
        }

        string nftIdString = nftsToShow[currentNftIdx++];
        return nftsDict[nftIdString];
    }

    static void changePricesForNfts(string type, string operation, int category) {

        if (type == "face") {
            if (faceNftMapper.ContainsKey(category)) {
                foreach (string nftId in faceNftMapper[category]) {
                    double currentNftPrice = nftsDict[nftId].price;
                    string tempString = String.Format("Nft: {0} previous price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                    double priceIncFactor = getPriceIncFactor(nftId);
                    if (operation == "+") {
                        currentNftPrice += (currentNftPrice * priceIncFactor);
                    }
                    else {
                        currentNftPrice = currentNftPrice * priceIncFactor;
                    }
                    nftsDict[nftId].price = currentNftPrice;
                    tempString = String.Format("Nft: {0} AFTER price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                }
            }
        }
        else if (type == "body") {
            if (bodyNftMapper.ContainsKey(category)) {
                foreach (string nftId in bodyNftMapper[category]) {
                    double currentNftPrice = nftsDict[nftId].price;
                    string tempString = String.Format("Nft: {0} previous price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                    double priceIncFactor = getPriceIncFactor(nftId);
                    if (operation == "+") {
                        currentNftPrice += (currentNftPrice * priceIncFactor);
                    }
                    else {
                        currentNftPrice = currentNftPrice * priceIncFactor;
                    }
                    nftsDict[nftId].price = currentNftPrice;
                    tempString = String.Format("Nft: {0} AFTER price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                }
            }
        }
        else if (type == "head") {
            if (headNftMapper.ContainsKey(category)) {
                foreach (string nftId in headNftMapper[category]) {
                    double currentNftPrice = nftsDict[nftId].price;
                    string tempString = String.Format("Nft: {0} previous price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                    double priceIncFactor = getPriceIncFactor(nftId);
                    if (operation == "+") {
                        currentNftPrice += (currentNftPrice * priceIncFactor);
                    }
                    else {
                        currentNftPrice = currentNftPrice * priceIncFactor;
                    }
                    nftsDict[nftId].price = currentNftPrice;
                    tempString = String.Format("Nft: {0} AFTER price: {1}", nftId, currentNftPrice);
                    // Debug.Log(tempString);
                }
            }
        }
    }

    static void buildNewFutureTrends() {

        // face, body, head
        int randomFacePosIdx = Random.Range(0, 3);
        int randomBodyPosIdx = Random.Range(0, 3);
        int randomHatPosIdx = Random.Range(0, 3);

        int randomFaceNegIdx = Random.Range(0, 3);
        int randomBodyNegIdx = Random.Range(0, 3);
        int randomHatNegIdx = Random.Range(0, 3);
        // Debug.Log("Future Trends Before");
        // Debug.Log("futureTrendingFacePos: " + futureTrendingFacePos.ToString());
        // Debug.Log("futureTrendingFaceNeg: " + futureTrendingFaceNeg.ToString());

        // Debug.Log("futureTrendingBodyPos: " + futureTrendingBodyPos.ToString());
        // Debug.Log("futureTrendingBodyNeg: " + futureTrendingBodyNeg.ToString());

        // Debug.Log("futureTrendingHatPos: " + futureTrendingHatPos.ToString());
        // Debug.Log("futureTrendingHatNeg: " + futureTrendingHatNeg.ToString());
        // Debug.Log("------------------------------");
        // Debug.Log("Current Trends Before");
        // Debug.Log("currentTrendingFacePos: " + currentTrendingFacePos.ToString());
        // Debug.Log("currentTrendingFaceNeg: " + currentTrendingFaceNeg.ToString());

        // Debug.Log("currentTrendingBodyPos: " + currentTrendingBodyPos.ToString());
        // Debug.Log("currentTrendingBodyNeg: " + currentTrendingBodyNeg.ToString());

        // Debug.Log("currentTrendingHatPos: " + currentTrendingHatPos.ToString());
        // Debug.Log("currentTrendingHatNeg: " + currentTrendingHatNeg.ToString());
        // Debug.Log("------------------------------");

        while (randomFacePosIdx == randomFaceNegIdx) {
            randomFaceNegIdx = Random.Range(0, 3);
        }

        while (randomBodyPosIdx == randomBodyNegIdx) {
            randomBodyNegIdx = Random.Range(0, 3);
        }

        while (randomHatPosIdx == randomHatNegIdx) {
            randomHatNegIdx = Random.Range(0, 3);
        }

        if (futureTrendingFacePos != -1) {
            prevFutureTrendingFacePos.Add(futureTrendingFacePos);
        }

        if (futureTrendingFaceNeg != -1) {
            prevFutureTrendingFaceNeg.Add(futureTrendingFaceNeg);
        }

        if (futureTrendingBodyPos != -1) {
            prevFutureTrendingBodyPos.Add(futureTrendingBodyPos);
        }

        if (futureTrendingBodyNeg != -1) {
            prevFutureTrendingBodyNeg.Add(futureTrendingBodyNeg);
        }
        
        if (futureTrendingHatPos != -1) {
            prevFutureTrendingHatPos.Add(futureTrendingHatPos);
        }

        if (futureTrendingHatNeg != -1) {
            prevFutureTrendingHatNeg.Add(futureTrendingHatNeg);
        }
        
        futureTrendingFacePos = randomFacePosIdx;
        futureTrendingFaceNeg = randomFaceNegIdx;

        futureTrendingBodyPos = randomBodyPosIdx;
        futureTrendingBodyNeg = randomBodyNegIdx;

        futureTrendingHatPos = randomHatPosIdx;
        futureTrendingHatNeg = randomHatNegIdx;

        int incPriceFor = Random.Range(0, 2);

        if (incPriceFor == 0) {
            // inc for face
            if (prevFutureTrendingFacePos.Count != 0) {
                int categoryToInc = Random.Range(0, prevFutureTrendingFacePos.Count-1);
                categoryToInc = prevFutureTrendingFacePos[categoryToInc];
                changePricesForNfts("face", "+", categoryToInc);
                currentTrendingFacePos = categoryToInc;
                currentTrendingBodyPos = -1;
                currentTrendingHatPos = -1;
            }
        }
        else if (incPriceFor == 1) {
            // inc for body
            if (prevFutureTrendingBodyPos.Count != 0) {
                int categoryToInc = Random.Range(0, prevFutureTrendingBodyPos.Count-1);
                categoryToInc = prevFutureTrendingBodyPos[categoryToInc];
                changePricesForNfts("body", "+", categoryToInc);
                currentTrendingFacePos = -1;
                currentTrendingBodyPos = categoryToInc;
                currentTrendingHatPos = -1;
            }
        }
        else {
            // inc for head
            if (prevFutureTrendingHatPos.Count != 0) {
                int categoryToInc = Random.Range(0, prevFutureTrendingHatPos.Count-1);
                categoryToInc = prevFutureTrendingHatPos[categoryToInc];
                changePricesForNfts("head", "+", categoryToInc);
                currentTrendingFacePos = -1;
                currentTrendingBodyPos = -1;
                currentTrendingHatPos = categoryToInc;
            }
        }

        int decPriceFor = Random.Range(0, 2);

        while (incPriceFor == decPriceFor) {
            decPriceFor = Random.Range(0, 2);
        }

        if (decPriceFor == 0) {
            // inc for face
            if (prevFutureTrendingFaceNeg.Count != 0) {
                int categoryToDec = Random.Range(0, prevFutureTrendingFaceNeg.Count-1);
                categoryToDec = prevFutureTrendingFaceNeg[categoryToDec];
                changePricesForNfts("face", "-", categoryToDec);
                currentTrendingFaceNeg = categoryToDec;
                currentTrendingBodyNeg = -1;
                currentTrendingHatNeg = -1;
            }
        }
        else if (decPriceFor == 1) {
            // dec for body
            if (prevFutureTrendingBodyNeg.Count != 0) {
                int categoryToDec = Random.Range(0, prevFutureTrendingBodyNeg.Count-1);
                categoryToDec = prevFutureTrendingBodyNeg[categoryToDec];
                changePricesForNfts("body", "-", categoryToDec);
                currentTrendingFaceNeg = -1;
                currentTrendingBodyNeg = categoryToDec;
                currentTrendingHatNeg = -1;
            }
        }
        else {
            // dec for head
            if (prevFutureTrendingHatNeg.Count != 0) {
                int categoryToDec = Random.Range(0, prevFutureTrendingHatNeg.Count-1);
                categoryToDec = prevFutureTrendingHatNeg[categoryToDec];
                changePricesForNfts("head", "-", categoryToDec);
                currentTrendingFaceNeg = -1;
                currentTrendingBodyNeg = -1;
                currentTrendingHatNeg = categoryToDec;
            }
        }

        // Debug.Log("Future Trends After");
        // Debug.Log("futureTrendingFacePos: " + futureTrendingFacePos.ToString());
        // Debug.Log("futureTrendingFaceNeg: " + futureTrendingFaceNeg.ToString());

        // Debug.Log("futureTrendingBodyPos: " + futureTrendingBodyPos.ToString());
        // Debug.Log("futureTrendingBodyNeg: " + futureTrendingBodyNeg.ToString());

        // Debug.Log("futureTrendingHatPos: " + futureTrendingHatPos.ToString());
        // Debug.Log("futureTrendingHatNeg: " + futureTrendingHatNeg.ToString());
        // Debug.Log("------------------------------");
        // Debug.Log("Current Trends After");
        // Debug.Log("currentTrendingFacePos: " + currentTrendingFacePos.ToString());
        // Debug.Log("currentTrendingFaceNeg: " + currentTrendingFaceNeg.ToString());

        // Debug.Log("currentTrendingBodyPos: " + currentTrendingBodyPos.ToString());
        // Debug.Log("currentTrendingBodyNeg: " + currentTrendingBodyNeg.ToString());

        // Debug.Log("currentTrendingHatPos: " + currentTrendingHatPos.ToString());
        // Debug.Log("currentTrendingHatNeg: " + currentTrendingHatNeg.ToString());

    }


    static double getPriceIncFactor(string nftId) {
        // Can implement changes to how the factor to increase the prices
        double factor = Random.Range(5, 100);
        // Debug.Log("Helooooo: " + factor / 100.0);
        return factor / 100.0;
    }

    static void changePrice() {
        // copy futureTrends into currentTrends
        buildNewFutureTrends();
    }

    static void pickNftsToShow() {
        // Debug.Log("Entering pickNftsToShow");
        if (nftsToShow.Count > 0) {
            return;
        }
        int pickedNftsCount = 0;
        HashSet<string> nftsPickedSet = new HashSet<string>();
        int totalNftCount = nftsList.nfts.Length;
        int noChangeCount = 0;
        int prevCount = 0;
        while (pickedNftsCount < 1000) {
            int randomIdx = Random.Range(0, totalNftCount-1);
            Nft pickedNft = nftsList.nfts[randomIdx];
            string nftId = pickedNft.nftId;

            if (!nftsPickedSet.Contains(nftId) && !nftsOwned.Contains(nftId)) {
                pickedNftsCount += 1;
                nftsPickedSet.Add(nftId);
            }

            if (prevCount == pickedNftsCount) {
                noChangeCount += 1;
            }

            if (noChangeCount > 150) {
                break;
            }

            prevCount = pickedNftsCount;

        }

        nftsToShow = nftsPickedSet.ToList();
        currentNftIdx = 0;

        // Debug.Log("Exiting pickNftsToShow");
    }

    static void populateFromJson() {

        if (jsonLoaded == false) {
            nftsList = JsonUtility.FromJson<Nfts>(NftsJson.text);

            for (int i=0; i<nftsList.nfts.Length; i++) {
                Nft nft = nftsList.nfts[i];
                nftsDict.Add(nft.nftId, nft);
                addNftToMappers(nft);
            }
            jsonLoaded = true;
        }
        
    }

    static void addNftToMappers(Nft nft) {
        int[] imagePics = nft.imagePics;

        // facePropNftMapper

        if (facePropNftMapper.ContainsKey(imagePics[4])) {
            List<string> list = facePropNftMapper[imagePics[4]];
            list.Add(nft.nftId);
        }
        else {
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            facePropNftMapper.Add(imagePics[4], list);
        }

        // bodyNftMapper

        if (bodyNftMapper.ContainsKey(imagePics[3])) {
            List<string> list = bodyNftMapper[imagePics[3]];
            list.Add(nft.nftId);
        }
        else {
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            bodyNftMapper.Add(imagePics[3], list);
        }

        // headNftMapper

        if (headNftMapper.ContainsKey(imagePics[2])) {
            List<string> list = headNftMapper[imagePics[2]];
            list.Add(nft.nftId);
        }
        else {
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            headNftMapper.Add(imagePics[2], list);
        }

        // bgNftMapper
        if (bgNftMapper.ContainsKey(imagePics[1])) {
            List<string> list = bgNftMapper[imagePics[1]];
            list.Add(nft.nftId);
        }
        else {
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            bgNftMapper.Add(imagePics[1], list);
        }
        // faceNftMapper
        if (faceNftMapper.ContainsKey(imagePics[0])) {
            List<string> list = faceNftMapper[imagePics[0]];
            list.Add(nft.nftId);
        }
        else {
            List<string> list = new List<string>();
            list.Add(nft.nftId);
            faceNftMapper.Add(imagePics[0], list);
        }
    }

    public static void passNft() {
        // Debug.Log("Buy prob: " + buyProb().ToString());
        // Debug.Log("Sell prob: " + sellProb().ToString());
        Nft nextNftToShow = getNextNftToShow();
    }

    public static Nfts nftsList = new Nfts();
    public static TextAsset NftsJson = Resources.Load<TextAsset>("nfts");
    
    // All vars related to NFTs
    public static Dictionary<string, Nft> nftsDict = new Dictionary<string, Nft>();
    // public Dictionary<string, List<string>> mainCategoryNftMapper = new Dictionary<string, List<string>>();
    // public Dictionary<string, List<string>> subCategoryNftMapper = new Dictionary<string, List<string>>();

    public static Dictionary<int, List<string>> faceNftMapper = new Dictionary<int, List<string>>();
    public static Dictionary<int, List<string>> bgNftMapper = new Dictionary<int, List<string>>();
    public static Dictionary<int, List<string>> headNftMapper = new Dictionary<int, List<string>>();
    public static Dictionary<int, List<string>> bodyNftMapper = new Dictionary<int, List<string>>();
    public static Dictionary<int, List<string>> facePropNftMapper = new Dictionary<int, List<string>>();

    [System.Serializable]
    public class Nft {
        public string nftId;
        public double price;
        public int rank;
        public int noOfBuyers;
        public int[] imagePics;
    }

    [System.Serializable]
    public class Nfts {
        public Nft[] nfts; 
    }
}
