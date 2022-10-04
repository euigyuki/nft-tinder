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

    public float incPerBuy = 5f;
    public float feverTime = 5f;
    public float feverCD = 10f;
    public bool isCD = false;

    public float decPerSec = 1f;
    public float decPerTO = 10f;

    public Color startColor;
    public Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        resetLevelBar();
        resetTimerBar();
    }

    // Update is called once per frame
    void Update()
    {
        currTime -= Time.deltaTime;
        if(currTime<=0){
            resetTimerBar();
            phone.phonePass();
        }
        setTimerBar();
        if(currLevel>=levelCap) StartCoroutine(feverMode());
        // if(currLevel <= 0){
        //     GameOverMenu.SetActive(true);
        //     enabled = false;
        //     phone.disablePhone();
        //     StaticAnalytics.toJson();
        // }
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
        float time = 0;
        while(time<feverTime){
            time += Time.deltaTime;
            currLevel  = Mathf.SmoothStep(levelCap,0,time/feverTime);
            setLevelBar();
            yield return null;
        }
        yield return new WaitForSeconds(feverCD);
        isCD = false;
    }

    private float Cap(float curr, float min, float max){
        if(curr<min) return min;
        else if(curr>max) return max;
        return curr;
    }
}