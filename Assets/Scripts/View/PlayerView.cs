using Assets.Scripts.Core;
using Assets.Scripts.Core.NPC;
using Assets.Scripts.View.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core.Skills;
using Assets.Scripts.Core.Static;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.View
{
    public class PlayerView : BaseView
    {
        private Player mPlayer;
        private Root mRoot;

        public DirectionSelector Move;
        public DirectionSelector Stand;
        public DirectionSelector Attack;

        public KeyCode[] SkillKeys;

        public override void Load<T>(T model, Root root)
        {
            mPlayer = model as Player;
            mRoot = root;

            mPlayer.Destroyed += Player_Destroyed;
            transform.localScale = mPlayer.Size;
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
                mPlayer.SetMoveDirection(Input.mousePosition - new Vector3(Screen.width / 2f, Screen.height / 2f));
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
                var ray = Camera.main.ScreenPointToRay(m);
                if (Physics.Raycast(ray, out var hit))
                {
                    var view = hit.collider.gameObject.GetComponentInChildren<BaseView>();
                    if (view != null)
                    {
                        ProcessMonsterClick(view.Model<Monster>());
                        ProcessStaticClick(view.Model<MapObject>(), Camera.main.ScreenToWorldPoint(m));
                    }
                }
            }


            for (int i = 0; i < 4; i++)
            {
                if (Input.GetKeyDown(SkillKeys[i]))
                {
                    var skill = mRoot.SelectedSkills[i]?.Model<Skill>();
                    if (skill != null)
                        skill.Use(Camera.main.ScreenToWorldPoint(Input.mousePosition));
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

        private void ProcessStaticClick(MapObject obj, Vector2 pointer)
        {
            if (obj == null)
                return;

            if (!obj.IsInteractive)
                return;

            if (Vector2.Distance(pointer, mPlayer.Position) < 1.2f)
                obj.InteractFrom(mPlayer);
        }

        public override T Model<T>()
        {
            return mPlayer as T;
        }
    }
}
