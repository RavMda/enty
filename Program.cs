using fgasfsasf;
using fgasfsasf.Methods;

var methods = new Methods();

// starting proxy
Proxy.Start();

// add events to the proxy
Proxy.AddEvent(methods.BeforeRequest, true);
Proxy.AddEvent(methods.BeforeResponse, false);

// disable proxy on program exit    
AppDomain.CurrentDomain.ProcessExit += delegate { Proxy.Stop(); };

// flashlight clicker in different thread
Clicker.Start();

// thing that waits for console input yea
while (true) Main.ReadKey();