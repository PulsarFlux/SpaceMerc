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
using SpaceMerc.RandomObjects;


namespace SpaceMerc
{
    class Location
    {
        class PathingNode
        {
            public bool Walkable;
            public bool Open;
            public bool Closed;
            public int[] Parent = new int[2];
            public int KnownDistance;
            public int GuessedDistance;

        }
        public int[,] FindPath(PathingNode[,] Grid, int[] StartPos, int[] EndPos)
        {
            // 'coordinate' points start from 1 
            int[] CurrentPos;
            CurrentPos = StartPos;
            Grid[StartPos[0], StartPos[1]].Open = true;
            bool Blocked = false;
            while (Grid[EndPos[0], EndPos[1]].Closed == false & Blocked == false)
            {
                for (int i = -1; i < 2; i++)
                {
                    for (int j = -1; j < 2; j++)
                    {
                        // make sure not on central node
                        if (!(i == 0 & j == 0))
                        {
                            // we want grid to have additional unwalkable squares forming the edge

                            
                            if (Grid[CurrentPos[0] + i, CurrentPos[1] + j].Walkable == true)
                            {
                                //check if diagonal
                                if (i != 0 & j != 0)
                                {
                                    if (Grid[CurrentPos[0] + i, CurrentPos[1] + j].Open == false)
                                    {
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].Open = true;
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance = Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 14;
                                    }
                                    else if (Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 14 < Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance)
                                    {
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].Parent = CurrentPos;
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance = Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 14;
                                    }

                                }
                                else
                                {
                                    if (Grid[CurrentPos[0] + i, CurrentPos[1] + j].Open == false)
                                    {
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].Open = true;
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance = Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 10;
                                    }
                                    else if (Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 14 < Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance)
                                    {
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].Parent = CurrentPos;
                                        Grid[CurrentPos[0] + i, CurrentPos[1] + j].KnownDistance = Grid[CurrentPos[0], CurrentPos[1]].KnownDistance + 10;
                                    }
                                }

                            }
                        }
                    }
                }
                Grid[CurrentPos[0], CurrentPos[1]].Closed = true;
                int MinPathGuess = Grid.GetLength(0) * Grid.GetLength(1);
                int[] MinPathGuessPoint = {0, 0};
                Blocked = true;
                for (int k = 0; k < Grid.GetLength(0); k++)
                {
                    for (int l = 0; l < Grid.GetLength(1); l++)
                    {
                        if (Grid[k + 1, l + 1].Open == true & Grid[k + 1, l + 1].Closed == false)
                        {
                            Blocked = false;
                            if (Grid[k + 1, l + 1].KnownDistance + Grid[k + 1, l + 1].GuessedDistance < MinPathGuess)
                            {
                                MinPathGuess = Grid[k + 1, l + 1].KnownDistance + Grid[k + 1, l + 1].GuessedDistance;
                                MinPathGuessPoint[0] = k + 1;
                                MinPathGuessPoint[1] = l + 1;
                            }
                        }
                    }
                }
                CurrentPos = MinPathGuessPoint;
            }
        // insert path returning code here
        }
        public Texture2D TileMap;
        public List<Object> Objects;
        public int Width;
        public Rectangle SourceRect;
        public int[,] TilePlan;
        public int Height;
        public int TilesHigh;
        public int TilesWide;
        int XZones;
        int YZones;
        public List<Enemy>[,] Zones;
        public List<Object>[,] ObjectZones;
        public List<Enemy> Enemies = new List<Enemy>();
        public void Create(string[] param, ContentHolder TCH, List<string> TilePlanRows)
        {
            // param of 0 is used for sorting earlier
            Width = Convert.ToInt32(param[1]);
            Height = Convert.ToInt32(param[2]);
            TilesWide = Convert.ToInt32(param[3]);
            TilesHigh = Convert.ToInt32(param[4]);
            TilePlan = new int[TilesWide, TilesHigh];
            TileMap = TCH.GetTexture(param[5]);
            int h = 0;
            foreach (string Row in TilePlanRows)
            {  
                string[] temp = FileServices.LineCommaSplit(Row);
                int k = 0;
                foreach (string Tile in temp)
                {
                    TilePlan[k, h] = Convert.ToInt32(Tile);
                    k += 1;
                }
                h += 1;
            }
            
            
            Objects = new List<Object>();
            SourceRect = new Rectangle();
            SourceRect.Width = Width / TilesWide;
            SourceRect.Height = Height / TilesHigh;
            if (Width % 100 == 0)
            {
                XZones = Width / 100;
            }
            else
            {
                XZones = (Width / 100) + 1;
            }
            if (Height % 100 == 0)
            {
                YZones = Height / 100;
            }
            else
            {
                YZones = (Height / 100) + 1;
            }
            Zones = new List<Enemy>[XZones, YZones];
            ObjectZones = new List<Object>[XZones, YZones];

            for (int i = 0; i < XZones; i++)
            {
                for (int j = 0; j < YZones; j++)
                {
                    Zones[i, j] = new List<Enemy>();
                    ObjectZones[i, j] = new List<Object>();
                }
            }
                  
        }
        public void AddObject(Texture2D Tex, int x, int y)
        {
            Object TempObject = new Object();
            TempObject.Create(Tex, x, y);
            Objects.Add(TempObject);
        }
        public void AddObject(string[] param, ContentHolder TCH)
        {
            Object TempObject = new Object();
            TempObject.Create(param, TCH);
            Objects.Add(TempObject);
            for (int i = (int)(TempObject.Position.X) / 100; i <= (int)(TempObject.Position.X + TempObject.Picture.Width) / 100; i++)
            {
                for (int j = (int)(TempObject.Position.Y) / 100; j <= (int)(TempObject.Position.Y + TempObject.Picture.Height) / 100; j++)
                {
                    if (!ObjectZones[i, j].Contains(TempObject))
                    {
                        ObjectZones[i, j].Add(TempObject);
                    }
                }
            }       
        }
        public void AddEnemy(GraphicsDeviceManager GDM, string[] param, Camera ACamera, ContentHolder TCH)
        {
            Enemy TempEnemy = new Enemy();
            TempEnemy.Create(GDM, param, ACamera, TCH);
            Enemies.Add(TempEnemy);
        }
        public void DrawTiles(Camera C, SpriteBatch SB)
        {
            Rectangle TestRect;
            TestRect.Width = SourceRect.Width;
            TestRect.Height = SourceRect.Width;
            C.ScreenView.X = (int)C.MapPosition.X - (C.ScreenView.Width / 2);
            C.ScreenView.Y = (int)C.MapPosition.Y - (C.ScreenView.Height / 2);
            for (int i = 0; i < TilePlan.GetLength(0); i++)
            {
                for (int k = 0; k < TilePlan.GetLength(1); k++)
                {
                    TestRect.X = i * SourceRect.Width;
                    TestRect.Y = k * SourceRect.Width;
                    if (TestRect.Intersects(C.ScreenView))
                    {
                        SourceRect.X = (TilePlan[i, k] % TilesWide) * SourceRect.Width;
                        SourceRect.Y = (TilePlan[i, k] / TilesWide) * SourceRect.Width;
                        TestRect.X = i * SourceRect.Width - (int)C.MapPosition.X + (C.ScreenView.Width / 2);
                        TestRect.Y = k * SourceRect.Width - (int)C.MapPosition.Y + (C.ScreenView.Height / 2);
                        SB.Draw(TileMap, TestRect, SourceRect, Color.White);
                    }
                }
            }
        }
        public void Draw(SpriteBatch spritebatch, Camera Cam, GameTime GT)
        {
            DrawTiles(Cam, spritebatch);
            foreach (Object Ob in Objects)
            {
                Ob.Draw(spritebatch, Cam, GT);

            }
            foreach (Enemy E in Enemies)
            {
                E.Draw(spritebatch);
            }
        }
        public void Update(Camera Cam)
        {
            for (int i = 0; i < XZones; i++)
            {
                for (int j = 0; j < YZones; j++)
                {
                    Zones[i, j].Clear();
                }
            }
            foreach (Enemy E in Enemies)
            {
                E.UpdatePos(Cam);
                E.UpdateBox();
                Zones[(int)(E.MapPosition.X - E.origin.X) / 100, (int)(E.MapPosition.Y - E.origin.Y) / 100].Add(E);
                if (!Zones[(int)(E.MapPosition.X - E.origin.X) / 100, (int)(E.MapPosition.Y + E.origin.Y) / 100].Contains(E))
                {
                    Zones[(int)(E.MapPosition.X - E.origin.X) / 100, (int)(E.MapPosition.Y + E.origin.Y) / 100].Add(E);
                }
                if (!Zones[(int)(E.MapPosition.X + E.origin.X) / 100, (int)(E.MapPosition.Y + E.origin.Y) / 100].Contains(E))
                {
                    Zones[(int)(E.MapPosition.X + E.origin.X) / 100, (int)(E.MapPosition.Y + E.origin.Y) / 100].Add(E);
                }
                if (!Zones[(int)(E.MapPosition.X + E.origin.X) / 100, (int)(E.MapPosition.Y - E.origin.Y) / 100].Contains(E))
                {
                    Zones[(int)(E.MapPosition.X + E.origin.X) / 100, (int)(E.MapPosition.Y - E.origin.Y) / 100].Add(E);
                }
            }
        

        }
    }
}
