using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Items.Base;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Inventory
{
    public class BugItem : MonoBehaviour, IPointerClickHandler
    {
        public Player Player;
        public Item Item;
        public Image IconTarget;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                Player.Inventory.PutOnArmor(Item); //one method
                Player.Inventory.PutOnWeapon(Item);
            }

            if (eventData.button == PointerEventData.InputButton.Right)
            {
                Player.DropOut(Item);
            }
        }
    }
}
