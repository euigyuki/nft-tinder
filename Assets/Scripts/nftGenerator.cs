using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nftGenerator : MonoBehaviour
{
    // face, bg, head, body, face
    public imageHolder[] imageParts = new imageHolder[5];

    public float duration = 0.5f;
    public int rotAngle = 45;

    private Vector3 originalScale;
    public Vector3 originalPos;
    
    public bool isCoroutine = false;
    // Start is called before the first frame update
    void Start()
    {
        randomGen();
        originalScale = transform.localScale;
        originalPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("g")){
        //     randomGen();
        // }

        // if(Input.GetKeyDown("t")){
        //     StartCoroutine(swipeEffect(false));
        // }

        // if(Input.GetKeyDown("r")){
        //     StartCoroutine(swipeEffect(true));
        // }
    }

    public void randomGen()
    {
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            int index = Random.Range(0,temp.imageTextures.Length);
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    public void setNftPic() {
        int[] chosenPicsIdxs = PriceManager.getNftPicsIdxs();
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            // int index = Random.Range(0,temp.imageTextures.Length);
            int index = chosenPicsIdxs[i];
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    public void swipe(bool swipeLeft){
        if(swipeLeft) StartCoroutine(swipeEffect(true));
        else StartCoroutine(swipeEffect(false));
    }

    public void moveCard(){
        setColor(new Color(1,1,1,1));
        StartCoroutine(setFirstCard());
    }

    public void setColor(Color newColor){
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            temp.imagePart.color = newColor;
        }
    }

    public void setSecondCard(){
        transform.localPosition = new Vector3(originalPos.x,originalPos.y,-2.5f);
        transform.eulerAngles = new Vector3(0,0,0);
    }

    IEnumerator setFirstCard(){
        float time = 0;
        isCoroutine = true;
        while(time<duration){
            time+= Time.deltaTime;
            transform.localPosition = new Vector3(transform.localPosition.x,transform.localPosition.y,Mathf.SmoothStep(-2.5f,-3f,time/duration));
            float scalar = Mathf.SmoothStep(0.8f*originalScale.x,originalScale.x,time/duration);
            transform.localScale = new Vector3(scalar,scalar,scalar);
            setColor(new Color(1,1,1, Mathf.SmoothStep(0.8f,1,time/duration)));
            yield return null;
        }
        isCoroutine = false;
    }

    IEnumerator swipeEffect(bool swipeLeft){
        float time = 0;
        float fadeStart = duration/1.5f;
        float rotStart = duration/4f;
        isCoroutine = true;
        while (time<duration){
            time += Time.deltaTime;
            if(swipeLeft){
                transform.localPosition = new Vector3(Mathf.SmoothStep(originalPos.x, 
                originalPos.x-3,time/duration),transform.localPosition.y,transform.localPosition.z);
                if(time>rotStart)
                    transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(0,rotAngle,(time-rotStart)/(duration-rotStart)));
            }else{
                transform.localPosition = new Vector3(Mathf.SmoothStep(originalPos.x, 
                originalPos.x+3,time/duration),transform.localPosition.y,transform.localPosition.z);
                if(time>rotStart)
                    transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(0,-1*rotAngle,(time-rotStart)/(duration-rotStart)));

            }
            if(time>fadeStart){
                setColor(new Color(1,1,1, Mathf.SmoothStep(1,0,(time-fadeStart)/(duration-fadeStart))));
            }
            yield return null;
        }
        setSecondCard();
        isCoroutine = false;
    }
}

[System.Serializable]
public class imageHolder{
    public RawImage imagePart;
    public Texture[] imageTextures = new Texture[4];
}
