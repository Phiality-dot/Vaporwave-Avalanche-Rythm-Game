using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class progressBar : MonoBehaviour
{
    public Transform player;
    private Transform endCube;
    public Image slider;
    public TMP_Text progresstext;
    private float levelDistance;
    private bool onlyonce = true;
    float progress;
    private void Awake()
    {
        if (GameObject.Find("VictoryCube") != null)
        {
            endCube = GameObject.Find("VictoryCube").GetComponent<Transform>();
        } else if (GameObject.Find("Cube (8)") !=null) 
        {
            endCube = GameObject.Find("Cube (8)").GetComponent<Transform>();
        }
    }
    // Start is called before the first frame update
    
    void Update()
    {
        if (onlyonce)
        {
            levelDistance = Mathf.Abs(endCube.position.z - player.position.z);
            onlyonce = false;
        }
        // Get the distance between the player and the end cube along the z-axis
        float distance = Mathf.Abs(endCube.position.z - player.position.z);

        // Calculate the progress as a percentage (assuming the level ends at a specific distance)
        progress = 1 - (distance / levelDistance);

        // Set the progress on the progress bar
        progress = Mathf.Round(progress * 100) / 100f;
        string formattedFloat = progress.ToString("0.00");
        slider.fillAmount = float.Parse(formattedFloat);
        progresstext.text = (float.Parse(formattedFloat) * 100f).ToString() + "%";
    }
}
