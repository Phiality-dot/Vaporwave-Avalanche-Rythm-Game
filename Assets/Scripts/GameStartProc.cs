using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameStartProc : MonoBehaviour
{

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        if (File.Exists(Path.Combine(Application.persistentDataPath, "sett.txt"))) {
            PlayerInfo.sensetivity = float.Parse(File.ReadAllText(Path.Combine(Application.persistentDataPath, "sett.txt")));
            slider.value = PlayerInfo.sensetivity;
        } else
        {
            slider.value = 50f;
            FileStream fileStream = File.Create(Path.Combine(Application.persistentDataPath, "sett.txt"));
            fileStream.Close();
            File.WriteAllText(Path.Combine(Application.persistentDataPath, "sett.txt"), slider.value.ToString());
        }
        
        slider.onValueChanged.AddListener(delegate
        {
            OnValChange();
        });
    }
    public void OnValChange()
    {
        PlayerInfo.sensetivity = slider.value;
        File.Delete(Path.Combine(Application.persistentDataPath, "sett.txt"));
        FileStream fileStream = File.Create(Path.Combine(Application.persistentDataPath, "sett.txt"));
        fileStream.Close();
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "sett.txt"), slider.value.ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
