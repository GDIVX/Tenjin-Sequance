namespace Game.Queue
{
    public interface IQueueable
    {
        public float InQueueSpeed { get; set; }
        public float CurrentWaitingTime { get; set; }
        public int CurrentGoalIndex { get; set; }
    }
}