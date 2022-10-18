using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class NewsManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI textShow;
    void Start()
    {
    
        textShow.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if(PriceManager.currentDay==2) {
            textShow.text = "A new body was added";
        }
        else if(PriceManager.currentDay==3) {
            textShow.text = "A new body was added";
        }
        else if(PriceManager.currentDay==4) {
            textShow.text = "Recommender is noew only Shown during fever mode";
        }
        else if(PriceManager.currentDay==5) {
            textShow.text = "You have shorter time to make decisions";
        }
    }
}
