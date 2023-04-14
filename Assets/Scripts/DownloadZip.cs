using System.Collections;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Compression;
using UnityEngine.Networking;
using Newtonsoft.Json;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class DownloadZip : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_Text downloading;
    private string serverUrl = "http://apollo.arcator.co.uk:41062";
    public Button downloadButton;
    private Level[] dleveld;
    AudioClip clip = null;
    string fileepath;
    private void Start()
    {
        downloading.gameObject.SetActive(false);
        downloadButton.onClick.AddListener(DownloadZipFile);
    }

    private void DownloadZipFile()
    {
        downloading.gameObject.SetActive(true);
        string selectedOption = dropdown.options[dropdown.value].text;
        string fileUrl = serverUrl + "/uploads/" + selectedOption + ".zip";

        StartCoroutine(DownloadAndExtract(fileUrl, selectedOption));
    }

    private IEnumerator DownloadAndExtract(string fileUrl, string selectedOption)
    {
        string extractedFolder = "DLSongs";
        string extractedFolderPath = Path.Combine(Application.persistentDataPath, extractedFolder, selectedOption);
        string filePath = Path.Combine(Application.persistentDataPath, selectedOption + ".zip");

        if (!Directory.Exists(extractedFolderPath))
        {
            Directory.CreateDirectory(extractedFolderPath);


            using (UnityWebRequest www = UnityWebRequest.Get(fileUrl))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    File.WriteAllBytes(filePath, www.downloadHandler.data);

                    ZipFile.ExtractToDirectory(filePath, extractedFolderPath, true);

                    File.Delete(filePath);
                }
            }
            

            // Read the JSON file
            string json = System.IO.File.ReadAllText(Path.Combine(extractedFolderPath, "RP.JSON"));

            // Deserialize the JSON data into a Level instance
            dleveld = JsonConvert.DeserializeObject<Level[]>(json);

            // Assign global var and load level
            StartCoroutine(GetAudio(Path.Combine(extractedFolderPath, selectedOption + ".mp3")));

            
        }
        else
        {
            // Read the JSON file
            string json = System.IO.File.ReadAllText(Path.Combine(extractedFolderPath, "RP.JSON"));

            // Deserialize the JSON data into a Level instance
            dleveld = JsonConvert.DeserializeObject<Level[]>(json);

            // Assign global var and load level
            StartCoroutine(GetAudio(Path.Combine(extractedFolderPath, selectedOption + ".mp3")));
        }
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
    IEnumerator GetAudio(string filepath)
    {
        filepath = RemoveNumbersFromString(filepath);
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filepath, AudioType.MPEG))
        {
            www.useHttpContinue = false;
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                clip = DownloadHandlerAudioClip.GetContent(www);
                fileepath = filepath;
                LevelInfo.beatTresh = dleveld[0].beatTresh;
                LevelInfo.clipidyclip = clip;
                LevelInfo.secFromStart = dleveld[0].startFrom;
                LevelInfo.endAt = dleveld[0].endAt;
                SceneManager.LoadScene(4);
            }
        }
    }
    public static string RemoveNumbersFromString(string input)
    {
        return Regex.Replace(input, @"-\d+", string.Empty);
    }
}
