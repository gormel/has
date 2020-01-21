namespace Assets.Scripts.Core.Static {
    public class MapVoid : MapObject
    {
        public override bool IsWalkable => false;
        public override bool IsInteractive => false;
    }
}