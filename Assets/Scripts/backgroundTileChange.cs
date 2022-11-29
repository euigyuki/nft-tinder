using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundTileChange : MonoBehaviour
{
    public SpriteRenderer tile1;
    public SpriteRenderer tile2;

    public Color start;
    public Color mid;
    public Color end;

    public float currTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeBackground());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator changeBackground(){
        yield return new WaitForSeconds(3);
        while(currTime<60){
            currTime += Time.deltaTime;
            if(currTime<30){
                tile1.color = Color.Lerp(start,mid,currTime/30);
                tile2.color = Color.Lerp(start,mid,currTime/30);
            }else{
                tile1.color = Color.Lerp(mid,end,(currTime-30)/30);
                tile2.color = Color.Lerp(mid,end,(currTime-30)/30);
            }
            yield return null;
        }
    }
}
