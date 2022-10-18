using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowMove : MonoBehaviour
{
    
    private float xPos;
    public float xDelta = 0.5f;
    public float freq = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        xPos = gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position =new Vector3( xPos + (xDelta* (1+Mathf.Sin(2f*Mathf.PI*freq*Time.timeSinceLevelLoad) ) ),gameObject.transform.position.y, gameObject.transform.position.z);

    }
}
