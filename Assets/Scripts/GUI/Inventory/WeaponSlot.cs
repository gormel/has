using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Inventory
{
    class WeaponSlot : MonoBehaviour, IPointerClickHandler
    {
        public Root Root;
        public ItemIcons IconsDatabase;
        public Image IconTarget;


        public void OnPointerClick(PointerEventData eventData)
        {
            Root.PlayerView.Model<Player>().Inventory.PutOffWeapon();
        }

        void Update()
        {
            IconTarget.sprite = IconsDatabase.GetIcon(Root.PlayerView.Model<Player>().Inventory.Weapon);
        }
    }
}
