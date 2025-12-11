using System.Net;
using System.Security.Cryptography.X509Certificates;
using Blaze2SDK;
using Blaze3SDK;
using NLog;
using NLog.Layouts;
using LogLevel = NLog.LogLevel;

namespace TheDirector;

internal class Program
{
    public const string Name = "TheDirector 1.0";

    private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

    private static async Task Main(string[] args)
    {
        StartLogger();
        
        var redirectorTaskSdk2 = StartRedirectorServerSdk2();
        var redirectorTaskSdk3 = StartRedirectorServerSdk3();
        
        Logger.Warn(Name + " started");
        await Task.WhenAll(redirectorTaskSdk2, redirectorTaskSdk3);
    }

    private static void StartLogger()
    {
        var logLevel = LogLevel.Trace;
        var layout = new SimpleLayout("[${longdate}][${callsite-filename:includeSourcePath=false}(${callsite-linenumber})][${level:uppercase=true}]: ${message:withexception=true}");
        LogManager.Setup().LoadConfiguration(builder =>
        {
            builder.ForLogger().FilterMinLevel(logLevel)
                .WriteToConsole(layout)
                .WriteToFile("logs/server-${shortdate}.log", layout);
        });
    }

    private static async Task StartRedirectorServerSdk2()
    {
        var redirector = Blaze2.CreateBlazeServer("RedirectorServer", new IPEndPoint(IPAddress.Any, 42100));
        redirector.AddComponent<RedirectorComponent>();
        await redirector.Start(-1).ConfigureAwait(false);
    }
    
    private static async Task StartRedirectorServerSdk3()
    {
        var certBytes = File.ReadAllBytes("gosredirector_mod.pfx");
        X509Certificate cert = new X509Certificate2(certBytes, "123456");

        var redirector = Blaze3.CreateBlazeServer("RedirectorServer", new IPEndPoint(IPAddress.Any, 42127), cert);
        redirector.AddComponent<RedirectorComponent>();
        await redirector.Start(-1).ConfigureAwait(false);
    }
    
}