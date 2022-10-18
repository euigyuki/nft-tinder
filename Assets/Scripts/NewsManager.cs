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
            textShow.text = "A new Trend was added. Look out for the Body parts!";
        }
        else if(PriceManager.currentDay==3) {
            textShow.text = "A new Trend was added. Look out for the Hat parts!";
        }
        else if(PriceManager.currentDay==4) {
            textShow.text = "The Recommender is now only shown during Fever Mode.";
        }
        else if(PriceManager.currentDay==5) {
            textShow.text = "You now have shorter time to make decisions";
        }
    }
}
