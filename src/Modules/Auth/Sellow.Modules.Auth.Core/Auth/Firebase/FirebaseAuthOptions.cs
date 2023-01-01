namespace Sellow.Modules.Auth.Core.Auth.Firebase;

internal sealed class FirebaseAuthOptions
{
    public string ApiKeyFilePath { get; set; } = null!;
    public string ProjectId { get; set; } = null!;
    public string Authority { get; set; } = null!;
    public string ValidIssuer { get; set; } = null!;
    public string ValidAudience { get; set; } = null!;
}