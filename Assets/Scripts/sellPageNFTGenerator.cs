using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sellPageNFTGenerator : MonoBehaviour
{
    // face, bg, head, body, face
    public imageHolder[] imageParts = new imageHolder[5];

    public GameObject rotPivot;
    public float duration;

    public int faceindex;
    public int bgIndex;
    public int headIndex;
    public int bodyIndex;
    public int fwIndex;

    public void receiveIndices(int f, int bg, int h, int b, int fw) {
        faceindex = f;
        bgIndex = b;
        headIndex = h;
        bodyIndex = b;
        fwIndex = fw;

        Debug.Log("Inside NFTGen");

    }

    // Start is called before the first frame update
    void Start()
    {
        genNFTByIndex();
    }

    // Update is called once per frame
    void Update()
    {
        // genNFTByIndex();
        if(Input.GetKeyDown("g")){
            genNFTByIndex();
        }

        if(Input.GetKeyDown("t")){
            StartCoroutine(rotateAnimation(1));
        }

        if(Input.GetKeyDown("r")){
            StartCoroutine(rotateAnimation(-1));
        }
    }

    public void randomGen()
    {
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            int index = Random.Range(0,temp.imageTextures.Length);
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    public void genNFTByIndex() {
        int[] indices = new int[5] { faceindex, bgIndex, headIndex, bodyIndex, fwIndex };
        
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            int index = indices[i];
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    IEnumerator rotateAnimation(int direction){
        Vector3 axis = new Vector3(0,0,1);
        float t = 0.0f;
        float tDelta = 0.0f;
        float rotationAngle = 90.0f/duration;
        while(t<duration){
            t+=Time.deltaTime;
            tDelta = Time.deltaTime;
            if(t>duration) tDelta-=(t-duration);
            transform.RotateAround(rotPivot.transform.position,axis,direction*rotationAngle*tDelta);
            yield return null;
        }
    }
}
