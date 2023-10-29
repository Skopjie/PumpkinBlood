using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] BridgeConnection pythonConnection;

    void Start() {
        InitPowerShell();
    }

    void InitPowerShell() {
        Process cmd = new Process();
        cmd.StartInfo.FileName = @"C:\Windows\System32\WindowsPowerShell\v1.0\powershell.exe";

        cmd.StartInfo.RedirectStandardInput = true;
        cmd.StartInfo.UseShellExecute = false;
        cmd.Start();

        //comando activar entorno virtual
        cmd.StandardInput.WriteLine("cd C:\\Users\\Propietario\\OneDrive\\Escritorio\\ProyectosActuales\\python_Frontend_Games");
        cmd.StandardInput.WriteLine("python head_direction.py");
    }

    public static string GetBuildResourcesUrl() {
        string path = Application.dataPath;
        path = path.Replace(Application.productName + "_Data", string.Empty);
        path += "resources";
        return path;
    }

    public static string ConvertPath(string path) {
        path = path.Replace("/", "\\");
        return path;
    }

    public static string GetAssetsResourcesUrl() {
        return Directory.GetParent(Application.dataPath).FullName;
    }

    public static string GetResourcesUrl() {
        string pathResources = Application.isEditor ? GetAssetsResourcesUrl() : GetBuildResourcesUrl();
        return pathResources;
    }
}
