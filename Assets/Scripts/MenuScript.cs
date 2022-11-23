using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToGame() {
        SceneManager.LoadScene("NewTutorialBuy");
    }
     public void SwitchToOptions() {
        SceneManager.LoadScene("Options");
    }
    public void SwitchToCreduts() {
        SceneManager.LoadScene("Credits");
    }

}
