using UnityEngine;

namespace SpaceGame.Core.SaveSystem
{
    [System.Serializable]
    public class GameData
    {
        public int LevelReached;
        public Vector3 playerPosition;

        //public SerializableDictionary<string, bool> checkPoints; 
        
        // the values defined in this constructor will be the default values
        // the game starts with when there's no data to load
        public GameData()
        {
            LevelReached = 1;
            playerPosition = Vector3.zero;

            //checkPoints = new SerializableDictionary<string, bool>();
        }

    }
}
