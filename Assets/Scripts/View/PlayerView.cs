using Assets.Scripts.Core;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.View
{
    public class PlayerView : BaseView
    {
        private Player mPlayer;
        private MapView mMapView;
        private Root mRoot;

        public DirectionSelector Move;
        public DirectionSelector Stand;
        public DirectionSelector Attack;

        public override void Load<T>(T model, Root root)
        {
            mPlayer = model as Player;
            mMapView = root.MapView;
            mRoot = root;

            mPlayer.Destroyed += Player_Destroyed;
        }

        private void Player_Destroyed(object sender, EventArgs e)
        {
            mPlayer.Destroyed -= Player_Destroyed;
            mRoot.GameOver();
        }

        void Update()
        {
            mPlayer.Update(TimeSpan.FromSeconds(Time.deltaTime));

            transform.localPosition = mPlayer.Position;

            if (Input.GetMouseButton((int)MouseButton.RightMouse))
            {
                mPlayer.SetMoveDirection(Input.mousePosition - new Vector3(Screen.width / 2, Screen.height / 2));
                Stand.Enabled = false;
                Move.Enabled = !Attack.Enabled;
                Move.Direction = mPlayer.Direction;
            }
            else
            {
                Stand.Enabled = !Attack.Enabled;
                if (mPlayer.Direction != Vector2.zero)
                    Stand.Direction = mPlayer.Direction;

                mPlayer.SetMoveDirection(Vector2.zero);
                Move.Enabled = false;
            }

            if (Input.GetMouseButton((int)MouseButton.LeftMouse))
            {
                var m = Input.mousePosition;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(m), out var hit))
                {
                    var view = hit.collider.gameObject.GetComponentInChildren<BaseView>();
                    if (view != null)
                    {
                        ProcessMonsterClick(view.Model<Monster>());
                    }
                }
            }

            mRoot.HealthBar.transform.localScale = new Vector3(1, mPlayer.Health / mPlayer.MaxHealth.Value);
            mRoot.ManaBar.transform.localScale = new Vector3(1, mPlayer.Mana / mPlayer.MaxMana.Value);
        }

        private IEnumerator AnimateAttack(Vector2 dir)
        {
            Attack.Enabled = true;
            Attack.Direction = dir;
            yield return new WaitForSeconds(0.9f);
            Attack.Enabled = false;
        }

        private void ProcessMonsterClick(Monster m)
        {
            if (m == null)
                return;

            if (mPlayer.AttackMonster(m))
            {
                StartCoroutine(AnimateAttack(m.Position - mPlayer.Position));
            }
        }

        public override T Model<T>()
        {
            return mPlayer as T;
        }
    }
}
