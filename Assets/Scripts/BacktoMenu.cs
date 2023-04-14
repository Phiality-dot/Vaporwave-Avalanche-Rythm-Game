using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BacktoMenu : MonoBehaviour
{
    public TMP_Dropdown dd;
    public Button b;
    public TMP_Text t;
    // Start is called before the first frame update
    void Start()
    {
        dd.gameObject.SetActive(false);
        b.gameObject.SetActive(false);
        t.gameObject.SetActive(false);
    }
    public void quit()
    {
        Application.Quit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
