using Fiddler;

namespace fgasfsasf;

public static class Proxy
{
    // start proxy
    public static void Start()
    {
        Main.SetStatus("starting proxy...");

        CreateCertificate();

        var startupSettings = new FiddlerCoreStartupSettingsBuilder()
            .RegisterAsSystemProxy()
            .DecryptSSL()
            .Build();
        FiddlerApplication.Startup(startupSettings);

        Main.SetStatus("started");
    }

    // stop proxy
    public static void Stop()
    {
        if (FiddlerApplication.IsStarted())
            FiddlerApplication.Shutdown();
        
        RemoveCertificate();
    }

    private static void CreateCertificate()
    {
        if (CertMaker.rootCertExists()) 
            return;
        
        CertMaker.createRootCert();
        CertMaker.trustRootCert();
    }

    private static void RemoveCertificate()
    {
        if (CertMaker.rootCertExists()) CertMaker.removeFiddlerGeneratedCerts(true);
    }

    // add event to fiddler
    public static void AddEvent(SessionStateHandler func, bool beforeRequest)
    {
        if (beforeRequest)
            FiddlerApplication.BeforeRequest += func;
        else
            FiddlerApplication.BeforeResponse += func;
    }

    // remove event from fiddler
    public static void RemoveEvent(SessionStateHandler func, bool beforeRequest)
    {
        if (beforeRequest)
            FiddlerApplication.BeforeRequest -= func;
        else
            FiddlerApplication.BeforeResponse -= func;
    }
}