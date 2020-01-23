using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.View.Common;
using UnityEngine;

namespace Assets.Scripts.GUI.Inventory
{
    class BugList : MonoBehaviour
    {
        public Root Root;
        public ItemIcons IconsDatabase;
        public GameObject BugItemPrefab;

        private CollectionObserver<Item> mBugObserver;
        private Dictionary<Item, BugItem> mBugItems = new Dictionary<Item, BugItem>();

        void Awake()
        {
            mBugObserver = new CollectionObserver<Item>(Root.PlayerView.Model<Player>().Inventory.Bag);
            mBugObserver.Added += BugObserverOnAdded;
            mBugObserver.Removed += BugObserverOnRemoved;
        }

        private void BugObserverOnRemoved(object sender, CollectionChangedEventArgs<Item> e)
        {
            mBugItems[e.Elem].transform.SetParent(null);
            Destroy(mBugItems[e.Elem].gameObject);
            mBugItems.Remove(e.Elem);
        }

        private void BugObserverOnAdded(object sender, CollectionChangedEventArgs<Item> e)
        {
            var inst = Instantiate(BugItemPrefab);
            inst.transform.SetParent(transform);
            var bugItem = inst.GetComponent<BugItem>();
            bugItem.Item = e.Elem;
            bugItem.Player = Root.PlayerView.Model<Player>();
            bugItem.IconTarget.sprite = IconsDatabase.GetIcon(e.Elem);
            mBugItems[e.Elem] = bugItem;
        }

        void Update()
        {
            mBugObserver.Update();
        }
    }
}
