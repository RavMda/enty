namespace enty;

public static class Main
{
    private static class Positions
    {
        public const int Status = 1;
        public const int Cookie = 2;
        public const int Buttons = 4;
        public const int Queue = 10;
    }
    
    public static class Queue
    {
        public static string country { get; set; }
        public static string eta { get; set; }
        public static string rank { get; set; }
        public static string rating { get; set; }
        public static string status { get; set; }
    }

    private static string cookie { get; set; } = "";

    public static bool bloodweb { get; private set; }
    public static bool market { get; private set; }
    public static bool flashlight { get; private set; }
    public static bool savefile { get; private set; }

    private static readonly List<Button> Buttons = new();

    static Main()
    {
        Console.CursorVisible = false;

        SetStatus("loading...");
        SetCookie("unknown");

        // default values ye
        bloodweb = true;
        market = true;
        flashlight = true;
        savefile = false;

        // free bloodweb
        AddButton("Free Bloodweb", 'F', delegate(bool b) { bloodweb = b; });

        // all skins & charms & characters
        AddButton("Inject Market", 'G', delegate(bool b) { market = b; });

        // beamer clicker when C is pressed
        AddButton("Beamer Spam", 'C', delegate(bool b) { flashlight = b; });

        // dumps savefile just in case 
        AddButton("Dump Savefile", 'V', delegate(bool b) { savefile = b; });

        UpdateButtons();

        Queue.status = "unknown";
        Queue.eta = "unknown";
        Queue.country = "unknown";
        Queue.rank = "unknown";
        Queue.rating = "unknown";

        UpdateQueue();
    }

    public static void SetStatus(string text)
    {
        // print status in console
        Colory.Print(Positions.Status, ":cdy:[!] Status: :cc:" + text);
    }

    public static void SetCookie(string newCookie)
    {
        // if its the same one dont do anything
        if (newCookie == cookie)
            return;

        // set the cookie
        cookie = newCookie;
        
        // strip characters
        if (newCookie.Length >= 18)
            newCookie = newCookie[..18];
        
        // print in console
        Colory.Print(Positions.Cookie, ":cdy:[*] Cookie: :cc:" + newCookie + "... :cg:[R to Copy]");

        // change status
        SetStatus("grabbed cookie");
    }

    public static void UpdateQueue()
    {
        Colory.Print(Positions.Queue - 1, ":cdy:[i] Queue Info [i]");
        
        Colory.Print(Positions.Queue + 1, ":cdy:[-] Status: :cc:" + Queue.status);
        Colory.Print(Positions.Queue + 2, ":cdy:[-] ETA: :cc:" + Queue.eta);
        Colory.Print(Positions.Queue + 3, ":cdy:[-] Country: :cc:" + Queue.country);
        Colory.Print(Positions.Queue + 4, ":cdy:[-] Rank: :cc:" + Queue.rank);
        Colory.Print(Positions.Queue + 5, ":cdy:[-] MMR: :cc:" + Queue.rating);
    }

    private static void UpdateButtons()
    {
        foreach (var button in Buttons)
        {
            // get button position
            var pos = Positions.Buttons + Buttons.IndexOf(button);
            
            // check if button is enabled
            var boolean = button.enabled ? "[+]" : "[ ]";
                
            // add spaces to make it look nicer
            var spaces = new string(' ', 15 - button.name.Length);
                
            // yeah length limit
            const string text = ":cr:{0} :cc:{1}{2}:ccc:[{3}]";

            // format it
            var str = string.Format(text, boolean, button.name, spaces, button.key);
            
            // print!
            Colory.Print(pos, str);
        }
    }

    private static void AddButton(string name, char key, Action<bool> callback)
    {
        var button = new Button
        {
            name = name,
            enabled = true,
            key = key,
            callback = callback
        };

        Buttons.Add(button);
    }

    public static void ReadKey()
    {
        var key = Console.ReadKey(true).KeyChar;

        foreach (var button in Buttons.Where(button => char.ToUpper(key) == button.key))
        {
            button.enabled = !button.enabled;
            button.callback(button.enabled);

            UpdateButtons();
        }

        if (char.ToLower(key) != 'r') 
            return;
        
        Clipboard.Set(cookie);
        SetStatus("copied cookie");
    }
    
    private class Button
    {
        public string name { get; init; }
        public bool enabled { get; set; }
        public char key { get; init; }
        public Action<bool> callback { get; init; }
    }
}