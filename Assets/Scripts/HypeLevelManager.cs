using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeLevelManager : MonoBehaviour
{
    [SerializeField] BarManager HypeBar;
    [SerializeField] BarManager TimerBar;
    [SerializeField] phoneBehavior phone;
    public static string countdownTimerStr = "";

    public GameObject GameOverMenu;
    public GameObject slider;

    private float currLevel;
    private float currTime;

    public float levelCap = 100f;
    public float timeCap = 7f;

    public float incPerBuy = 5f;
    public float feverTime = 3f;
    public float feverCD = 5f;
    public bool isCD = false;

    public float decPerSec = 1f;
    public float decPerTO = 10f;

    public Color startColor;
    public Color endColor;

    private Timer timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        if(PriceManager.currentDay==5) timeCap = 4f;
        resetLevelBar();
        resetTimerBar();

        if(PriceManager.currentDay>=4){
            slider.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsPaused)
        {
            return;
        }
        currTime -= Time.deltaTime;
        if(currTime<=0){
            resetTimerBar();
            resetLevelBar();
            phone.phonePass();
        }
        setTimerBar();
        if(currLevel>=levelCap) StartCoroutine(feverMode());
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

    public void resetLevelBar(){
        currLevel = 0f;
        setLevelBar();
    }

    public void resetTimerBar(){
        currTime = timeCap;
        setTimerBar();
    }

    public void levelIncrease(){
        if(!isCD){
            currLevel += incPerBuy;
            setLevelBar();
        }
    }

    IEnumerator feverMode(){
        isCD = true;
        float ogDuration = phone.getDuration();
        phone.setDuration(ogDuration/4);
        float time = 0;
        PriceManager.offerDiscount = true;
        if(PriceManager.currentDay>=4){
            slider.SetActive(true);
        }
        while(time<feverTime){
            time += Time.deltaTime;
            currLevel  = Mathf.SmoothStep(levelCap,0,time/feverTime);
            setLevelBar();
            yield return null;
        }
        PriceManager.offerDiscount = false;
        if(PriceManager.currentDay>=4){
            slider.SetActive(false);
        }
        phone.setDuration(ogDuration);

        time = 1;
        Debug.Log("time: " + time);
        while(time < 6)
        {
            time += Time.deltaTime;
            countdownTimerStr = "Hype starts in " + getExactCountdownTime(time).ToString();
            // yield return new WaitForSeconds(1);
            yield return null;
        } 
        countdownTimerStr = "";

        // yield return new WaitForSeconds(feverCD);
        isCD = false;
        yield return new WaitForSeconds(0f);
    }

    private float Cap(float curr, float min, float max){
        if(curr<min) return min;
        else if(curr>max) return max;
        return curr;
    }

    private int getExactCountdownTime(float time) {
        return 6 - (int) time;
    }

    public void disableHype(){
        PriceManager.offerDiscount = false;
        enabled = false;
    }
}