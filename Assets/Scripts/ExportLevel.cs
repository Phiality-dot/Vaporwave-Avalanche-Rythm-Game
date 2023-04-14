using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.IO;
using UnityEngine.UI;
using TMPro;
using Unity.SharpZipLib.Utils;

public class ExportLevel : MonoBehaviour
{
    public TMP_InputField BeatTreshold;
    public TMP_InputField Sfrom;
    public TMP_InputField FilePath;
    public TMP_Text uploadtext;
    public randomobjectspawner ROS;
    private string serverUrl = "http://apollo.arcator.co.uk:41062/upload.php";
    private string username = System.Environment.UserName;

    public void ExportLevelSettings()
    {
        string filePath = FilePath.text;

        if (File.Exists(filePath))
        {
            uploadtext.gameObject.SetActive(true);
            if (Directory.Exists(@".\ExpSongs"))
            {
                if (File.Exists(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON"))
                {
                    if (Directory.Exists(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath))))
                    {
                        Directory.Delete(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)), true);
                    }
                    Directory.CreateDirectory(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)));

                    File.Copy(filePath, Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(filePath)), true);
                    File.Copy(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON", Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON")), true);
                    
                    ZipUtility.CompressFolderToZip(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)) + ".zip", null, Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)));
                    Debug.Log("cool");
                    byte[] fileD = File.ReadAllBytes(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)) + ".zip");
                    string fileN = Path.GetFileNameWithoutExtension(filePath) + ".zip";
                    UploadFile(fileD, fileN);
                }
            } else
            {
                Directory.CreateDirectory(@".\ExpSongs");
                if (Directory.Exists(@".\ExpSongs"))
                {
                    if (File.Exists(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON"))
                    {
                        if (Directory.Exists(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath))))
                        {
                            Directory.Delete(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)), true);
                        }
                        Directory.CreateDirectory(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)));

                        File.Copy(filePath, Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(filePath)), true);
                        File.Copy(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON", Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath), Path.GetFileName(@"C:\Users\" + username + @"\Documents\VaporwaveAvalanche\RP.JSON")), true);

                        ZipUtility.CompressFolderToZip(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)) + ".zip", null, Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)));
                        Debug.Log("cool");
                        byte[] fileD = File.ReadAllBytes(Path.Combine(Path.GetFullPath(@".\ExpSongs\"), Path.GetFileNameWithoutExtension(filePath)) + ".zip");
                        string fileN = Path.GetFileNameWithoutExtension(filePath) + ".zip";
                        UploadFile(fileD, fileN);
                    }
                }
                }
        }
    }

    public void UploadFile(byte[] fileData, string fileName)
    {
        StartCoroutine(UploadFileCo(fileData, fileName));
    }

    private IEnumerator UploadFileCo(byte[] fileData, string fileName)
    {
        UnityWebRequest www = new UnityWebRequest();
        WWWForm form = new WWWForm();
        form.AddBinaryData("file", fileData, fileName, "application/zip");
        
        www = UnityWebRequest.Post(serverUrl, form);
        
        www.useHttpContinue = false;
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            uploadtext.text = "Success!";
            Debug.Log("File uploaded successfully!");
        }
    }
}
