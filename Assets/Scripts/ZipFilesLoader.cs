using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class ZipFilesLoader : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_InputField inputField;
    private string serverUrl = "http://apollo.arcator.co.uk:41062/get_zip_files.php";
    private List<string> options;

    private void Start()
    {
        StartCoroutine(GetZipFiles());
        inputField.onValueChanged.AddListener(OnInputValueChanged);
    }
    private void OnInputValueChanged(string value)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options.Where(s => s.Contains(value)).ToList());
    }

    private IEnumerator GetZipFiles()
    {
        UnityWebRequest www = UnityWebRequest.Get(serverUrl);
        www.useHttpContinue = false;
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string[] files = www.downloadHandler.text.Split(',');
            dropdown.ClearOptions();
            dropdown.AddOptions(new List<string>(files));
            options = new List<string>(files);
        }
    }
}
