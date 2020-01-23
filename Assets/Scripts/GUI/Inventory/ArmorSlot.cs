using Assets.Scripts.Core;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Inventory
{
    class ArmorSlot : MonoBehaviour, IPointerClickHandler
    {
        public Root Root;
        public ItemIcons IconsDatabase;
        public Image IconTarget;

        public void OnPointerClick(PointerEventData eventData)
        {
            Root.PlayerView.Model<Player>().Inventory.PutOffArmor();
        }

        void Update()
        {
            IconTarget.sprite = IconsDatabase.GetIcon(Root.PlayerView.Model<Player>().Inventory.Armor);
        }
    }
}