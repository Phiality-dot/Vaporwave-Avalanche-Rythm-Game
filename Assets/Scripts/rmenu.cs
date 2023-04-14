using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class rmenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void returntomenu()
    {
        SceneManager.LoadScene(sceneName:"Main Menu");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
