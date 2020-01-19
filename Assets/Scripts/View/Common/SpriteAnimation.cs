using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.View.Common
{
    class SpriteAnimation : MonoBehaviour
    {
        public CustomTileset Tileset;
        public SpriteRenderer Renderer;

        public int[] AnimationIndices;

        public float FrameRate;
        public bool Loop = true;

        private int mCurrentIndex;

        private void Awake()
        {
        }

        void OnEnable()
        {
            if (Tileset.Tiles.Count > 0)
                StartCoroutine(UpdateSprite());
        }

        private IEnumerator UpdateSprite()
        {
            while(true)
            {
                yield return new WaitForSeconds(1 / FrameRate);
                mCurrentIndex = (mCurrentIndex + AnimationIndices.Length + 1) % AnimationIndices.Length;
                if (!Loop && mCurrentIndex == AnimationIndices.Length - 1)
                    break;
            }
        }

        void Update()
        {
            if (Tileset.Tiles.Count > 0)
            {
                var rSize = Renderer.size;
                var spriteIndex = AnimationIndices[mCurrentIndex];
                Renderer.sprite = Tileset.Tiles[spriteIndex];
                Renderer.size = rSize;
            }
        }
    }
}
