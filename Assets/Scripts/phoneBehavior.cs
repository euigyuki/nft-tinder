using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class phoneBehavior : MonoBehaviour
{
    public GameObject buy;
    public GameObject pass;
    public int fadeTime;
    // Start is called before the first frame update
    void Start()
    {
        buy.SetActive(false);
        pass.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("right")){
            StartCoroutine(showAndHide(buy));
            PriceManager.instance.SubstractMoney();
        }

        if(Input.GetKeyDown("left")){
            StartCoroutine(showAndHide(pass));
            PriceManager.instance.passItem();
        }
    }

    IEnumerator showAndHide(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSeconds(fadeTime);
        obj.SetActive(false);
    }
}
