using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GadigGame2013.Unit.Mel
{
    public class Tank : Unit
    {
        private int unitType;
        private int facingDirection;
        private bool isUnitAlive;
        private int maxMovePoints;
        private bool hasFinished;
        private int movePoints;
        private int TileWidth = 48;
        private int TileHeight = 48;
        private Vector2 Location;
        private int Health;
        private int Range;
        public Tank(int xCoor, int yCoor)
        {
            this.maxMovePoints = 2;
            this.hasFinished = false;
            Location = new Vector2(xCoor, yCoor);
            this.Health = 30;
            this.Range = 1;
            this.movePoints = 2;
            this.isUnitAlive = true;
            this.unitType = 3;
            //Texture = Content.Load<Texture2D>(@"SideMenuHolder");
        }
        public override int getX()
        {
            return (int)Location.X;
        }
        public override int getY()
        {
            return (int)Location.Y;
        }
        public override void setX(int x)
        {
            Location.X = x;
        }
        public override void setY(int y)
        {
            Location.Y = y;
        }
        public override int getHealth()
        {
            return Health;
        }
        public override int getMovePoints()
        {
            return movePoints;
        }
        public override int getRange()
        {
            return Range;
        }
        public override int getAbilities()
        {
            return 1;
        }
        public override void setHealth(int x)
        {
            this.Health = x;
        }
        public override void setMovePointers(int y)
        {
            this.movePoints = y;
        }
        public override void setRange(int range)
        {
            this.Range = range;
        }
        public override void setAbilities()
        {
            this.Range++;
            this.Range--;
        }
        public override int attack()
        {
            this.hasFinished = true;
            return 2;
        }
        public override void defend()
        {
            this.movePoints++;
            this.movePoints--;
        }
        public override void move(int x)
        {
            if (x == 1 && this.movePoints >= 1 && Location.X + 1 < 16)
            {
                Location.X = Location.X + 1;
                this.facingDirection = 1;
                this.movePoints--;
            }
            else if (x == 2 && this.movePoints >= 1 && Location.Y - 1 < 13)
            {
                Location.Y = Location.Y - 1;
                this.facingDirection = 2;
                this.movePoints--;
            }
            else if (x == 3 && this.movePoints >= 1 && Location.X - 1 >= 0)
            {
                Location.X = Location.X - 1;
                this.facingDirection = 3;
                this.movePoints--;
            }
            else if (x == 4 && this.movePoints >= 1 && Location.X + 1 >= 0)
            {
                Location.Y = Location.Y + 1;
                this.facingDirection = 4;
                this.movePoints--;
            }
            if (this.movePoints == 0)
                this.hasFinished = true;
        }
        public override void newTurn()
        {
            this.movePoints = maxMovePoints;
            hasFinished = false;
        }
        public override bool hasUnitFinished()
        {
            return this.hasFinished;
        }
        public override bool isAlive()
        {
            return this.isUnitAlive;
        }
        public override void updateUnit()
        {
            if (this.Health <= 0)
                this.isUnitAlive = false;
            if (this.movePoints == 0)
                this.hasFinished = true;
        }
        public override int getUnitType()
        {
            return this.unitType;
        }
        public override int getMaxMovePoints()
        {
            return this.maxMovePoints;
        }
        public override int getDirection()
        {
            return this.facingDirection;
        }
    }
}
