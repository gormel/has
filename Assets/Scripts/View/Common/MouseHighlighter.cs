using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    class MouseHighlighter : MonoBehaviour
    {
        public GameObject Highlight;

        void OnMouseOver()
        {
            Highlight.SetActive(true);
        }

        void OnMouseExit()
        {
            Highlight.SetActive(false);
        }
    }
}
