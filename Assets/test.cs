using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Diagnostics;
using System.IO;
using System;

public class test
{
    public static void DownloadAudioClip(string youtubeUrl)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = $"/C yt-dlp --extract-audio --audio-format wav --output " + Path.Combine(Application.persistentDataPath, "ytdls", @"(title)s.% (ext)s") + " " + youtubeUrl,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = false
        };
    
    using (Process process = new Process { StartInfo = startInfo })
        {
            process.Start();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                string errorMessage = process.StandardError.ReadToEnd();
                throw new Exception($"Download failed with error: {errorMessage}");
            }
        }
    }

}