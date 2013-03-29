using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace GadigGame2013
{
    public class Player
    {
        private bool hasLost;
        private bool isTurnDone;
        private Unit.Unit[] myUnits = new Unit.Unit[10];
        private int colour;
        private int currentUnit;
        public Player(int side)
        {
            currentUnit = 0;
            colour = side;
            if (colour == 1)
            {
                myUnits[0] = new Unit.Mel.Fighter(0, 0);
                myUnits[1] = new Unit.Mel.Fighter(0, 1);
                myUnits[2] = new Unit.Mel.Fighter(0, 2);
                myUnits[3] = new Unit.Mel.Fighter(0, 3);
                myUnits[4] = new Unit.Mel.Tank(0, 4);
                myUnits[5] = new Unit.Mel.Tank(1, 0);
                myUnits[6] = new Unit.Mel.Tank(1, 1);
                myUnits[7] = new Unit.Mel.Berserker(1, 2);
                myUnits[8] = new Unit.Mel.Berserker(1, 3);
                myUnits[9] = new Unit.Mel.Berserker(1, 4);
            }
            else if (colour == 2)
            {
                myUnits[0] = new Unit.Mel.Fighter(15, 8);
                myUnits[1] = new Unit.Mel.Fighter(15, 9);
                myUnits[2] = new Unit.Mel.Fighter(15, 10);
                myUnits[3] = new Unit.Mel.Fighter(15, 11);
                myUnits[4] = new Unit.Mel.Tank(15, 12);
                myUnits[5] = new Unit.Mel.Tank(14, 8);
                myUnits[6] = new Unit.Mel.Tank(14, 9);
                myUnits[7] = new Unit.Mel.Berserker(14, 10);
                myUnits[8] = new Unit.Mel.Berserker(14, 11);
                myUnits[9] = new Unit.Mel.Berserker(14, 12);
            }
        }
        public Unit.Unit[] getUnits()
        {
            return myUnits;
        }
        public int getColour()
        {
            return colour;
        }
        public int nextUnit()
        {
            currentUnit++;
            if (currentUnit == 10)
                currentUnit = 0;
            if (myUnits[currentUnit].hasUnitFinished())
            {
                int tempValue = currentUnit;
                bool checker = false;
                while (myUnits[currentUnit].hasUnitFinished() && !this.isTurnDone && !checker)
                {
                    currentUnit++;
                    if (currentUnit == 10)
                        currentUnit = 0;
                    if (!myUnits[currentUnit].hasUnitFinished() && myUnits[currentUnit].isAlive())
                    {
                        checker = true;
                    }
                    if (currentUnit == tempValue)
                        this.isTurnDone = true;
                }
            }
            
            return currentUnit;
        }
        public void moveUnit(int x)
        {
            myUnits[currentUnit].move(x);
        }
        public void newTurn()
        {
            for (int i = 0; i < 10; i++)
            {
                myUnits[i].newTurn();
            }
            currentUnit = 0;
            this.isTurnDone = false;

        }
        public void updatePlayer()
        {
            hasLost = true;
            for(int i=0;i<myUnits.Length;i++)
            {
                myUnits[i].updateUnit();
                if(myUnits[i].isAlive())
                    hasLost = false;
            }
        }
        public Unit.Unit getUnit(int x)
        {
            return myUnits[x];
        }
        public bool hasPlayerLost()
        {
            return this.hasLost;
        }
    }
}
