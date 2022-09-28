using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class analyticsTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // WebGLFileSaver.IsSavingSupported();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Analyticstest (){
        // WebGLFileSaver.IsSavingSupported();
        StaticAnalytics.toJson();

    }
}
