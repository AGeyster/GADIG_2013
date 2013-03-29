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

namespace GadigGame2013.Unit.Ran
{
    public class Archer //: Unit
    {
        private int movePoints;
        private int TileWidth = 48;
        private int TileHeight = 48;
        private Vector2 Location;
        private int Health;
        private int Range;
        public Archer(int xCoor, int yCoor)
        {
            Location = new Vector2(xCoor, yCoor);
            this.Health = 20;
            this.Range = 2;
            this.movePoints = 3;
            //Texture = Content.Load<Texture2D>(@"SideMenuHolder");
        }
        public int getX()
        {
            return (int)Location.X;
        }
        public int getY()
        {
            return (int)Location.Y;
        }
        public void setX(int x)
        {
            Location.X = x;
        }
        public void setY(int y)
        {
            Location.Y = y;
        }
        public int getHealth()
        {
            return Health;
        }
        public int getMovePoints()
        {
            return movePoints;
        }
        public int getRange()
        {
            return Range;
        }
        public int getAbilities()
        {
            return 1;
        }
        public void setHealth(int x)
        {
            this.Health = x;
        }
        public void setMovePointers(int y)
        {
            this.movePoints = y;
        }
        public void setRange(int range)
        {
            this.Range = range;
        }
        public void setAbilities()
        {
            this.Range++;
            this.Range--;
        }
        public void attack()
        {

        }
        //public void defend();
        public void move(int x)
        {
            if (x == 1)
            {
                Location.X = Location.X + 1;
            }
            else if (x == 2)
            {
                Location.Y = Location.Y - 1;
            }
            else if (x == 3)
            {
                Location.X = Location.X - 1;
            }
            else if (x == 4)
            {
                Location.Y = Location.Y + 1;
            }
        }
    }
}
