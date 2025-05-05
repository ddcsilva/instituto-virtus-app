using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

namespace InstitutoVirtusApp.API.Extensions;

public static class FirebaseExtension
{
    public static void AddFirebase(this IServiceCollection services, IConfiguration configuration)
    {
        FirebaseApp.Create(new AppOptions()
        {
            Credential = GoogleCredential.FromFile("secrets/firebase-service-account.json")
        });
    }
}
