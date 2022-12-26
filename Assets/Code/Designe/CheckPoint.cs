using UnityEngine;
using SpaceGame.Core.SaveSystem;

namespace SpaceGame.Designe
{
    public class CheckPoint : CheckForPlayer, IDataPersistence
    {
        private Transform RespawnPoint;

        public override void Here()
        {
            RespawnPoint = transform;
        }

        public void LoadData(GameData data)
        {
            // Nothing... 
        }

        public void SaveData(GameData data)
        {
            data.playerPosition = RespawnPoint.position;
        }
    }
}
