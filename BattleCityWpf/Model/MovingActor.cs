using System;
using System.Linq;

namespace BattleCityWpf.Model
{
    public abstract class MovingGameObject : GameObject
    {
        private readonly GameField _field;
        public override int Layer => 1;
        public Facing Facing { get; set; }
        public CellLocation CellLocation { get; private set; }
        public CellLocation TargetCellLocation { get; private set; }

        protected MovingGameObject(GameField field, CellLocation location, Facing facing) : base(location.ToPoint())
        {
            _field = field;
            Facing = facing;
            CellLocation = TargetCellLocation = location;
        }

        public bool IsMoving => TargetCellLocation != CellLocation;

        public bool SetTarget(CellLocation loc)
        {
            if (IsMoving)
                //We are the bear rolling from the hill
                throw new InvalidOperationException("Unable to change direction while moving");
            if(loc  == CellLocation)
                return true;
            Facing = GetDirection(CellLocation, loc);
            if (loc.X < 0 || loc.Y < 0)
                return false;
            if (loc.X >= _field.Width || loc.Y >= _field.Height)
                return false;
            if (!_field.Tiles[loc.X, loc.Y].IsPassable)
                return false;

            if (
                _field.GameObjects.OfType<MovingGameObject>()
                    .Any(t => t != this && (t.CellLocation == loc || t.TargetCellLocation == loc)))
                return false;

            TargetCellLocation = loc;
            return true;
        }

        public CellLocation GetTileAtDirection(Facing facing)
        {
            if (facing == Facing.North)
                return CellLocation.WithY(CellLocation.Y - 1);
            if (facing == Facing.South)
                return CellLocation.WithY(CellLocation.Y + 1);
            if (facing == Facing.West)

                return CellLocation.WithX(CellLocation.X - 1);
            return CellLocation.WithX(CellLocation.X + 1);
        }

        public bool SetTarget(Facing facing) => SetTarget(GetTileAtDirection(facing));

        Facing GetDirection(CellLocation current, CellLocation target)
        {
            if (target.X < current.X)
                return Facing.West;
            if (target.X > current.X)
                return Facing.East;
            if (target.Y < current.Y)
                return Facing.North;
            return Facing.South;
        }

        public void SetLocation(CellLocation loc)
        {
            CellLocation = loc;
            Location = loc.ToPoint();
        }

        protected virtual double SpeedFactor => (double)1/15;

        public void MoveToTarget()
        {
            if (TargetCellLocation == CellLocation)
                return;
            var speed = GameField.CellSize*
                        (_field.Tiles[CellLocation.X, CellLocation.Y].Speed +
                         _field.Tiles[TargetCellLocation.X, TargetCellLocation.Y].Speed)/2
                        *SpeedFactor;
            var pos = Location;
            var direction = GetDirection(CellLocation, TargetCellLocation);
            if (direction == Facing.North)
            {
                pos.Y -= speed;
                Location = pos;
                if (pos.Y/GameField.CellSize <= TargetCellLocation.Y)
                    SetLocation(TargetCellLocation);
            }
            else if (direction == Facing.South)
            {
                pos.Y += speed;
                Location = pos;
                if (pos.Y / GameField.CellSize >= TargetCellLocation.Y)
                    SetLocation(TargetCellLocation);
            }
            else if (direction == Facing.West)
            {
                pos.X -= speed;
                Location = pos;
                if (pos.X / GameField.CellSize <= TargetCellLocation.X)
                    SetLocation(TargetCellLocation);
            }
            else if (direction == Facing.East)
            {
                pos.X += speed;
                Location = pos;
                if (pos.X / GameField.CellSize >= TargetCellLocation.X)
                    SetLocation(TargetCellLocation);
            }
            
        }
    }
}