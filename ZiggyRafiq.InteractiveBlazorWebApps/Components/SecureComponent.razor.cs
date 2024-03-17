using Microsoft.AspNetCore.Components;
using ZiggyRafiq.InteractiveBlazorWebApps.Services;

namespace ZiggyRafiq.InteractiveBlazorWebApps.Components;
public partial class SecureComponent : ComponentBase
{
    private EncryptionService encryptionService = new EncryptionService();
    private string? encryptedMessage;
    private string? decryptedMessage;

    protected override void OnInitialized()
    {
        var message = "Sensitive information";
        encryptedMessage = encryptionService.Encrypt(message);
        decryptedMessage = encryptionService.Decrypt(encryptedMessage);
    }
}
