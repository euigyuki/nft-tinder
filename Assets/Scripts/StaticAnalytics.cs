using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Proyecto26;
using Random=UnityEngine.Random;

public class StaticAnalytics : MonoBehaviour
{
    // Start is called before the first frame update

    //public static StaticAnalytics instance {get; private set;}
    //public static analyticsData data = new analyticsData();
    // public static int leftPress = 0;
    // public static int rightPress = 0;

    private const string projectId = "nft-tinder-b99da"; // You can find this in your Firebase project settings
    private static readonly string databaseURL = $"https://{projectId}.firebaseio.com/";
    public static int levelCount = 0;
    public static int userId;
    
    //"https://nft-tinder-analytics-default-rtdb.firebaseio.com/buySell.json"
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void leftPressIncrement(){
        analyticsData.leftPress += 1;
    }

    public static void rightPressIncrement(){
        analyticsData.rightPress += 1;
    }

    public static void usedTime(float time){
        analyticsData.timeUsed = time;
    }

    public static string toJson(){
        var data = new {
        left=analyticsData.leftPress,
        right=analyticsData.rightPress,
        time = analyticsData.timeUsed
        };
        //System.Random rnd = new System.Random();
        //int userId= rnd.Next();

        if (userId == 0) {
            System.Random rnd = new System.Random();
            userId= rnd.Next();
            // userId = Random.Range(100000,1000000);
        }
        analyticsJson ajson = new analyticsJson();
        ajson.leftPress=analyticsData.leftPress;
        ajson.rightPress=analyticsData.rightPress; 
        ajson.timeUsed = analyticsData.timeUsed;


        buyTrendDataJson bjson = new buyTrendDataJson();
        bjson.totalBuys = buyTrendData.totalBuys;
        bjson.posTrendingBuys = buyTrendData.posTrendingBuys;
        bjson.negTrendingBuys = buyTrendData.negTrendingBuys;
        bjson.neutralBuys = buyTrendData.neutralBuys;

        PostBuySellData(ajson,userId);

        PostBuyTrendData(bjson, userId);

        return JsonUtility.ToJson(ajson);
    }


    public static void PostBuySellData(analyticsJson data, int userId)
    {
        //userId=userId.ToString();
        Debug.Log("Sending data to firebase - PostBuySellData");
        RestClient.Patch<analyticsJson>($"https://nft-tinder-analytics-default-rtdb.firebaseio.com/buySellLatest/{userId}.json", data);
    }

    public static void incPosTrendingBuy() {
        buyTrendData.totalBuys += 1;
        buyTrendData.posTrendingBuys += 1;
    }

    public static void incNegTrendingBuy() {
        buyTrendData.totalBuys += 1;
        buyTrendData.negTrendingBuys += 1;
    }
    

    public static void incNeutralTrendingBuy() {
        buyTrendData.totalBuys += 1;
        buyTrendData.neutralBuys += 1;
    }

    public static void PostBuyTrendData(buyTrendDataJson data, int userId) {
        Debug.Log("Sending data to firebase - PostBuyTrendData");
        RestClient.Patch<buyTrendDataJson>($"https://nft-tinder-analytics-default-rtdb.firebaseio.com/buyTrendsData/{userId}.json", data);
    }

    public static void postEachLevelSellData(int totalSellscount, int posSellcount, int negSellcount){
        Debug.Log("Sending data to firebase - sell data");
        if (userId == 0) {
            System.Random rnd = new System.Random();
            userId= rnd.Next();
            // userId = Random.Range(100000,1000000);
        }
        sellDataJson sjson = new sellDataJson();
        sjson.totalSells = totalSellscount;
        sjson.posSell = posSellcount;
        sjson.negSell = negSellcount;
        levelCount = levelCount + 1;
        RestClient.Patch<sellDataJson>($"https://nft-tinder-analytics-default-rtdb.firebaseio.com/sellLevelsData/{userId}/{levelCount}.json", sjson);
        postEachLevelWalletPortfolioValue();
    }

    public static void postEachLevelWalletPortfolioValue() {
        Debug.Log("Sending data to firebase - postEachLevelWalletPortfolioValue data");
        if (userId == 0) {
            System.Random rnd = new System.Random();
            userId= rnd.Next();
            // userId = Random.Range(100000,1000000);
        }

        walletPortfolioValueData data = new walletPortfolioValueData();
        data.walletValue = PriceManager.walletValue;
        data.portfolioValue = PriceManager.getPortfolioValue();
        RestClient.Patch<walletPortfolioValueData>($"https://nft-tinder-analytics-default-rtdb.firebaseio.com/walletPortfolioValueData/{userId}/{levelCount}.json", data);
    }


}



[Serializable]
public class analyticsData{
    public static int leftPress = 0;
    public static int rightPress = 0;
    public static float timeUsed = 0;
} 

[Serializable]
public class analyticsJson{
    public int leftPress = 0;
    public int rightPress = 0;
    public float timeUsed = 0;
} 


[Serializable]
public class buyTrendData{
    public static int totalBuys = 0;
    public static int posTrendingBuys = 0;
    public static int negTrendingBuys = 0;
    public static int neutralBuys = 0;
} 

[Serializable]
public class buyTrendDataJson{
    public int totalBuys = 0;
    public int posTrendingBuys = 0;
    public int negTrendingBuys = 0;
    public int neutralBuys = 0;
} 

[Serializable]
public class sellData{
    public static int totalSells = 0;
    public static int posSell = 0;
    public static int negSell = 0;
}

[Serializable]
public class sellDataJson{
    public int totalSells = 0;
    public int posSell = 0;
    public int negSell = 0;
}

[Serializable]
public class walletPortfolioValueData {
    public double walletValue = 0;
    public double portfolioValue = 0;
}
