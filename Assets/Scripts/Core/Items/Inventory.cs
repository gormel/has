using System.Collections.Generic;
using Assets.Scripts.Core.Items.Base;

namespace Assets.Scripts.Core.Items
{
    public class Inventory
    {
        public int Size { get; } = 60;
        private readonly Player mOwner;
        public Base.Weapon Weapon { get; set; }
        public Base.Armor Armor { get; set; }

        public List<Item> Bag { get; } = new List<Item>();

        public Inventory(Player owner)
        {
            mOwner = owner;
        }

        public bool PutOnWeapon(Item item)//unite weapon & armor
        {
            if (!(item is Base.Weapon))
                return false;

            if (!Bag.Contains(item))
                return false;

            if (Weapon != null)
            {
                Weapon.OnPutOff(mOwner);
                Bag.Add(Weapon);
            }

            Weapon = (Base.Weapon)item;
            Bag.Remove(item);
            item.OnPuton(mOwner);
            return true;
        }

        public bool PutOffWeapon()
        {
            if (Bag.Count >= Size)
                return false;

            Weapon.OnPutOff(mOwner);
            Bag.Add(Weapon);
            Weapon = null;
            return true;
        }

        public bool PutOnArmor(Item item)
        {
            if (!(item is Base.Armor))
                return false;

            if (!Bag.Contains(item))
                return false;

            if (Armor != null)
            {
                Armor.OnPutOff(mOwner);
                Bag.Add(Armor);
            }

            Armor = (Base.Armor)item;
            Bag.Remove(item);
            item.OnPuton(mOwner);
            return true;
        }

        public bool PutOffArmor()
        {
            if (Bag.Count >= Size)
                return false;

            Armor.OnPutOff(mOwner);
            Bag.Add(Armor);
            Armor = null;
            return true;
        }
    }
}
