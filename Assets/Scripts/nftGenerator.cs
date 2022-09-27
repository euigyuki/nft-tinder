using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nftGenerator : MonoBehaviour
{
    // face, bg, head, body, face
    public imageHolder[] imageParts = new imageHolder[5];

    public GameObject rotPivot;
    public float duration;
    public int rotAngle;

    // Start is called before the first frame update
    void Start()
    {
        randomGen();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("g")){
            randomGen();
        }

        if(Input.GetKeyDown("t")){
            // StartCoroutine(rotateAnimation(1));
            StartCoroutine(swipeEffect(false));
            Debug.Log(Screen.width);
        }

        if(Input.GetKeyDown("r")){
            // StartCoroutine(rotateAnimation(-1));
            StartCoroutine(swipeEffect(true));
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

    public void setNftPic() {
        int[] chosenPicsIdxs = PriceManager.getNftPicsIdxs();
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            // int index = Random.Range(0,temp.imageTextures.Length);
            int index = chosenPicsIdxs[i];
            temp.imagePart.texture = temp.imageTextures[index];
        }
    }

    public void setColor(Color newColor){
        for(int i = 0; i<imageParts.Length; i++){
            imageHolder temp = imageParts[i];
            temp.imagePart.color = newColor;
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

    IEnumerator swipeEffect(bool swipeLeft){
        float time = 0;
        float fadeStart = duration/1.5f;
        float rotStart = duration/4f;
        while (time<duration){
            time += Time.deltaTime;
            if(swipeLeft){
                transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, 
                transform.position.x-0.025f,time/duration),transform.position.y,transform.position.z);
                if(time>rotStart)
                    transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(0,rotAngle,(time-rotStart)/(duration-rotStart)));
            }else{
                transform.position = new Vector3(Mathf.SmoothStep(transform.position.x, 
                transform.position.x+0.025f,time/duration),transform.position.y,transform.position.z);
                if(time>rotStart)
                    transform.eulerAngles = new Vector3(0,0,Mathf.LerpAngle(0,-1*rotAngle,(time-rotStart)/(duration-rotStart)));

            }
            if(time>fadeStart){
                setColor(new Color(1,1,1, Mathf.SmoothStep(1,0,(time-fadeStart)/(duration-fadeStart))));
            }
            yield return null;
        }

    }
}

[System.Serializable]
public class imageHolder{
    public RawImage imagePart;
    public Texture[] imageTextures = new Texture[4];
}
