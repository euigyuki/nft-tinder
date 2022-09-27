using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticAnalytics : MonoBehaviour
{
    // Start is called before the first frame update

    public static StaticAnalytics instance {get; private set;}
    //public static analyticsData data = new analyticsData();
    public static int leftPress = 0;
    public static int rightPress = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void leftPressIncrement(){
        leftPress += 1;
    }

    public static void rightPressIncrement(){
        rightPress += 1;
    }

    public static string toJson(){
        var data = new {
        left=leftPress,
        right=rightPress
        };
        Debug.Log(data);
        
        return JsonUtility.ToJson(data);
    }


}

// public class analyticsData{
//     public int leftPress = 0;
//     public int rightPress = 0;
// }