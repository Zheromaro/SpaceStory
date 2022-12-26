using UnityEngine;
using UnityEngine.UI;
using SpaceGame.Core.SaveSystem;

namespace SpaceGame.UI
{
    public class LevelSelector : MonoBehaviour, IDataPersistence
    {
        private int LevelReached;

        [SerializeField] private Button[] levelButtons;

        private void Start()
        {
            for (int i = 0; i < levelButtons.Length; i++)
            {
                if(i + 1 > LevelReached)
                    levelButtons[i].interactable = false;
            }
        }

        public void LoadData(GameData gameData)
        {
            LevelReached = gameData.LevelReached;
        }

        public void SaveData(GameData gameData)
        {
            gameData.LevelReached = LevelReached;
        }
    }
}
