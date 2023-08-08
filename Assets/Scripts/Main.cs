using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

        cmd.StandardInput.WriteLine("cd C:\\Users\\Propietario\\OneDrive\\Escritorio\\ProyectosActuales\\python_Frontend_Games");
        cmd.StandardInput.WriteLine("python head_direction.py");

    }
}
