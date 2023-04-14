using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerInfo.charge = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInfo.charge < 1f)
        {
            GetComponent<Image>().fillAmount = PlayerInfo.charge / 4f;
        }
        else
        {
            GetComponent<Image>().fillAmount = 1f;
        }
    }
}
