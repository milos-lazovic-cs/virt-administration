using Microsoft.AspNetCore.Identity;

namespace ContainersPortal.Models;

public class User : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PuttyPrivateKey { get; set; } = string.Empty;
    public string OpenSshPrivateKey { get; set; } = string.Empty;
    public string PublicKey { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public int Port { get; set; }
    public string DockerImageName { get; set;} = string.Empty;
    public string DockerContainerName { get ; set; } = string.Empty;
    public string ImageVolumePath { get; set; } = string.Empty;
    public string MountVolumePath { get; set; } = string.Empty;

}