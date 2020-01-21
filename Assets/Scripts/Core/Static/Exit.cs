using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Core.Static
{
    public class Exit : MapObject
    {
        private readonly Game mGame;

        public Exit(Game game)
        {
            mGame = game;
        }

        public override bool IsWalkable => true;
        public override bool IsInteractive => true;

        public override void InteractFrom(Player palyer)
        {
            mGame.CompleteLevel();
        }
    }
}
