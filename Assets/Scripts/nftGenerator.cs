using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nftGenerator : MonoBehaviour
{
    // face, bg, head, body, face
    public imageHolder[] imageParts = new imageHolder[5];
    public static nftGenerator nftGen;

    // Start is called before the first frame update
    void Start()
    {
        nftGen = this;
        randomGen();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("g")){
            randomGen();
        }
    }

    public void randomGen()
    {
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            int index = Random.Range(0,4);
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }
}

[System.Serializable]
public class imageHolder{
    public RawImage imagePart;
    public Texture[] imageTextures = new Texture[4];
}
