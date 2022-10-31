using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ObjectiveManager : MonoBehaviour
{

    public TextMeshProUGUI objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        objectiveText.text = String.Format("Wallet + Portfolio >= ${0:0.##}", PriceManager.getLevelObjective(PriceManager.currentDay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
