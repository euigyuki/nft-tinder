using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAnalytics : MonoBehaviour
{
    // Start is called before the first frame update

    //public static StaticAnalytics instance {get; private set;}
    public static analyticsData data = new analyticsData();
    // public static int leftPress = 0;
    // public static int rightPress = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void leftPressIncrement(){
        analyticsData.leftPress += 1;
        // Debug.Log("leftPressIncrement");
        // Debug.Log(analyticsData.leftPress);
    }

    public static void rightPressIncrement(){
        analyticsData.rightPress += 1;
        // Debug.Log("rightPressIncrement");
        // Debug.Log(analyticsData.rightPress);
    }

    public static string toJson(){
        var data = new {
        left=analyticsData.leftPress,
        right=analyticsData.rightPress
        };
        // Debug.Log(data);
        // analyticsJson ajson = new analyticsJson();
        // ajson.leftPress=analyticsData.leftPress;
        // ajson.rightPress=analyticsData.rightPress; 
        // Debug.Log(ajson.leftPress);
        // Debug.Log(ajson.rightPress);
        // Debug.Log(analyticsData.leftPress);
        // Debug.Log(analyticsData.rightPress);
        Debug.Log(data);
        return JsonUtility.ToJson(data);
    }


}

public class analyticsData{
    public static int leftPress = 0;
    public static int rightPress = 0;
} 

// public class analyticsJson{
//     public int leftPress = 0;
//     public int rightPress = 0;
// } 

