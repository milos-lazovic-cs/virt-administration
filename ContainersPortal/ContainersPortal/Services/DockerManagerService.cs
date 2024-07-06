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
        string? volumePath = null, string? portsMapping = null)
    {
        try
        {
            string buildDockerCont = $"docker build -t {imageName} {dockerFilePath}";

            string port = string.Empty;
            if (portsMapping != null)
                port = $"-p {portsMapping} ";

            string volume = string.Empty;
            if (volumePath != null)
                volume = $"-v {volumePath}:{GlobalConstants.USER_CONT_VOLUME_PATH} ";

            string runDockerCont = $"docker run -d " + port + volume +
                $"--name {containerName} {imageName} ";

            //$"{(portsMapping != null ? "-p " + portsMapping : string.Empty)} " +
            //$"{(volumePath != null ? $"-v {volumePath}:{GlobalConstants.USER_CONT_VOLUME_PATH}" : string.Empty)} " +

            List<string> commands = new List<string>()
            {
                buildDockerCont,
                runDockerCont
            };

            foreach (var command in commands)
                _linuxHelperService.ExecuteCommand(command);

            _logger.LogInformation("Container running.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while creating container: {ex.ToString()}");
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

        return new SshKeys
        {
            PuttyPrivateKey = File.ReadAllText($"{outputPath}id_rsa.ppk"),
            OpenSshPrivateKey = File.ReadAllText($"{outputPath}id_rsa"),
            PublicKey = File.ReadAllText($"{outputPath}id_rsa.pub")
        };
    }




}