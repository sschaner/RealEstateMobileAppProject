namespace RealEstateApp
{
    public class HttpClientHandlerService
    {
        // Note: run the following to trust your development certificate ==> dotnet dev-certs https --trust


        /// <summary>
        /// Gets the platform message handler.
        /// </summary>
        /// <returns></returns>
        public HttpMessageHandler GetPlatformMessageHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
    }
}
