using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MoneyBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image Fill;
    public static MoneyBar instance;
    public RectTransform picture;
    
    private void Awake(){
        instance = this;
        picture = GetComponent<RectTransform>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
  
}
