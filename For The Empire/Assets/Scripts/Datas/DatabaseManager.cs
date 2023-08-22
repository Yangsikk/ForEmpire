using System.Collections.Generic;

namespace Game.Data {
    public class DatabaseManager : Singleton<DatabaseManager> {
        public GameData gameData;
        public InGameData inGameData;
        public PlayerData playerData;
        public DatabaseManager() {
            playerData = new();
            gameData = new();
            inGameData = new();
        }
    }
}