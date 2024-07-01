using System.Diagnostics;
using ContainersPortal.Helpers;
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

    public void BuildAndRunContainer(string dockerFilePath, string imageName, 
        string containerName, string? portsMapping = null)
    {
        List<string> commands = new List<string>()
        {
            // $"echo \"laza\" | sudo -S docker build -t {imageName} {dockerFilePath}",
            // $"echo \"laza\" | sudo -S docker run -d {(portsMapping != null ? "-p "+portsMapping : string.Empty)} --name {containerName} {imageName} "// + 
            $"docker build -t {imageName} {dockerFilePath}",
            $"docker run -d {(portsMapping != null ? "-p "+portsMapping : string.Empty)} --name {containerName} {imageName} "// + 
            // $"-v /var/run/docker.sock:/var/run/docker.sock -v $(which docker):/bin/docker",
        };

        foreach (var command in commands)
            _linuxHelperService.ExecuteCommand(command);      
        _logger.LogInformation("Container running.");
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