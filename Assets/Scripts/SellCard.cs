using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellCard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        setNftCategory();
        setNftNumber();
        setNftPrice();
    }

    public void SetBackGround(bool isGood){
        if(isGood) background.color = goodColor;
        else background.color = badColor;
    }

    void setNftCategory() {
        //category.text = PriceManager.getNftId();
    }

    void setNftNumber() {
       //number.text = PriceManager.getNftId();
    }

    void setNftPrice() {
        //price.text = PriceManager.getNftId();
    }
}

[System.Serializable]
public class imageHolder{
    public RawImage imagePart;
    public RawImage background;
    public Color goodColor;
    public Color badColor;
    public TextMeshProUGUI category;
    public TextMeshProUGUI number;
    public TextMeshProUGUI price;
}