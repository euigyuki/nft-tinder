using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;
using UnityEngine.SceneManagement;

public class daySellSummary : MonoBehaviour
{

    public TextMeshProUGUI portfolioValue;
    public TextMeshProUGUI walletValue;
    public TextMeshProUGUI dayProfitLossValueText;
    public Button goToNextDayButton;

    // Start is called before the first frame update
    void Start()
    {
        walletValue.text = String.Format("Wallet Value = ${0:0.##}", PriceManager.walletValue);
        portfolioValue.text = String.Format("Portfolio Value = ${0:0.##}", PriceManager.getPortfolioValue());

        setProfitLossText();
        goToNextDayButton.onClick.AddListener(loadBuyScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setProfitLossText() {
        // double profitLoss = sellHelper.totalProfitLoss;
        if(sellHelper.totalProfitLoss > 0) {
            dayProfitLossValueText.text = String.Format("Profit for the day = ${0:0.##}", sellHelper.totalProfitLoss);
        } else if(sellHelper.totalProfitLoss < 0) {
            dayProfitLossValueText.text = String.Format("Loss for the day = ${0:0.##}", -1 * sellHelper.totalProfitLoss);
        } else {
            dayProfitLossValueText.text = "You have not made any profit or loss";
        }
    }

    void loadBuyScene() {
        SceneManager.LoadScene("trendingDerrick");
    }
}
