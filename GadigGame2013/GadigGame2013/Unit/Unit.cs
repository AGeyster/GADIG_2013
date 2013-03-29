using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GadigGame2013.Unit
{
    abstract public class Unit
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
        abstract public int getX();
        abstract public int getY();
        abstract public void setX(int x);
        abstract public void setY(int y);
        abstract public int getHealth();
        abstract public int getMovePoints();
        abstract public int getRange();
        abstract public int getAbilities();
        abstract public void setHealth(int x);
        abstract public void setMovePointers(int y);
        abstract public void setRange(int range);
        abstract public void setAbilities();
        abstract public int attack();
        abstract public void defend();
        abstract public void move(int x);
        abstract public void newTurn();
        abstract public bool hasUnitFinished();
        abstract public bool isAlive();
        abstract public void updateUnit();
        abstract public int getUnitType();
        abstract public int getMaxMovePoints();
        abstract public int getDirection();
    }
}
