using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class MoneyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Fill;
    public static MoneyBar instance;
    public RectTransform picture;
    public GameObject subText;
    public GameObject addText;
    
    private void Awake(){
        instance = this;
        picture = GetComponent<RectTransform>();
    }
    void Start()
    {
       subText.SetActive(false);
       addText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowMoney() {
        StartCoroutine(showAndHide(subText));
    }
   IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        obj.SetActive(false);
    }
}
