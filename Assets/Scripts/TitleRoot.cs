using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class TitleRoot : MonoBehaviour
    {
        public string LevelScene;

        public void StartLevel()
        {
            //init default params
            Root.SavedPlayer = null;
            Root.Level = 0;
            SceneManager.LoadScene(LevelScene);
        }
    }
}
