using System.Collections.Generic;

namespace Game.Data {
    public class DatabaseManager : Singleton<DatabaseManager> {
        public GameData gameData;
        public InGameData inGameData;
        public DatabaseManager() {
            gameData = new();
            inGameData = new();
        }
    }
}