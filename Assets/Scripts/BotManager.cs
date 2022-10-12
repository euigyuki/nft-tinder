using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject botText;
    public GameObject botText2;
    public bool isCoroutine = false;
    public float duration;

    void Start()
    {
        botText.SetActive(false);
        botText2.SetActive(false);
    }

    public void BotBuy() {
        if(isCoroutine==true) return;
        else{
        StartCoroutine(showAndHide());
        }
        
    }

    public void BotPass() {
        if(isCoroutine==true) return;
        else{
        StartCoroutine(showAndHide2());
        }   
    }
    
    IEnumerator showAndHide()
    {
        botText.SetActive(true);
        yield return new WaitForSeconds(duration);
        botText.SetActive(false);
    }

    IEnumerator showAndHide2()
    {
        botText2.SetActive(true);
        yield return new WaitForSeconds(duration);
        botText2.SetActive(false);
    }
}
