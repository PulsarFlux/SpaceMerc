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

namespace SpaceMerc
{
    class ProjectileManager
    {
        public List<Projectile> Projectiles;
        public ProjectileManager()
        {
            Projectiles = new List<Projectile>();
        }
        public void Draw(SpriteBatch spriteBatch, Camera C)
        {
            Vector2 TempPos;
          foreach (Projectile P in Projectiles)
          {
              TempPos = P.Position - C.MapPosition;
              TempPos.X += (C.ScreenView.Width/2);
              TempPos.Y += (C.ScreenView.Height/2);
              spriteBatch.Draw(P.Type.Picture, TempPos,null, Color.White,P.Angle,P.Origin,1.0f,SpriteEffects.None,0);
          }
        }
        public void Update(int MapWidth, int MapHeight, List<Enemy>[,] Zones, List<Enemy> Enemies, List<Object>[,] ObjectZones)
        {
            for (int i = 0; i < Projectiles.Count(); i++)
            {
                if (Projectiles[i].Position.X + Projectiles[i].Type.BoundingBox.Width < 0)
                {
                    Projectiles.RemoveAt(i);
                    i -= 1;
                }
                else if (Projectiles[i].Position.Y + Projectiles[i].Type.BoundingBox.Height < 0)
                {
                    Projectiles.RemoveAt(i);
                    i -= 1;
                }
                else if (Projectiles[i].Position.Y >= MapHeight)
                {
                    Projectiles.RemoveAt(i);
                    i -= 1;
                }
                else if (Projectiles[i].Position.X >= MapWidth)
                {
                    Projectiles.RemoveAt(i);
                    i -= 1;
                }
            }
            Rectangle TempR =  new Rectangle();
            List<Projectile> TempP = new List<Projectile>();
            List<Enemy> TempE = new List<Enemy>();
            foreach (Projectile P in Projectiles)
            {
                TempR = P.Type.BoundingBox;
                TempR.X = (int)P.Position.X;
                TempR.Y = (int)P.Position.Y;
                foreach (Enemy E in Zones[(int)P.Position.X / 100, (int)P.Position.Y / 100])
                {
                    if (TempR.Intersects(E.Box))
                    {
                        TempP.Add(P);
                        if (E.Hit(P))
                        {
                            TempE.Add(E);
                        }
                    }
                }
                foreach (Object O in ObjectZones[(int)P.Position.X / 100, (int)P.Position.Y / 100])
                {
                    foreach (Rectangle R in O.BoundingBoxes)
                    {
                        if (TempR.Intersects(R))
                        {
                            TempP.Add(P);
                        }
                    }
                }
            }
            foreach (Projectile P in TempP)
            {
                Projectiles.Remove(P);
            }
            foreach (Enemy E in TempE)
            {
                Enemies.Remove(E);
            }
            foreach (Projectile P in Projectiles)
            {
                P.Position.X += (float)(P.Speed * Math.Cos((double)P.Angle));
                P.Position.Y += (float)(P.Speed * Math.Sin((double)P.Angle));
            }
        }
    }
}
