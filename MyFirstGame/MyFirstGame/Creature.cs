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
    class Creature
    {
        public Vector2 ScreenPosition;
        public Vector2 LastMapPosition;
        public Vector2 MapPosition;
        public int MovementSpeed;
        public float Angle;
        public Texture2D Pic;
        public Texture2D CurrentPic;
        public Vector2 origin;
        public Rectangle Box;
        public void Create(int x, int y, float Ang, Texture2D Tex, Camera ACamera)
        {
            MapPosition.X = x;
            MapPosition.Y = y;
            ScreenPosition.X = MapPosition.X - ACamera.MapPosition.X + (ACamera.ScreenView.Width / 2);
            ScreenPosition.Y = MapPosition.Y - ACamera.MapPosition.Y + (ACamera.ScreenView.Height / 2);
            Angle = Ang;
            Pic = Tex;
            Box = Tex.Bounds;
            CurrentPic = Pic;
            Box.X = x - (int)(origin.X);
            Box.Y = y - (int)(origin.Y);
        }
        public void UpdateBox()
        {
            Box.X = (int)(MapPosition.X - origin.X);
            Box.Y = (int)(MapPosition.Y - origin.Y);
        }   
        public void ObjectCollisions(Location CurrentLocation, Camera Cam)
        {
            Rectangle OldBox;
            OldBox = Box;
            OldBox.X = (int)(LastMapPosition.X - origin.X);
            OldBox.Y = (int)(LastMapPosition.Y - origin.Y);
            for (int i = 0; i <= CurrentLocation.Objects.Count - 1; i++)
            {
                for (int k = 0; k <= CurrentLocation.Objects[i].BoundingBoxes.Count - 1; k++)
                {
                    if (CurrentLocation.Objects[i].BoundingBoxes[k].Intersects(Box))
                    {
                        if ((OldBox.Right > CurrentLocation.Objects[i].BoundingBoxes[k].Left & OldBox.Right < CurrentLocation.Objects[i].BoundingBoxes[k].Right)
                            | (OldBox.Left < CurrentLocation.Objects[i].BoundingBoxes[k].Right & OldBox.Left > CurrentLocation.Objects[i].BoundingBoxes[k].Left)
                            | (OldBox.Right <= CurrentLocation.Objects[i].BoundingBoxes[k].Right & OldBox.Left >= CurrentLocation.Objects[i].BoundingBoxes[k].Left))
                        {
                            if (OldBox.Bottom <= CurrentLocation.Objects[i].BoundingBoxes[k].Top)
                            {

                                MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Top - Pic.Height + origin.Y;
                                ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                UpdateBox();
                                for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                {
                                    for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                    {
                                        if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                        {
                                            MapPosition = LastMapPosition;
                                            ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                            ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        }
                                    }
                                }
                            }
                            else
                            {

                                MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Bottom + origin.Y;
                                ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                UpdateBox();
                                for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                {
                                    for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                    {
                                        if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                        {
                                            MapPosition = LastMapPosition;
                                            ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                            ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        }
                                    }
                                }
                            }
                        }
                        else if ((OldBox.Top <= CurrentLocation.Objects[i].BoundingBoxes[k].Bottom & OldBox.Top >= CurrentLocation.Objects[i].BoundingBoxes[k].Top)
                            | (OldBox.Bottom >= CurrentLocation.Objects[i].BoundingBoxes[k].Top & OldBox.Bottom <= CurrentLocation.Objects[i].BoundingBoxes[k].Bottom)
                            | (OldBox.Top <= CurrentLocation.Objects[i].BoundingBoxes[k].Top & OldBox.Bottom >= CurrentLocation.Objects[i].BoundingBoxes[k].Bottom))
                        {
                            if (OldBox.Right <= CurrentLocation.Objects[i].BoundingBoxes[k].Left)
                            {

                                MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Left - 21;
                                ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                UpdateBox();
                                for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                {
                                    for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                    {
                                        if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                        {
                                            MapPosition = LastMapPosition;
                                            ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                            ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        }
                                    }
                                }
                            }
                            else
                            {

                                MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Right + origin.X;
                                ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                UpdateBox();
                                for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                {
                                    for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                    {
                                        if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                        {
                                            MapPosition = LastMapPosition;
                                            ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                            ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (OldBox.X > CurrentLocation.Objects[i].BoundingBoxes[k].Right)
                            {
                                if (OldBox.Y < CurrentLocation.Objects[i].BoundingBoxes[k].Top)
                                {
                                    if (OldBox.Bottom < CurrentLocation.Objects[i].BoundingBoxes[k].Top - (MapPosition.Y - OldBox.Y) * (OldBox.Left - CurrentLocation.Objects[i].BoundingBoxes[k].Right) / (OldBox.Left - MapPosition.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Top - Pic.Height + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Right + origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (OldBox.Top > CurrentLocation.Objects[i].BoundingBoxes[k].Bottom + (OldBox.Y - MapPosition.Y) * (OldBox.Left - CurrentLocation.Objects[i].BoundingBoxes[k].Right) / (OldBox.Left - MapPosition.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Bottom + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Right + origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (OldBox.Y < CurrentLocation.Objects[i].BoundingBoxes[k].Top)
                                {
                                    if (OldBox.Bottom < CurrentLocation.Objects[i].BoundingBoxes[k].Top - (MapPosition.Y - OldBox.Y) * (CurrentLocation.Objects[i].BoundingBoxes[k].Left - OldBox.Left) / (MapPosition.X - OldBox.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Top - Pic.Height + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Left - origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (OldBox.Top > CurrentLocation.Objects[i].BoundingBoxes[k].Bottom + (OldBox.Y - MapPosition.Y) * (CurrentLocation.Objects[i].BoundingBoxes[k].Left - OldBox.Left) / (MapPosition.X - OldBox.X))
                                    {
                                        MapPosition.Y = CurrentLocation.Objects[i].BoundingBoxes[k].Bottom + origin.Y;
                                        ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MapPosition.X = CurrentLocation.Objects[i].BoundingBoxes[k].Left - origin.X;
                                        ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                        UpdateBox();
                                        for (int l = 0; l <= CurrentLocation.Objects.Count - 1; l++)
                                        {
                                            for (int m = 0; m <= CurrentLocation.Objects[l].BoundingBoxes.Count - 1; m++)
                                            {
                                                if (CurrentLocation.Objects[l].BoundingBoxes[m].Intersects(Box))
                                                {
                                                    MapPosition = LastMapPosition;
                                                    ScreenPosition.Y = MapPosition.Y + (Cam.ScreenView.Height / 2) - Cam.MapPosition.Y;
                                                    ScreenPosition.X = MapPosition.X + (Cam.ScreenView.Width / 2) - Cam.MapPosition.X;
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
        }
    }
}
