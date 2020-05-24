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
    class Object
    {
        public Texture2D Picture;
        public Vector2 Position;
        public List<Rectangle> BoundingBoxes;
        public AnimatedSprite Animation;
        public void Create(Texture2D Pic, int x, int y)
        {
            Picture = Pic;
            Position.X = x;
            Position.Y = y;
            BoundingBoxes = new List<Rectangle>();
        }
        public void Create(string[] param, ContentHolder TCH)
        {
            Picture = TCH.GetTexture(param[1]);
            Position.X = Convert.ToInt32(param[2]);
            Position.Y = Convert.ToInt32(param[3]);
            int FW = Convert.ToInt32(param[4]);
            int FH = Convert.ToInt32(param[5]);
            Texture2D AniSource = TCH.GetTexture(param[6]);
            float fps = (float)Convert.ToInt32(param[7]);
            int Fs = Convert.ToInt32(param[8]);
            int BBoxW = Convert.ToInt32(param[9]);
            int BBoxH = Convert.ToInt32(param[10]);
            int BBoxX = Convert.ToInt32(param[11]);
            int BBoxY = Convert.ToInt32(param[12]);
            BoundingBoxes = new List<Rectangle>();
            Animation = new AnimatedSprite();
            Animation.Create(FW, FH, AniSource, fps, Fs);
            AddBoundingBox(BBoxW, BBoxH, BBoxX, BBoxY);
        }
        private void AddBoundingBox(int Width, int Height, int x, int y)
        {
            Rectangle TempBox = new Rectangle();
            TempBox.Width = Width;
            TempBox.Height = Height;
            TempBox.X = (int)Position.X + x;
            TempBox.Y = (int)Position.Y + y;
            BoundingBoxes.Add(TempBox);

        }
        public void Draw(SpriteBatch spritebatch, Camera C, GameTime GT)
        {
            Vector2 ScreenPos;
            ScreenPos.X = Position.X + (C.ScreenView.Width / 2) - C.MapPosition.X;
            ScreenPos.Y = Position.Y + (C.ScreenView.Height / 2) - C.MapPosition.Y;
            spritebatch.Draw(Picture, ScreenPos, Color.White);
            if (Animation != null)
            {
                Animation.UpdateFrame(GT);
                Animation.Draw(spritebatch, Position, C);
            }
        }
    }
}
