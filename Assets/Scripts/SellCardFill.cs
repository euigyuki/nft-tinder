using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SellCardFill : MonoBehaviour
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
    //   public void SetBackGround(bool isGood){
    //      if(isGood) background.color = goodColor;
    //      else background.color = badColor;
    //  }

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
 public class imageHolder2{
     public RawImage imagePart;
     public RawImage background;
     public Color goodColor;
     public Color badColor;
     public TextMeshPro category;
     public TextMeshPro number;
     public TextMeshPro price;
 }
