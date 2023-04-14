using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class clkstartl1 : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(GameObject.Find("cMusic"));

    }
    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    
}
