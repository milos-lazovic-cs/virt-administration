using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;

namespace ContainersPortal.Helpers;

public class LinuxHelperService
{

    public const string GET_HOST_IP_ADDRESS = "hostname -I | awk '{print $1}'";
    public const string GET_HOST_UNUSED_PORT = "'{ start_port=6000; while true; do (echo >/dev/tcp/localhost/$start_port) >/dev/null 2>&1; [ $? -ne 0 ] && { echo $start_port; break; } || start_port=$((start_port + 1)); done }'";

    private readonly ILogger _logger;

    public LinuxHelperService(ILogger<LinuxHelperService> logger)
    {
        _logger = logger;        
    }

    public string ExecuteCommand(string command)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();

            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            
            process.WaitForExit();

            _logger.LogInformation("Command: " + command);
            _logger.LogInformation("Output: " + output);
            if (!string.IsNullOrEmpty(error))
                _logger.LogError("Error: " + error);

            return output;
        }
    }

    public string ExecuteCommandOnHost(string username, string ipAddress, string password, string script)
    {
        string command = $"sshpass -p '{password}' ssh -o StrictHostKeyChecking=no {username}@{ipAddress} {script}";
        //string command = $"sshpass -p '{password}' ssh {username}@{ipAddress} {script}";
        //string command = $"ssh {username}@{ipAddress} {script}";
        return ExecuteCommand(command);
    }
    
    public int GetAvailablePort(int startingPort)
    {
        IPEndPoint[] endPoints;
        List<int> portArray = new List<int>();

        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();

        //getting active connections
        TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
        portArray.AddRange(from n in connections
                            where n.LocalEndPoint.Port >= startingPort
                            select n.LocalEndPoint.Port);

        //getting active tcp listners - WCF service listening in tcp
        endPoints = properties.GetActiveTcpListeners();
        portArray.AddRange(from n in endPoints
                            where n.Port >= startingPort
                            select n.Port);

        //getting active udp listeners
        endPoints = properties.GetActiveUdpListeners();
        portArray.AddRange(from n in endPoints
                            where n.Port >= startingPort
                            select n.Port);

        portArray.Sort();

        for (int i = startingPort; i < UInt16.MaxValue; i++)
            if (!portArray.Contains(i))
                return i;

        return 0;
    }
}