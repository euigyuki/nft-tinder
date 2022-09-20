using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarManager : MonoBehaviour
{
    public SpriteRenderer bar;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(1,1,1);
    }

    public void setXScale(float xScale){
        transform.localScale = new Vector3(xScale,1,1);
    }

    public void setBarColor(Color color){
        bar.color = color;
    }
}
