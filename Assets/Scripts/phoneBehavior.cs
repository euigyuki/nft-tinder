using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class phoneBehavior : MonoBehaviour
{
    public GameObject buy;
    public GameObject pass;
    public int fadeTime;
    [SerializeField] HypeLevelManager hlMang;
    [SerializeField] nftGenerator generator;

    // Start is called before the first frame update
    void Start()
    {
        buy.SetActive(false);
        pass.SetActive(false);
        PriceManager.setUp();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right")){
            phoneBuy();
        }

        if(Input.GetKeyDown("left")){
            phonePass();
        }
    }

    public void phonePass(){
        StartCoroutine(showAndHide(pass));
        // PriceManager.instance.passItem();
        // PriceManager.instance.passNft();
        // generator.randomGen();
        StaticAnalytics.leftPressIncrement();
        PriceManager.passNft();
        generator.setNftPic();
        hlMang.resetTimerBar();
    }

    public void phoneBuy(){
        StartCoroutine(showAndHide(buy));
        // PriceManager.instance.SubstractMoney();
        // PriceManager.instance.buyNft();
        // generator.randomGen();
        StaticAnalytics.rightPressIncrement();
        PriceManager.buyNft();
        generator.setNftPic();
        hlMang.resetTimerBar();
        hlMang.levelIncrease();
    }

    IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        obj.SetActive(false);
    }

    public void disablePhone(){
        enabled =false;
    }
}
