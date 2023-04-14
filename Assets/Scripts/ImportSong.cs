using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Networking;
using TMPro;
using System.Diagnostics;
using System.Net;
public class ImportSong : MonoBehaviour
{
    public Button importButton;
    public Button actualButton;
    public Button[] otherButtons;
    public TMPro.TMP_Dropdown[] otherdrops;
    public TMPro.TMP_InputField[] otherinputs;
    public TMPro.TMP_Text[] othertext;
    public TMPro.TMP_InputField inputField;
    public TMPro.TMP_InputField BeatTreshold;
    public TMPro.TMP_InputField Sfrom;
    public TMPro.TMP_InputField endAt;
    public AudioSource audioSource;
    public randomobjectspawner ROS;
    public Button[] ndButtons;
    AudioClip clip = null;
    public RecentlyPlayed rp;
    private string fileepath;
    public TMP_Dropdown dd;
    public TMP_Text t;
    public TMP_Text fillout;
    
    private void Start()
    {
        importButton.onClick.AddListener(Import);
        actualButton.onClick.AddListener(delegate () { LoadSong(inputField.text); });
        inputField.text = "";
        inputField.placeholder.color = Color.black;
        BeatTreshold.placeholder.color = Color.black;
        Sfrom.placeholder.color = Color.black;
        endAt.placeholder.color = Color.black;
        inputField.gameObject.SetActive(false);
        BeatTreshold.gameObject.SetActive(false);
        Sfrom.gameObject.SetActive(false);
        endAt.gameObject.SetActive(false);
    }

    private void Import()
    {
        HideButtons();
        dd.gameObject.SetActive(true);
        t.gameObject.SetActive(true);
        inputField.gameObject.SetActive(true);
        BeatTreshold.gameObject.SetActive(true);
        Sfrom.gameObject.SetActive(true);
        endAt.gameObject.SetActive(true);
    }

    private void HideButtons()
    {
        foreach (Button button in otherButtons)
        {
            button.gameObject.SetActive(false);
        }
        foreach (TMPro.TMP_Dropdown button in otherdrops)
        {
            button.gameObject.SetActive(false);
        }
        foreach (TMPro.TMP_Text button in othertext)
        {
            button.gameObject.SetActive(false);
        }
        foreach (TMP_InputField button in otherinputs)
        {
            button.gameObject.SetActive(false);
        }
    }
    IEnumerator GetAudio(string filepath)
    {
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + filepath, AudioType.MPEG))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                UnityEngine.Debug.Log(www.error);
            }
            else
            {
                 clip = DownloadHandlerAudioClip.GetContent(www);
                 fileepath = filepath;
            }
        }
    }
    public void DownloadSong(string songUrl)
    {
        string outputFolder = Application.dataPath + "/Songs/";

        // Get YouTube video name
        ProcessStartInfo info = new ProcessStartInfo();
        info.FileName = "yt-dlp.exe";
        info.CreateNoWindow = true;
        info.Arguments = $"--get-title \"{songUrl}\"";
        info.RedirectStandardOutput = true;
        info.UseShellExecute = false;
        Process process = Process.Start(info);
        string videoTitle = process.StandardOutput.ReadToEnd().Trim();
        process.WaitForExit();

        // Remove invalid characters from video title
        char[] invalidChars = Path.GetInvalidFileNameChars();
        foreach (char c in invalidChars)
        {
            videoTitle = videoTitle.Replace(c, '-');
        }

        // Build output file path and name
        string fileName = $"{videoTitle}.mp3";
        
        string outputFile = Path.Combine(outputFolder, fileName);

        // Run YT-DLP command to download song in MP3 format
        ProcessStartInfo processInfo = new ProcessStartInfo();
        processInfo.FileName = "yt-dlp.exe";
        processInfo.Arguments = $"--extract-audio --audio-format mp3 --audio-quality 0 -o \"{outputFile}\" \"{songUrl}\" -S ext:mp3 --extract-audio --audio-format mp3";
        Process process2 = Process.Start(processInfo);
        process2.WaitForExit();
        // Convert M4A to MP3 using FFMPEG
        ProcessStartInfo processInfo3 = new ProcessStartInfo();
        processInfo3.FileName = "ffmpeg.exe";
        processInfo3.CreateNoWindow = true;
        processInfo3.Arguments = $"-i \"{outputFile}\" -vn -ar 44100 -ac 2 -b:a 192k \"{outputFile.Replace("m4a", "mp3")}\" -y";
        Process process3 = Process.Start(processInfo3);
        process3.WaitForExit();

        // Load downloaded MP3 file into AudioClip
        StartCoroutine(GetAudio(outputFile.Replace("m4a", "mp3")));
        fileepath = outputFile.Replace("m4a", "mp3");
    }
    public void LoadSong(string filePath)
    {


        string fileName = Path.GetFileName(filePath);
        UnityEngine.Debug.Log("Starting check");
        if (File.Exists(filePath) && float.Parse(BeatTreshold.text) > 0f && float.Parse(Sfrom.text) > -1f && float.Parse(endAt.text) > 0f && Path.GetExtension(filePath) == ".mp3" || filePath.StartsWith("https://www.youtube.com") || filePath.StartsWith("https://youtube.com"))
        {
            if (filePath.StartsWith("https://www.youtube.com") || filePath.StartsWith("https://youtube.com"))
            {
                DownloadSong(filePath);
            }
            else
            {
                StartCoroutine(GetAudio(filePath));
            }
            

            if (clip != null)
            {
                LevelInfo.clipidyclip = clip;
                SceneManager.LoadScene(4, LoadSceneMode.Additive);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
        }
        else
        {
            fillout.gameObject.SetActive(true);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        GameObject audioSourceObject = GameObject.Find("cMusic");
        //DontDestroyOnLoad(audioSourceObject);
        UnityEngine.Debug.Log(clip);
        if (audioSourceObject != null)
        {
            AudioSource audioSource = audioSourceObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.Play();
                audioSource.loop = true;
                UnityEngine.Debug.Log(audioSource.isPlaying);
                UnityEngine.Debug.Log(audioSource.clip);
                audioSource.time = float.Parse(Sfrom.text);
                LevelInfo.secFromStart = float.Parse(Sfrom.text);
            }
            if(endAt.text == "0" || endAt == null)
            {
                LevelInfo.endAt = clip.length;
            } else
            {
                LevelInfo.endAt = float.Parse(endAt.text);
            }
            ROS = GameObject.Find("masterspawner").GetComponent<randomobjectspawner>();
            ROS.beatThreshold = float.Parse(BeatTreshold.text);
            LevelInfo.beatTresh = float.Parse(BeatTreshold.text);
            rp.addToPlayed(fileepath, float.Parse(Sfrom.text), float.Parse(BeatTreshold.text), LevelInfo.endAt);
            inputField.gameObject.SetActive(false);
            BeatTreshold.gameObject.SetActive(false);
            Sfrom.gameObject.SetActive(false);
            audioSource.Play();
            UnityEngine.Debug.Log(audioSource.isPlaying);
            
            SceneManager.UnloadScene(0);
        }
    }
}
