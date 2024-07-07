using ContainersPortal.Constants;
using ContainersPortal.Controllers;
using ContainersPortal.Models;
using ContainersPortal.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ContainersPortal.Controllers;

public class AccountController : Controller
{
    private readonly ILogger _logger;
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly DockerManagerService _dockerManagerService;
    private readonly DatabaseContext _dbContext;
    private readonly LinuxHelperService _linuxHelperService;
    private const string _imgPath = "/home/milos/UserVolumes/";
    private const string _mountDirPath = "/mnt/user-volumes/";
    private const int _blockSize = 1;
    private const int _blockCount = 1024;


    public AccountController(ILogger<AccountController> logger,
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        DockerManagerService dockerManagerService,
        DatabaseContext dbContext,
        LinuxHelperService linuxHelperService)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
        _dockerManagerService = dockerManagerService;
        _dbContext = dbContext;
        _linuxHelperService = linuxHelperService;
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(UserRegistrationModel userModel)
    {
        if(!ModelState.IsValid)
        {
            return View(userModel);
        }

        var user = new User
        {
            FirstName = userModel.FirstName,
            LastName = userModel.LastName,
            Email = userModel.Email,
            UserName = userModel.Username
        };

        var result = await _userManager.CreateAsync(user, userModel.Password);
        if(!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.TryAddModelError(error.Code, error.Description);
            }

            return View(userModel);
        }

        var AddToRoleResult = await _userManager.AddToRoleAsync(user, "Guest");
        _logger.LogInformation(AddToRoleResult.ToString());

        //var imageName = user.UserName + "-img";
        //var containerName = user.UserName + "-cont";
        //var keysPath = $"./SshKeys/{user.UserName}/";
        //var hostKeysPath = $"~/SshKeys/{user.UserName}/";
        //var imgPath = _imgPath + $"{user.UserName}.img";
        //var mountDirPath = _mountDirPath + $"{user.UserName}-volume/";

        //var port = Int32.Parse(_linuxHelperService
        //    .ExecuteCommandOnHost(GlobalConstants.HOST_USERNAME,
        //                          GlobalConstants.HOST_IP_ADDRESS,
        //                          GlobalConstants.HOST_PASSWORD,
        //                          LinuxHelperService.GET_HOST_UNUSED_PORT));


        //var ipAddress = (_linuxHelperService
        //    .ExecuteCommandOnHost(GlobalConstants.HOST_USERNAME,
        //                          GlobalConstants.HOST_IP_ADDRESS,
        //                          GlobalConstants.HOST_PASSWORD,
        //                          LinuxHelperService.GET_HOST_IP_ADDRESS)).ToString();

        //_logger.LogInformation($"Ip Address: {ipAddress}. Port: {port}");

        //_linuxHelperService.CreateNewVolume(imgPath, mountDirPath, _blockSize, _blockCount);

        //_dockerManagerService.BuildAndRunContainer(
        //    "./Docker/",
        //    imageName,
        //    containerName,
        //    $"{mountDirPath}:{GlobalConstants.USER_CONT_VOLUME_PATH}",
        //    $"{port}:22");

        //Directory.CreateDirectory(keysPath);
        //var sshKeys = _dockerManagerService.CreatePublicPrivateKeyPair(containerName, keysPath, hostKeysPath);

        //var dbUser = _dbContext.Users.Where(u => u.Email == user.Email).FirstOrDefault();
        //if (dbUser == null)
        //    return RedirectToAction(nameof(HomeController.Index), "Home");

        //dbUser.PuttyPrivateKey = sshKeys.PuttyPrivateKey;
        //dbUser.OpenSshPrivateKey = sshKeys.OpenSshPrivateKey;
        //dbUser.PublicKey = sshKeys.PublicKey;
        //dbUser.Port = port;
        //dbUser.IpAddress = ipAddress;
        //dbUser.DockerImageName = imageName;
        //dbUser.DockerContainerName = containerName;
        //dbUser.ImageVolumePath = imgPath;
        //dbUser.MountVolumePath = mountDirPath;

        //await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"User '{user.UserName}' registered.");

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(UserLoginModel userModel, string returnUrl = null)
    {
        _logger.LogInformation($"Return URL: {returnUrl}");

        if (!ModelState.IsValid)
        {
            return View(userModel);
        }

        var result = await _signInManager.PasswordSignInAsync(userModel.Username,
                                                              userModel.Password,
                                                              userModel.RememberMe,
                                                              false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Invalid UserName or Password");
            return View();
        }

        //var dbUser = _dbContext.Users.Where(u => u.UserName == userModel.Username).FirstOrDefault();
        //if (dbUser == null)
        //    return RedirectToAction(nameof(HomeController.Index), "Home");

        //_dockerManagerService.StartContainer(dbUser.DockerContainerName);

        return RedirectToLocal(returnUrl);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        var user = await _userManager.GetUserAsync(User);

        //if (user != null)
        //    _dockerManagerService.StopContainer(user.DockerContainerName);

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }

    private IActionResult RedirectToLocal(string returnUrl)
    {
        if (Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);
        else
            return RedirectToAction(nameof(HomeController.Index), "Home");
    }


}