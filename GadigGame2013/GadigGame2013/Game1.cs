using System;
using System.Collections.Generic;
//using System.Console;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public SpriteFont hudFont;
        GraphicsDeviceManager graphics;
        int screenWidth = 968;
        int screenHeight = 624;
        Texture2D UnitPlaceholder;
        public Player playerOne;
        public Player playerTwo;
        int currentPlayer;
        public Unit.Unit[] unitMap = new Unit.Unit[20];
        int currentUnit;
        public readonly Rectangle ScreenRectangle;
        SpriteBatch spriteBatch;
        Engine.TileMap myMap = new Engine.TileMap();
        int squaresAcross = 16;
        int squaresDown = 13;
        KeyboardState ks;
        KeyboardState lastState;
        Texture2D minimapEmpty;
        Texture2D minimapOne;
        Texture2D minimapTwo;
        Texture2D minimapGrid;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = screenWidth;
            graphics.PreferredBackBufferHeight = screenHeight;
            this.IsMouseVisible = true;

            ScreenRectangle = new Rectangle(
                0,
                0,
                screenWidth,
                screenHeight);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            playerOne = new Player(1);
            playerTwo = new Player(2);
            for (int i = 0; i < 20; i++)
            {
                if (i >= 10)
                    unitMap[i] = playerTwo.getUnit(i - 10);
                else
                    unitMap[i] = playerOne.getUnit(i);
            }
            currentUnit = 0;
            currentPlayer = 0;
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //Minimap border is 20 wide, 35 tall
            // Create a new SpriteBatch, which can be used to draw textures.
            hudFont = Content.Load<SpriteFont>("Hud");
            Engine.Tile.TileSetTexture = Content.Load<Texture2D>(@"part2_tileset");
            UnitPlaceholder = Content.Load<Texture2D>(@"unit");
            Engine.Minimap.Texture = Content.Load<Texture2D>(@"MiniMapPlaceholder");
            Engine.Minimap.Location.X = (screenWidth - 200); Engine.Minimap.Location.Y = 0;
            this.minimapEmpty = Content.Load<Texture2D>(@"MinimapEmpty");
            this.minimapOne = Content.Load<Texture2D>(@"MinimapPlayerOne");
            this.minimapTwo = Content.Load<Texture2D>(@"MinimapPlayerTwo");
            this.minimapGrid = Content.Load<Texture2D>(@"MinimapGrid");
            InGameMenu.SideMenu.Texture = Content.Load<Texture2D>(@"SideMenuHolder"); InGameMenu.SideMenu.Location.X = (screenWidth - 200); InGameMenu.SideMenu.Location.Y = 200;
            InGameMenu.MainMenu.Texture = Content.Load<Texture2D>(@"MainMenu"); InGameMenu.MainMenu.Location.X = (screenWidth - 175); InGameMenu.MainMenu.Location.Y = screenHeight - 60;
            InGameMenu.LoadGame.Texture = Content.Load<Texture2D>(@"LoadGame"); InGameMenu.LoadGame.Location.X = (screenWidth - 175); InGameMenu.LoadGame.Location.Y = screenHeight - 120;
            InGameMenu.SaveGame.Texture = Content.Load<Texture2D>(@"SaveGame"); InGameMenu.SaveGame.Location.X = (screenWidth - 175); InGameMenu.SaveGame.Location.Y = screenHeight - 180;
            InGameMenu.EndTurnButton.Texture = Content.Load<Texture2D>(@"EndTurn"); InGameMenu.EndTurnButton.Location.X = (screenWidth - 175); InGameMenu.EndTurnButton.Location.Y = screenHeight - 240;

            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            lastState = ks;
            ks = Keyboard.GetState();
            /*if (ks.IsKeyDown(Keys.Left))
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X - 2, 0, (myMap.MapWidth - squaresAcross) * Engine.Tile.TileWidth);
            if (ks.IsKeyDown(Keys.Right))
                Camera.Location.X = MathHelper.Clamp(Camera.Location.X + 2, 0, (myMap.MapWidth - squaresAcross) * Engine.Tile.TileWidth);
            if (ks.IsKeyDown(Keys.Up))
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y - 2, 0, (myMap.MapHeight - squaresDown) * Engine.Tile.TileHeight);
            if (ks.IsKeyDown(Keys.Down))
                Camera.Location.Y = MathHelper.Clamp(Camera.Location.Y + 2, 0, (myMap.MapHeight - squaresDown) * Engine.Tile.TileHeight);
             */
            if (lastState.IsKeyDown(Keys.E) && ks.IsKeyUp(Keys.E))
                changeTurn();
            if (lastState.IsKeyDown(Keys.T) && ks.IsKeyUp(Keys.T))
            //this.currentPlayer.getUnit(currentUnit).attack();
            {
                //1 = right
                //2 = up
                //3 = left
                //4 = down
                int damage = 0;
                int direction = 0;
                if (currentPlayer == 0)
                {
                    damage = playerOne.getUnit(currentUnit).attack();
                    direction = playerOne.getUnit(currentUnit).getDirection();
                    bool checker = false;
                    if (direction == 1 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerOne.getUnit(currentUnit).getX() + 1) == playerTwo.getUnit(i).getX()) && (playerOne.getUnit(currentUnit).getY() == playerTwo.getUnit(i).getY()))
                            {
                                checker = true;
                                playerTwo.getUnit(i).setHealth(playerTwo.getUnit(i).getHealth() - damage);
                            }
                        }
                    }
                    else if (direction == 2 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerOne.getUnit(currentUnit).getX()) == playerTwo.getUnit(i).getX()) && ((playerOne.getUnit(currentUnit).getY()+1) == playerTwo.getUnit(i).getY()))
                            {
                                playerTwo.getUnit(i).setHealth(playerTwo.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                    else if (direction == 3 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerOne.getUnit(currentUnit).getX()-1) == playerTwo.getUnit(i).getX()) && ((playerOne.getUnit(currentUnit).getY()) == playerTwo.getUnit(i).getY()))
                            {
                                playerTwo.getUnit(i).setHealth(playerTwo.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                    else if (direction == 4 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerOne.getUnit(currentUnit).getX()) == playerTwo.getUnit(i).getX()) && ((playerOne.getUnit(currentUnit).getY() - 1) == playerTwo.getUnit(i).getY()))
                            {
                                playerTwo.getUnit(i).setHealth(playerTwo.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                }
                else if (currentPlayer == 1)
                {
                    damage = playerTwo.getUnit(currentUnit).attack();
                    direction = playerTwo.getUnit(currentUnit).getDirection();
                    bool checker = false;
                    if (direction == 1 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerTwo.getUnit(currentUnit).getX() + 1) == playerOne.getUnit(i).getX()) && (playerTwo.getUnit(currentUnit).getY() == playerOne.getUnit(i).getY()))
                            {
                                checker = true;
                                playerOne.getUnit(i).setHealth(playerOne.getUnit(i).getHealth() - damage);
                            }
                        }
                    }
                    else if (direction == 2 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerTwo.getUnit(currentUnit).getX()) == playerOne.getUnit(i).getX()) && ((playerTwo.getUnit(currentUnit).getY() + 1) == playerOne.getUnit(i).getY()))
                            {
                                playerOne.getUnit(i).setHealth(playerOne.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                    else if (direction == 3 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerTwo.getUnit(currentUnit).getX() - 1) == playerOne.getUnit(i).getX()) && ((playerTwo.getUnit(currentUnit).getY()) == playerOne.getUnit(i).getY()))
                            {
                                playerOne.getUnit(i).setHealth(playerOne.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                    else if (direction == 4 && !checker)
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            if (((playerTwo.getUnit(currentUnit).getX()) == playerOne.getUnit(i).getX()) && ((playerTwo.getUnit(currentUnit).getY() - 1) == playerOne.getUnit(i).getY()))
                            {
                                playerOne.getUnit(i).setHealth(playerOne.getUnit(i).getHealth() - damage);
                                checker = true;
                            }
                        }
                    }
                }
            }
            if (lastState.IsKeyDown(Keys.Tab) && ks.IsKeyUp(Keys.Tab))
            {
                if (currentPlayer == 0)
                {
                    currentUnit = playerOne.nextUnit();
                    //currentPlayer = 1;
                }
                else if (currentPlayer == 1)
                {
                    currentUnit = playerTwo.nextUnit();
                    //currentPlayer = 0;
                }
            }

            if (lastState.IsKeyDown(Keys.W) && ks.IsKeyUp(Keys.W))
            {
                if (currentPlayer == 0)
                {
                    playerOne.moveUnit(2);
                }
                else if (currentPlayer == 1)
                {
                    playerTwo.moveUnit(2);
                }
            }
            if (lastState.IsKeyDown(Keys.S) && ks.IsKeyUp(Keys.S))
            {
                if (currentPlayer == 0)
                    playerOne.moveUnit(4);
                else if (currentPlayer == 1)
                    playerTwo.moveUnit(4);
            }
            if (lastState.IsKeyDown(Keys.A) && ks.IsKeyUp(Keys.A))
            {
                if (currentPlayer == 0)
                    playerOne.moveUnit(3);
                else if (currentPlayer == 1)
                    playerTwo.moveUnit(3);
            }
            if (lastState.IsKeyDown(Keys.D) && ks.IsKeyUp(Keys.D))
            {
                if (currentPlayer == 0)
                    playerOne.moveUnit(1);
                else if (currentPlayer == 1)
                    playerTwo.moveUnit(1);
            }
            for (int i = 0; i < 20; i++)
            {
                if (i >= 10)
                    unitMap[i] = playerTwo.getUnit(i - 10);
                else
                    unitMap[i] = playerOne.getUnit(i);
            }
            Console.WriteLine(" ");
            if (playerOne.hasPlayerLost())
            {
                Console.WriteLine("Player One has Lost");
                Exit();
            }
            else if (playerTwo.hasPlayerLost())
            {
                Console.WriteLine("Player Two has Lost");
                Exit();
            }
            playerOne.updatePlayer();
            playerTwo.updatePlayer();
            if (currentPlayer == 0 && playerOne.getUnit(currentUnit).hasUnitFinished())
                currentUnit = playerOne.nextUnit();
            else if (currentPlayer == 1 && playerTwo.getUnit(currentUnit).hasUnitFinished())
                currentUnit = playerTwo.nextUnit();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            Vector2 firstSquare = new Vector2(Camera.Location.X / Engine.Tile.TileWidth, Camera.Location.Y / Engine.Tile.TileHeight);
            int firstX = (int)firstSquare.X;
            int firstY = (int)firstSquare.Y;

            Vector2 squareOffset = new Vector2(Camera.Location.X % Engine.Tile.TileWidth, Camera.Location.Y % Engine.Tile.TileHeight);
            int offsetX = (int)squareOffset.X;
            int offsetY = (int)squareOffset.Y;

            for (int y = 0; y < squaresDown;y++ )
            {
                for (int x = 0; x < squaresAcross; x++)
                {
                    foreach (int tileID in myMap.Rows[y + firstY].Columns[x + firstX].BaseTiles)
                    {
                        spriteBatch.Draw(Engine.Tile.TileSetTexture, new Rectangle((x * Engine.Tile.TileWidth) - offsetX, (y * Engine.Tile.TileHeight) - offsetY, Engine.Tile.TileWidth, Engine.Tile.TileHeight), Engine.Tile.GetSourceRectangle(tileID), Color.White);
                    }
                }
            }
            for (int counter = 0; counter < unitMap.Length;counter++ )
            {
                if (unitMap[counter].isAlive())
                {
                    //spriteBatch.Draw(UnitPlaceholder, new Rectangle((unitMap[counter].getX() * Engine.Tile.TileWidth) - offsetX, (unitMap[counter].getY() * Engine.Tile.TileHeight) - offsetY, Engine.Tile.TileWidth, Engine.Tile.TileHeight), Color.White);
                    //Console.Write("Drawing unit, maybe");
                    spriteBatch.Draw(UnitPlaceholder, new Rectangle(unitMap[counter].getX() * 48, unitMap[counter].getY() * 48, 48, 48), Color.White);
                }
            }
            //spriteBatch.Draw(Unit.Unit.Texture, Unit.Unit.Location, Color.White);
            spriteBatch.Draw(Engine.Minimap.Texture, Engine.Minimap.Location, Color.White);
            spriteBatch.Draw(InGameMenu.SideMenu.Texture, InGameMenu.SideMenu.Location, Color.White);
            spriteBatch.Draw(InGameMenu.MainMenu.Texture, InGameMenu.MainMenu.Location, Color.White);
            spriteBatch.Draw(InGameMenu.LoadGame.Texture, InGameMenu.LoadGame.Location, Color.White);
            spriteBatch.Draw(InGameMenu.SaveGame.Texture, InGameMenu.SaveGame.Location, Color.White);
            spriteBatch.Draw(InGameMenu.EndTurnButton.Texture, InGameMenu.EndTurnButton.Location, Color.White);
            
            //Draw minimap
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 13; j++)
                {
                    spriteBatch.Draw(this.minimapEmpty, new Rectangle((screenWidth - 180 + (i * 10)), (35 + j * 10), 10, 10), Color.White);
                }
            }
            for (int i = 0; i < 20; i++)
            {
                if (i < 10 && unitMap[i].isAlive())
                {
                    spriteBatch.Draw(this.minimapOne, new Rectangle((screenWidth - 180 + unitMap[i].getX() * 10),(35 + unitMap[i].getY() * 10),10,10), Color.White);
                }
                else if (i >= 10 && unitMap[i].isAlive())
                {
                    spriteBatch.Draw(this.minimapTwo, new Rectangle((screenWidth - 180 + unitMap[i].getX() * 10), (35 + unitMap[i].getY() * 10), 10, 10), Color.White);
                }
            }
            spriteBatch.Draw(this.minimapGrid, new Rectangle((screenWidth - 180),(35),160,130),Color.White);
            DrawHud();
            spriteBatch.End();

                // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        public void changeTurn()
        {
            if (currentPlayer == 0)
            {
                playerTwo.newTurn();
                currentPlayer = 1;
                currentUnit = 0;
            }
            else if (currentPlayer == 1)
            {
                playerOne.newTurn();
                currentPlayer = 0;
                currentUnit = 0;
            }
        }
        private void DrawHud()
        {
            //1 = Fighter
            //2 = Zerker
            //3 = Tank
            string tempString = "Current Player: "+(this.currentPlayer+1)+" ";
            DrawShadowedString(hudFont, tempString, new Vector2((screenWidth - 200), 220),Color.Yellow);
            if (currentPlayer == 0)
            {
                if (playerOne.getUnit(currentUnit).getUnitType() == 1)
                    tempString = "Unit: Fighter";
                else if (playerOne.getUnit(currentUnit).getUnitType() == 2)
                    tempString = "Unit: Berzerker";
                else if ((playerOne.getUnit(currentUnit).getUnitType() == 3))
                    tempString = "Unit: Tank";
            }
            else if (currentPlayer == 1)
            {
                if (playerTwo.getUnit(currentUnit).getUnitType() == 1)
                    tempString = "Unit: Fighter";
                else if (playerTwo.getUnit(currentUnit).getUnitType() == 2)
                    tempString = "Unit: Berzerker";
                else if ((playerTwo.getUnit(currentUnit).getUnitType() == 3))
                    tempString = "Unit: Tank";
            }
            DrawShadowedString(hudFont, tempString, new Vector2((screenWidth - 200), 240), Color.Yellow);
            if (currentPlayer == 0)
            {
                tempString = "Move Points: "+playerOne.getUnit(currentUnit).getMovePoints()+"/"+playerOne.getUnit(currentUnit).getMaxMovePoints()+" ";
            }
            else if (currentPlayer == 1)
            {
                tempString = "Move Points: " + playerTwo.getUnit(currentUnit).getMovePoints() + "/" + playerTwo.getUnit(currentUnit).getMaxMovePoints() + " ";
            }
            DrawShadowedString(hudFont, tempString, new Vector2((screenWidth - 200), 260), Color.Yellow);
            if (currentPlayer == 0)
            {
                tempString = "Health: " + playerOne.getUnit(currentUnit).getHealth() + " ";
            }
            else if (currentPlayer == 1)
            {
                tempString = "Health: " + playerTwo.getUnit(currentUnit).getHealth() + " ";
            }
            DrawShadowedString(hudFont, tempString, new Vector2((screenWidth - 200), 280), Color.Yellow);
            if (currentPlayer == 0)
            {
                tempString = "Position: (" + playerOne.getUnit(currentUnit).getX() + ", " + playerOne.getUnit(currentUnit).getY() + ")";
            }
            else if (currentPlayer == 1)
            {
                tempString = "Position: (" + playerTwo.getUnit(currentUnit).getX() + ", " + playerTwo.getUnit(currentUnit).getY() + ")";
            }
            DrawShadowedString(hudFont, tempString, new Vector2((screenWidth - 200), 300), Color.Yellow);
        }
        private void DrawShadowedString(SpriteFont font, string value, Vector2 position, Color color)
        {
            //spriteBatch.Begin();
            spriteBatch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            spriteBatch.DrawString(font, value, position, color);
            //spriteBatch.End();
        }
    }
}
