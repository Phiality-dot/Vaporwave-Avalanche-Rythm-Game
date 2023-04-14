using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using Newtonsoft.Json;
using UnityEngine.UI;

public class RecentlyPlayed : MonoBehaviour
{
    public TMPro.TMP_Dropdown dropdownboi;
    public TMP_InputField sfrom;
    public TMP_InputField beattresh;
    public TMP_InputField song;
    public TMP_InputField endAt;
    public Button b;
    private string username = System.Environment.UserName;
    private Level[] dleveld;
    // Start is called before the first frame update
    void Start()
    {

        dropdownboi.onValueChanged.AddListener(delegate
        {
            showstuff();
        });

        if (Directory.Exists(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\"))
        {
            if (File.Exists(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON")) {

                // Read the JSON file
                string json = System.IO.File.ReadAllText(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON");

                // Deserialize the JSON data into a SampleObject instance
                dleveld = JsonConvert.DeserializeObject<Level[]>(json);



                List<string> options = new List<string>();
                foreach (Level level in dleveld)
                {
                    options.Add(level.name);
                }



                dropdownboi.AddOptions(options);






            } else
            {
                File.Create(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON");
            }
        } else
        {
            Directory.CreateDirectory(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\");
            if (File.Exists(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON"))
            {

            }
            else
            {
                File.Create(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON");
            }
        }
        dropdownboi.value = 0;


    }
    public void addToPlayed(string filePath, float startFrom, float beatTresh, float endAt)
    {
        string nameoffile = Path.GetFileName(filePath);
        // Create a sample object to write to a JSON file
        Level[] levels = new Level[]
    {
        new Level
        {
        filePath = filePath,
        beatTresh = beatTresh,
        startFrom = startFrom,
        name = nameoffile,
        endAt = endAt
    } };
        
    

        

        // Write the sample object to a JSON file
        string json = JsonConvert.SerializeObject(levels);
        System.IO.File.WriteAllText(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON", json);
    }
    [System.Serializable]
    public class Level
    {
        public string filePath;
        public float beatTresh;
        public float startFrom;
        public string name;
        public float endAt;
    }
    [System.Serializable]
    public class Lebel : IEnumerable
    {
        public string filePath;
        public float beatTresh;
        public float startFrom;
        public string name;
        public IEnumerator GetEnumerator()
        {
            yield return name;
            yield return beatTresh;
            yield return startFrom;
            yield return filePath;
        }
    }
    public void showstuff()
    {
        b.gameObject.SetActive(true);
        sfrom.text = dleveld[0].startFrom.ToString();
        beattresh.text = dleveld[0].beatTresh.ToString();
        song.text = dleveld[0].filePath.ToString();
        endAt.text = dleveld[0].endAt.ToString();
    }
}
