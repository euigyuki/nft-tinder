using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopWindows : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pop;
    public GameObject image;
    public GameObject button;
    
    public static PopWindows instance;
    void Start()
    {
       instance = this;
       image.SetActive(false); 
       button.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showmessage(){
        StartCoroutine(messageshow());
    }

    IEnumerator messageshow(){
        image.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        image.SetActive(false);
    }
}
