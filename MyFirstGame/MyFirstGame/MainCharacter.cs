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
    class MainCharacter : Creature
    {
        public float AimTurnSpeed = 0.06f;
        public Texture2D AimedPic;
        public Weapon CurrentWeapon;
        public string CurrentProjectileType;
        public Boolean Aiming;
        public Boolean UnAiming;
        public Boolean Aimed;
        public ProjectileType PT;
        public float TimeCounter;
        public float ShootTimeCounter;
        public void AimPressed(Cursor Curs)
        {
            if (Aimed)
            {
                Aiming = false;
                UnAiming = true;
            }
            else
            {
                Aiming = true;
                UnAiming = false;
            }
        }
        public void Aim(GameTime gametime)
        {
            if (Aiming)
            {
                TimeCounter += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (TimeCounter >= 0.3f)
                {
                    Aimed = true;
                    MovementSpeed = 3;
                    Aiming = false;
                    CurrentPic = AimedPic;
                    TimeCounter = 0;
                }
            }
            else if (UnAiming)
            {
                TimeCounter += (float)gametime.ElapsedGameTime.TotalSeconds;
                if (TimeCounter >= 0.3f)
                {
                    Aimed = false;
                    MovementSpeed = 5;
                    UnAiming = false;
                    CurrentPic = Pic;
                    TimeCounter = 0;
                }
            }

        }
        public void Create(int x, int y, float Ang, Texture2D Tex, Texture2D AimPic, Weapon FirstWep, Camera ACamera)
        {
            MapPosition.X = x;
            MapPosition.Y = y;
            ScreenPosition.X = MapPosition.X - ACamera.MapPosition.X + (ACamera.ScreenView.Width / 2);
            ScreenPosition.Y = MapPosition.Y - ACamera.MapPosition.Y + (ACamera.ScreenView.Height / 2);
            CurrentWeapon = FirstWep;
            CurrentProjectileType = "Plasma";
            Angle = Ang;
            Pic = Tex;
            MovementSpeed = 5;
            AimedPic = AimPic;
            Box = Tex.Bounds;
            CurrentPic = Pic;
            Box.Width = 33;
            origin.X = 12;
            origin.Y = 30;
            // origin.X = (float)Math.Round((double)(Tex.Width / 2), 0, MidpointRounding.AwayFromZero);
            // origin.Y = (float)Math.Round((double)(Tex.Height / 2), 0, MidpointRounding.AwayFromZero);
            Box.X = x - (int)(origin.X);
            Box.Y = y - (int)(origin.Y);
        }
        public void Move()
        {
            KeyboardState newstate;
            newstate = Keyboard.GetState();
            if (newstate.IsKeyDown(Keys.W))
            {
                LastMapPosition.Y = MapPosition.Y;
                ScreenPosition.Y -= MovementSpeed;
                MapPosition.Y -= MovementSpeed;
            }
            if (newstate.IsKeyDown(Keys.A))
            {
                LastMapPosition.X = MapPosition.X;
                ScreenPosition.X -= MovementSpeed;
                MapPosition.X -= MovementSpeed;
            }
            if (newstate.IsKeyDown(Keys.S))
            {
                LastMapPosition.Y = MapPosition.Y;
                ScreenPosition.Y += MovementSpeed;
                MapPosition.Y += MovementSpeed;

            }
            if (newstate.IsKeyDown(Keys.D))
            {
                LastMapPosition.X = MapPosition.X;
                ScreenPosition.X += MovementSpeed;
                MapPosition.X += MovementSpeed;
            }
            UpdateBox();

        }
        public void BoundsCheck(Camera Cam, int MapWidth, int MapHeight)
        {
            if (ScreenPosition.X > (Cam.ScreenView.Width - Cam.XBuffer))
            {
                if ((Cam.MapPosition.X + (Cam.ScreenView.Width / 2)) < MapWidth)
                {
                    ScreenPosition.X = Cam.ScreenView.Width - Cam.XBuffer;
                    Cam.MapPosition.X += MovementSpeed;
                    MapPosition.X = Cam.MapPosition.X - (Cam.ScreenView.Width / 2) + ScreenPosition.X;
                }
                else
                {
                    Cam.MapPosition.X = MapWidth - (Cam.ScreenView.Width / 2);
                    if (ScreenPosition.X > Cam.ScreenView.Width)
                    {
                        MapPosition.X = MapWidth;
                        ScreenPosition.X = Cam.ScreenView.Width;
                    }

                }
            }

            else if (ScreenPosition.X < Cam.XBuffer)
            {
                if ((Cam.MapPosition.X - (Cam.ScreenView.Width / 2)) > 0)
                {
                    ScreenPosition.X = Cam.XBuffer;
                    Cam.MapPosition.X -= MovementSpeed;
                    MapPosition.X = Cam.MapPosition.X - (Cam.ScreenView.Width / 2) + ScreenPosition.X;
                }
                else
                {
                    Cam.MapPosition.X = Cam.ScreenView.Width / 2;
                    if (ScreenPosition.X < 0)
                    {
                        MapPosition.X = 0;
                        ScreenPosition.X = 0;
                    }

                }
            }
            if (ScreenPosition.Y > (Cam.ScreenView.Height - Cam.YBuffer))
            {
                if ((Cam.MapPosition.Y + (Cam.ScreenView.Height / 2)) < MapHeight)
                {
                    ScreenPosition.Y = Cam.ScreenView.Height - Cam.YBuffer;
                    Cam.MapPosition.Y += MovementSpeed;
                    MapPosition.Y = Cam.MapPosition.Y - (Cam.ScreenView.Height / 2) + ScreenPosition.Y;
                }
                else
                {
                    Cam.MapPosition.Y = MapHeight - (Cam.ScreenView.Height / 2);
                    if (ScreenPosition.Y > Cam.ScreenView.Height)
                    {
                        MapPosition.Y = MapHeight;
                        ScreenPosition.Y = Cam.ScreenView.Height;
                    }

                }
            }

            else if (ScreenPosition.Y < Cam.YBuffer)
            {
                if ((Cam.MapPosition.Y - (Cam.ScreenView.Height / 2)) > 0)
                {
                    ScreenPosition.Y = Cam.YBuffer;
                    Cam.MapPosition.Y -= MovementSpeed;
                    MapPosition.Y = Cam.MapPosition.Y - (Cam.ScreenView.Height / 2) + ScreenPosition.Y;

                }
                else
                {
                    Cam.MapPosition.Y = Cam.ScreenView.Height / 2;
                    if (ScreenPosition.Y < 0)
                    {
                        MapPosition.Y = 0;
                        ScreenPosition.Y = 0;
                    }

                }
            }
            UpdateBox();
        }
        public void EnemyCollisions(Location CurrentLocation, Camera Cam)
        {
            Rectangle OldBox;
            OldBox = Box;
            OldBox.X = (int)(LastMapPosition.X - origin.X);
            OldBox.Y = (int)(LastMapPosition.Y - origin.Y);
            for (int i = 0; i <= CurrentLocation.Enemies.Count - 1; i++)
            {
                 if (CurrentLocation.Enemies[i].Box.Intersects(Box))
                    {
                        if ((OldBox.Right > CurrentLocation.Enemies[i].Box.Left & OldBox.Right < CurrentLocation.Enemies[i].Box.Right)
                            | (OldBox.Left < CurrentLocation.Enemies[i].Box.Right & OldBox.Left > CurrentLocation.Enemies[i].Box.Left)
                            | (OldBox.Right <= CurrentLocation.Enemies[i].Box.Right & OldBox.Left >= CurrentLocation.Enemies[i].Box.Left))
                        {
                            if (OldBox.Bottom <= CurrentLocation.Enemies[i].Box.Top)
                            {

                                MapPosition.Y = CurrentLocation.Enemies[i].Box.Top - Pic.Height + origin.Y;
                                ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                            }
                            else
                            {

                                MapPosition.Y = CurrentLocation.Enemies[i].Box.Bottom + origin.Y;
                                ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                            }
                        }
                        else if ((OldBox.Top <= CurrentLocation.Enemies[i].Box.Bottom & OldBox.Top >= CurrentLocation.Enemies[i].Box.Top)
                            | (OldBox.Bottom >= CurrentLocation.Enemies[i].Box.Top & OldBox.Bottom <= CurrentLocation.Enemies[i].Box.Bottom)
                            | (OldBox.Top <= CurrentLocation.Enemies[i].Box.Top & OldBox.Bottom >= CurrentLocation.Enemies[i].Box.Bottom))
                        {
                            if (OldBox.Right <= CurrentLocation.Enemies[i].Box.Left)
                            {

                                MapPosition.X = CurrentLocation.Enemies[i].Box.Left - 21;
                                ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                            }
                            else
                            {

                                MapPosition.X = CurrentLocation.Enemies[i].Box.Right + origin.X;
                                ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                            }
                        }
                        else
                        {
                            if (OldBox.X > CurrentLocation.Enemies[i].Box.Right)
                            {
                                if (OldBox.Y < CurrentLocation.Enemies[i].Box.Top)
                                {
                                    if (OldBox.Bottom < CurrentLocation.Enemies[i].Box.Top - (MapPosition.Y - OldBox.Y) * (OldBox.Left - CurrentLocation.Enemies[i].Box.Right) / (OldBox.Left - MapPosition.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Enemies[i].Box.Top - Pic.Height + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Enemies[i].Box.Right + origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                    }
                                }
                                else
                                {
                                    if (OldBox.Top > CurrentLocation.Enemies[i].Box.Bottom + (OldBox.Y - MapPosition.Y) * (OldBox.Left - CurrentLocation.Enemies[i].Box.Right) / (OldBox.Left - MapPosition.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Enemies[i].Box.Bottom + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Enemies[i].Box.Right + origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                    }
                                }
                            }
                            else
                            {
                                if (OldBox.Y < CurrentLocation.Enemies[i].Box.Top)
                                {
                                    if (OldBox.Bottom < CurrentLocation.Enemies[i].Box.Top - (MapPosition.Y - OldBox.Y) * (CurrentLocation.Enemies[i].Box.Left - OldBox.Left) / (MapPosition.X - OldBox.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Enemies[i].Box.Top - Pic.Height + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Enemies[i].Box.Left - 21;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                    }
                                }
                                else
                                {
                                    if (OldBox.Top > CurrentLocation.Enemies[i].Box.Bottom + (OldBox.Y - MapPosition.Y) * (CurrentLocation.Enemies[i].Box.Left - OldBox.Left) / (MapPosition.X - OldBox.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Enemies[i].Box.Bottom + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Enemies[i].Box.Left - 21;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                    }
                                }

                            }
                        }
                    }
                }
            }
        public void Fire(ProjectileManager PM, MouseState MS, GameTime GT)
        {
            CurrentWeapon.Fire(this, PM, MS, GT);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentPic, ScreenPosition, null, Color.White, Angle, origin, 1.0f, SpriteEffects.None, 0);
        }
    }
}
