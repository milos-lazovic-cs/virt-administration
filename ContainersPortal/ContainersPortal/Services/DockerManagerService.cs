using System.Diagnostics;
using ContainersPortal.Constants;
using ContainersPortal.Models;

namespace ContainersPortal.Services;

public class DockerManagerService
{
    private readonly ILogger _logger;
    private readonly LinuxHelperService _linuxHelperService;

    public DockerManagerService(ILogger<DockerManagerService> logger,
        LinuxHelperService linuxHelperService)
    {
        _logger = logger;
        _linuxHelperService = linuxHelperService;
    }

    public void BuildAndRunContainer(string dockerFilePath, string imageName, string containerName,
        string? volumeMappings = null, string? portsMapping = null)
    {
        try
        {
            string buildDockerCont = $"docker build -t {imageName} {dockerFilePath}";

            string port = string.Empty;
            if (portsMapping != null)
                port = $"-p {portsMapping} ";

            string volume = string.Empty;
            if (volumeMappings != null)
                volume = $"-v {volumeMappings} ";

            string runDockerCont = $"docker run -d " + port + volume +
                $"--name {containerName} {imageName} ";

            List<string> commands = new List<string>()
            {
                buildDockerCont,
                runDockerCont
            };

            foreach (var command in commands)
                _linuxHelperService.ExecuteCommand(command);

            _logger.LogInformation($"Container '{containerName}' running.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating container {containerName}: {ex.ToString()}");
        }
    }

    public void BuildDockerImage(string dockerFilePath, string imageName)
    {
        try
        {
            string buildDockerCont = $"docker build -t {imageName} {dockerFilePath}";

            _linuxHelperService.ExecuteCommand(buildDockerCont);

            _logger.LogInformation($"Docker image '{imageName}' building.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while building image {imageName}: {ex.ToString()}");
        }
    }

    public void StartContainer(string containerName)
    {
        try
        {
            string stopContainer = $"docker start {containerName}";
            _linuxHelperService.ExecuteCommand(stopContainer);
            _logger.LogError($"Starting container '{containerName}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Starting container failed.");
        }
    }


    public void StopContainer(string containerName)
    {
        try
        {
            string stopContainer = $"docker stop {containerName}";
            _linuxHelperService.ExecuteCommand(stopContainer);
            _logger.LogError($"Stopping container '{containerName}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Stopping container failed.");
        }
    }

    public void RemoveContainer(string containerName)
    {
        try
        {
            string removeContainer = $"docker rm {containerName}";
            _linuxHelperService.ExecuteCommand(removeContainer);
            _logger.LogError($"Removing container '{containerName}'.");
        }
        catch (Exception ex)
        {
            _logger.LogError("Removing container failed.");
        }
    }



    // ~/.ssh/
    // /home/milos/.ssh/
    public SshKeys CreatePublicPrivateKeyPair(string containerName, string outputPath, string hostKeysPath)
    {
        List<string> commands = new List<string>()
        {
            // $"echo \"laza\" | sudo -S chmod ugo+rw {outputPath}",
            // $"echo \"laza\" | sudo -S ssh-keygen -t rsa -f {outputPath}id_rsa",
            // $"echo \"laza\" | sudo -S chmod ugo+rw {outputPath}id_rsa",
            // $"echo \"laza\" | sudo -S puttygen {outputPath}id_rsa -O private -o {outputPath}id_rsa.ppk",
            // $"echo \"laza\" | sudo -S chmod ugo+rw {outputPath}id_rsa.ppk",
            // $"echo \"laza\" | sudo -S docker cp {outputPath}id_rsa.pub {containerName}:/root/.ssh/authorized_keys"

            $"chmod ugo+rw {outputPath}",
            $"ssh-keygen -t rsa -f {outputPath}id_rsa",
            $"chmod ugo+rw {outputPath}id_rsa",
            $"puttygen {outputPath}id_rsa -O private -o {outputPath}id_rsa.ppk",
            $"chmod ugo+rw {outputPath}id_rsa.ppk",
            $"docker cp {outputPath}id_rsa.pub {containerName}:/root/.ssh/authorized_keys"
        };

        foreach (var command in commands)
            _linuxHelperService.ExecuteCommand(command);

        _logger.LogInformation($"Public and private key pair created. \nPutty private key: {outputPath}id_rsa.ppk.\n" +
            $"OpenSSH private key: {outputPath}id_rsa.\n" +
            $"Public key: {outputPath}id_rsa.pub.");

        return new SshKeys
        {
            PuttyPrivateKey = File.ReadAllText($"{outputPath}id_rsa.ppk"),
            OpenSshPrivateKey = File.ReadAllText($"{outputPath}id_rsa"),
            PublicKey = File.ReadAllText($"{outputPath}id_rsa.pub")
        };
    }




}