using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Scripts.GUI.Inventory
{
    public class ItemInfoPopup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject Popup;
        public Text InfoTarget;
        public BugItem ItemSource;

        public void OnPointerEnter(PointerEventData eventData)
        {
            InfoTarget.text = string.Join(Environment.NewLine, ItemSource.Item.PropertyDescriptions);
            Popup.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Popup.SetActive(false);
        }
    }
}
