using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrendFiller : MonoBehaviour
{
    public TrendFillerHelper helper = new TrendFillerHelper();
    // Start is called before the first frame update

    void Start()
    {    }

    // Update is called once per frame
    void Update()
    {
        parsePriceManager();
        showTrends();
    }

    void parsePriceManager(){
        helper.currUpDown[0] = PriceManager.futureTrendingFacePos > -1 ? true : false;
        helper.currUpDown[1] = PriceManager.futureTrendingBodyPos > -1 ? true : false;
        helper.currUpDown[2] = PriceManager.futureTrendingHatPos > -1 ? true : false;

        helper.futureUpDown[0] = PriceManager.futureTrendingFaceNeg > -1 ? false : true;
        helper.futureUpDown[1] = PriceManager.futureTrendingBodyNeg > -1 ? false : true;
        helper.futureUpDown[2] = PriceManager.futureTrendingHatNeg > -1 ? false : true;

        helper.currTrends[0] = PriceManager.futureTrendingFacePos;
        helper.currTrends[1] = PriceManager.futureTrendingBodyPos;
        helper.currTrends[2] = PriceManager.futureTrendingHatPos;

        helper.futureTrends[0] = PriceManager.futureTrendingFaceNeg;
        helper.futureTrends[1] = PriceManager.futureTrendingBodyNeg;
        helper.futureTrends[2] = PriceManager.futureTrendingHatNeg;
    }

    void showTrends(){
        for(int i = 0; i<3;i++){
            int indexJ = helper.currTrends[i];
            if(indexJ>-1){
                NFTParts temp = helper.currTrendParts[i];
                temp.SetTexture(i,indexJ);
                temp.SetBackGround(helper.currUpDown[i]);
            }   
        }

        for(int i = 0; i<3;i++){
            int indexJ = helper.futureTrends[i];
            if(indexJ>-1){
                NFTParts temp = helper.futureTrendParts[i];
                temp.SetTexture(i,indexJ);
                temp.SetBackGround(helper.futureUpDown[i]);
            }
        }
    }
}

//face, body, hat
[System.Serializable]
public class TrendFillerHelper{
    public NFTParts[] currTrendParts = new NFTParts[3];
    public NFTParts[] futureTrendParts = new NFTParts[3];
    public int[] currTrends = new int[3];
    public int[] futureTrends = new int[3];
    public bool[] currUpDown = new bool[3];
    public bool[] futureUpDown = new bool[3];
}
