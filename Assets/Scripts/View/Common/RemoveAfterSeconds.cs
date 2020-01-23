using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    public class RemoveAfterSeconds : MonoBehaviour
    {
        public float Seconds;
        void Start()
        {
            StartCoroutine(WaitForRemove(Seconds));
        }

        IEnumerator WaitForRemove(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}
