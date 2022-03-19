namespace fgasfsasf.Methods;

public partial class queue
{
    public class Party
    {
        public List<object> _customizationMesh { get; set; }
        public string _playerName { get; set; }
        public string _platformSessionId { get; set; }
        public string _uniquePlayerId { get; set; }
        public int _playerRank { get; set; }
        public int _characterLevel { get; set; }
        public int _prestigeLevel { get; set; }
        public int _gameRole { get; set; }
        public int _characterId { get; set; }
        public string _powerId { get; set; }
        public int _location { get; set; }
        public bool _ready { get; set; }
        public bool _crossplayAllowed { get; set; }
        public string _playerPlatform { get; set; }
        public string _playerProvider { get; set; }
        public string _postMatchmakingRole { get; set; }
        public string _postMatchmakingState { get; set; }
        public int _roleRequested { get; set; }
        public bool _isStateInitialized { get; set; }
        public int _disconnectionPenaltyEndOfBan { get; set; }
    }

    public class PartyRoot
    {
        public Party body { get; set; }
    }
}