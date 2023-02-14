using System.Globalization;
using Fiddler;
using Newtonsoft.Json;

namespace enty.Methods;

public class Methods
{

    public void BeforeRequest(Session session)
    {
        if (!session.uriContains("bhvrdbd"))
            return;

        session.bBufferResponse = true;

        FreeBloodweb(session);
        DumpSave(session);
        FilesErrorFix(session);
    }

    public void BeforeResponse(Session session)
    {
        if (!session.uriContains("bhvrdbd"))
            return;
        
        GrabCookie(session);
        InjectMarket(session);
        QueueInfo(session);
    }

    private void FilesErrorFix(Session session)
    {
        if (!(session.uriContains("/logs") | session.uriContains("/batch"))) 
            return;
        
        if(session.GetRequestBodyAsString().Contains("pakvalidation"))
            session["x-breakrequest"] = "FiddlerScript: app.com returned script";
    }
    
    private void DumpSave(Session session)
    {
        if (session.uriContains("/v1/players/me/states/binary?schemaVersion=0&stateName=FullProfile") &&
            Main.savefile)
            File.WriteAllText("dump.txt", session.GetRequestBodyAsString());
    }

    private void InjectMarket(Session session)
    {
        if (!session.uriContains("/v1/inventories") || !Main.market) 
            return;
        
        session.bBufferResponse = true;

        session.utilDecodeResponse();
        session.utilSetResponseBody(Market.Market_json);

        Main.SetStatus("injected market");
    }

    private void GrabCookie(Session session)
    {
        if (!session.RequestHeaders.ToString().Contains("bhvrSession=")) 
            return;

        Main.SetCookie(session.RequestHeaders["Cookie"].Replace("bhvrSession=", ""));
    }

    private void FreeBloodweb(Session session)
    {
        if (!session.uriContains("v1/wallet/withdraw") || !Main.bloodweb) 
            return;
        
        session.utilCreateResponseAndBypassServer();
        session.utilSetResponseBody("{\"userId\":\"null\",\"balance\":0,\"currency\":\"USCents\"}");
    }

    private void QueueInfo(Session session)
    {
        if (!session.fullUrl.EndsWith("v1/queue"))
            return;

        var body = session.GetResponseBodyAsString();

        if (body.Contains(@"QUEUED"))
        {
            Main.SetStatus("QUEUED");

            var queued = JsonConvert.DeserializeObject<queue.QueueStartedRoot>(body);
            if (queued == null)
                return;

            var time = TimeSpan.FromMilliseconds(queued.queueData.ETA);

            Main.Queue.status = queued.queueData.position + " queued";
            Main.Queue.eta = time.ToString(@"m\m\ s\s");
            Main.Queue.country = "unknown";
            Main.Queue.rank = "unknown";
            Main.Queue.rating = "unknown";

            Main.UpdateQueue();
        }

        if (body.Contains(@"MATCHED"))
        {
            Main.SetStatus("MATCHED");

            var matched = JsonConvert.DeserializeObject<queue.QueueMatchedRoot>(body);
            if (matched == null)
                return;

            var data = matched.matchData.skill;

            Main.Queue.status = matched.status;
            Main.Queue.eta = "unknown";
            Main.Queue.country = data.country;
            Main.Queue.rank = data.rank.ToString();
            Main.Queue.rating = Math.Round(data.rating.rating).ToString(CultureInfo.CurrentCulture);

            Main.UpdateQueue();
        }
    }
}