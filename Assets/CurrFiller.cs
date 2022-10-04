using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrFiller : MonoBehaviour
{
    public CurrFillerHelper helper = new CurrFillerHelper();
    public GameObject CurrentTrendPage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        parsePriceManager();
        // helper.currUpDown[0]=true;
        // helper.currUpDown[1]=false;
        // helper.currUpDown[2])=false;
        showTrends();
        
        
        
    }
    void hide()
    {
        CurrentTrendPage.SetActive(false);
    }
    void show()
    {
        CurrentTrendPage.SetActive(true);
    }
    void parsePriceManager(){
        helper.currUpDown[0] = PriceManager.currentTrendingFacePos > -1 ? true : false;
        helper.currUpDown[1] = PriceManager.currentTrendingBodyPos > -1 ? true : false;
        helper.currUpDown[2] = PriceManager.currentTrendingHatPos > -1 ? true : false;
        // Debug.Log(helper.currUpDown[0]);
        // Debug.Log(helper.currUpDown[1]);
        // Debug.Log(helper.currUpDown[2]);
        helper.futureUpDown[0] = PriceManager.currentTrendingFaceNeg > -1 ? false : true;
        helper.futureUpDown[1] = PriceManager.currentTrendingBodyNeg > -1 ? false : true;
        helper.futureUpDown[2] = PriceManager.currentTrendingHatNeg > -1 ? false : true;

        helper.currTrends[0] = PriceManager.currentTrendingFacePos;
        helper.currTrends[1] = PriceManager.currentTrendingBodyPos;
        helper.currTrends[2] = PriceManager.currentTrendingHatPos;
        

        helper.futureTrends[0] = PriceManager.currentTrendingFaceNeg;
        helper.futureTrends[1] = PriceManager.currentTrendingBodyNeg;
        helper.futureTrends[2] = PriceManager.currentTrendingHatNeg;
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
        int x=0;
        for (int i =0;i<3;i++)
        {
            if(helper.currTrends[i]==-1 && helper.futureTrends[i]==-1)
            {
                x=x+1;
            }
        }
        if (x==3)
        {
            hide();
        }
        else
        {
            show();
        }
    }
}
//face, body, hat
[System.Serializable]
public class CurrFillerHelper{
    public NFTParts[] currTrendParts = new NFTParts[3];
    public NFTParts[] futureTrendParts = new NFTParts[3];
    public int[] currTrends = new int[3];
    public int[] futureTrends = new int[3];
    public bool[] currUpDown = new bool[3];
    public bool[] futureUpDown = new bool[3];
}
