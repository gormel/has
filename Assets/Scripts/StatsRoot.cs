using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.GUI.Inventory;
using Assets.Scripts.View.Items;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class StatsRoot : MonoBehaviour
    {
        public string TitleScene;

        public ItemIcons ItemsDatabase;

        public Text LevelNumber;
        public Text Kills;
        public Text CharacterLevel;
        public Image Weapon;
        public Image Armor;
        public Text WeaponStats;
        public Text ArmorStats;

        void Start()
        {
            LevelNumber.text = Root.Level.ToString();
            Kills.text = Root.SavedPlayer.Kills.ToString();
            CharacterLevel.text = Root.SavedPlayer.Level.ToString();
            if (Root.SavedPlayer.Inventory.Weapon != null)
            {
                Weapon.sprite = ItemsDatabase.GetIcon(Root.SavedPlayer.Inventory.Weapon);
                WeaponStats.text = string.Join(Environment.NewLine, Root.SavedPlayer.Inventory.Weapon.PropertyDescriptions);
            }

            if (Root.SavedPlayer.Inventory.Armor != null)
            {
                Armor.sprite = ItemsDatabase.GetIcon(Root.SavedPlayer.Inventory.Armor);
                ArmorStats.text = string.Join(Environment.NewLine, Root.SavedPlayer.Inventory.Armor.PropertyDescriptions);
            }
        }

        public void GoToTitle()
        {
            SceneManager.LoadScene(TitleScene);
        }
    }
}
