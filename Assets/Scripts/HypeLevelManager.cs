using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeLevelManager : MonoBehaviour
{
    [SerializeField] BarManager HypeBar;
    [SerializeField] BarManager TimerBar;
    [SerializeField] phoneBehavior phone;

    public GameObject GameOverMenu;

    private float currLevel;
    private float currTime;

    public float levelCap = 100f;
    public float timeCap = 5f;

    public float incPerBuy = 10f;

    public float decPerSec = 1f;
    public float decPerTO = 10f;

    public Color startColor;
    public Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        currLevel = levelCap;
        resetTimerBar();
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;
        currLevel -= decPerSec*Time.deltaTime;
        if(currTime<=0){
            resetTimerBar();
            currLevel -= decPerTO;
            phone.phonePass();
        }
        if(currLevel <= 0){
            GameOverMenu.SetActive(true);
            enabled = false;
            phone.disablePhone();
        } 
        setLevelBar();
        setTimerBar();
    }

    public void setLevelBar(){
        currLevel = Cap(currLevel,0,levelCap);
        HypeBar.setXScale(Mathf.Lerp(0f,1f,currLevel/levelCap));
    }

    public void setTimerBar(){
        currTime = Cap(currTime,0,timeCap);
        TimerBar.setXScale(Mathf.Lerp(0f,1f,currTime/timeCap));
        TimerBar.setBarColor(Color.Lerp(endColor,startColor,currTime/timeCap));
    }

    public void resetTimerBar(){
        currTime = timeCap;
        setTimerBar();
    }

    public void levelIncrease(){
        currLevel += incPerBuy;
        setLevelBar();
    }

    private float Cap(float curr, float min, float max){
        if(curr<min) return min;
        else if(curr>max) return max;
        return curr;
    }
}
