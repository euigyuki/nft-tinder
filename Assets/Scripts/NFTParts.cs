using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NFTParts : MonoBehaviour
{

    public imageHolder[] imageParts = new imageHolder[5];
    public RawImage background;
    public static Color goodColor;
    public static Color badColor;
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
        if(ClickMode.Mode=="Normal"){
            goodColor= new Color (0.38f,0.81f,0.43f,1.0f);
            badColor= new Color (0.81f,0.41f,0.38f,1.0f);
        }
        if(ClickMode.Mode=="ColorBlind"){
            goodColor=new Color(0.047f,0.48f,0.863f,1.0f);
            badColor=new Color(1.0f,0.76f,0.039f,1.0f);
        }

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
