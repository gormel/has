using System;
using System.Collections.Generic;
using Assets.Scripts.Core.Items.Armors;
using Assets.Scripts.Core.Items.Base;
using Assets.Scripts.Core.Items.Weapons;
using Random = UnityEngine.Random;

namespace Assets.Scripts.Core.NPC
{
    static class DropTable
    {
        private class DropRecord
        {
            public float Possibility { get; }
            public Func<int, Item> CreateLoot { get; }

            public DropRecord(float possibility, Func<int, Item> createLoot)
            {
                Possibility = possibility;
                CreateLoot = createLoot;
            }
        }

        private static List<DropRecord> mTable = new List<DropRecord>
        {
            new DropRecord(0.3f, lvl => new Chainmail(lvl)),
            new DropRecord(0.3f, lvl => new Robe(lvl)),
            new DropRecord(0.001f, lvl => new ChainmailBikini(lvl)),

            new DropRecord(0.3f, lvl => new Sword(lvl)),
            new DropRecord(0.3f, lvl => new Spear(lvl)),
            new DropRecord(0.3f, lvl => new Staff(lvl)),
        };

        public static Item GenerateLoot(int level)
        {
            if (Random.value > 0.3)
                return null;

            var possibles = new List<Item>();
            foreach (var record in mTable)
            {
                if (Random.value < record.Possibility)
                    possibles.Add(record.CreateLoot(level));
            }

            if (possibles.Count == 0)
                return null;

            return possibles[Random.Range(0, possibles.Count)];
        }
    }
}
