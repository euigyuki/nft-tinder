using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider slider;
    public Image Fill;
    public static MoneyBar instance;
    private void Awake(){
        instance = this;
        slider = gameObject.GetComponent<Slider>();
    }
    void Start()
    {
        InitiateBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void InitiateBar() {
        slider.value = 1f;
    }
}
