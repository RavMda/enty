namespace enty.Methods;

public partial class queue
{
    public class QueueStarted
    {
        public double ETA { get; set; }
        public int position { get; set; }
        public bool stable { get; set; }
        public int sizeA { get; set; }
        public int sizeB { get; set; }
    }

    public class QueueStartedRoot
    {
        public string status { get; set; }
        public QueueStarted queueData { get; set; }
    }
}