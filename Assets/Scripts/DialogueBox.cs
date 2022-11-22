using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public GameObject arrow; 
    private float yPos;
    public float yDelta = 100;
    public float freq = 1;
    public SpriteRenderer imagePart;
    public Sprite[] imageSprites = new Sprite[5];
    // Start is called before the first frame update
    void Start()
    {
        yPos = arrow.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        arrow.transform.localPosition =new Vector3( arrow.transform.localPosition.x,yPos + (yDelta* (1+Mathf.Sin(2f*Mathf.PI*freq*Time.timeSinceLevelLoad) ) ), arrow.transform.localPosition.z);
    }

    // 0 ang, 1 dis, 2 happy, 3 laugh, 4 sad
    public void changeEmotion(int index){
        imagePart.sprite = imageSprites[index];
    }
}
