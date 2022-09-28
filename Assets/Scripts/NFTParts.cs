using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFTParts : MonoBehaviour
{

    public imageHolder[] imageParts = new imageHolder[5];
    public RawImage background;
    public Color goodColor;
    public Color badColor;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    public void SetTexture(int indexI, int indexJ){
        imageHolder temp =  imageParts[indexI];
        temp.imagePart.texture = temp.imageTextures[indexJ];
        temp.imagePart.color = new Color(1,1,1,1);
    }

    public void SetBackGround(bool isGood){
        if(isGood) background.color = goodColor;
        else background.color = badColor;
    }

    public void init(){
        for(int i=0; i<imageParts.Length; i++){
            imageHolder temp =  imageParts[i];
            temp.imagePart.color = new Color(1,1,1,0);
        }
        background.color = new Color(1,1,1,0);
    }
}
