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
    class Cursor
    {
        public Texture2D Picture;
        public Texture2D CursorAimPic;
        public Texture2D CharacAimPic;
        public MouseState currentmouse;
        public MouseState previousmouse;
        public Vector2 Pos;
        public Vector2 CursorOrigin;
        public Vector2 AimCursorOrigin;
        private Vector2 Offset;
        private Vector2 AimOffset;
        public Color Colour;
        public float Angle;
        public double Distance;
        private Rectangle SourceRect;
        public int GetSize(MainCharacter MC)
        {
            int Size;
            Size = (int)(Distance * 0.15) + 10;
            return Size;
        }
        public void SetMouse(MainCharacter MC, ProjectileManager PM, GameTime GT)
        {
            currentmouse = Mouse.GetState();
            Pos.X = (currentmouse.X);
            Pos.Y = (currentmouse.Y);
            if ((currentmouse.RightButton.Equals(ButtonState.Pressed)) & !(previousmouse.RightButton.Equals(ButtonState.Pressed)))
            {
                MC.AimPressed(this);
                
            }
            Distance = Math.Sqrt(Math.Pow((Pos.X - MC.ScreenPosition.X), 2.0f) + Math.Pow((Pos.Y - MC.ScreenPosition.Y), 2.0f));
            Vector2 Pointing = new Vector2();
            if (MC.Aimed)
            {
                Pointing = Pos - MC.ScreenPosition - Vector2.Transform(AimOffset, Matrix.CreateRotationZ(MC.Angle));
            }
            else
            {
                Pointing = Pos - MC.ScreenPosition - Vector2.Transform(Offset, Matrix.CreateRotationZ(MC.Angle));
            }
            Angle = (float)Math.Atan2(Pointing.Y, Pointing.X);
            // Angle = - (float)Math.Atan2((double)(MC.ScreenPosition.Y - Pos.Y),(double)(Pos.X - MC.ScreenPosition.X)) - (float)Math.Asin(Math.Abs(AimOffset.Y) / Distance);
            // reset angles if they go out of bounds and turn MC

            if (MC.Aimed)
            {
                if (MC.Angle < -1 * MathHelper.Pi) MC.Angle += MathHelper.TwoPi;
                if (MC.Angle > MathHelper.Pi) MC.Angle -= MathHelper.TwoPi;
                if (Math.Abs((Angle - MC.Angle)) <= MC.AimTurnSpeed)
                {
                    MC.Angle = Angle;
                }
                else if (Angle > MathHelper.PiOver2 & MC.Angle < -1 * MathHelper.PiOver2)
                {
                    MC.Angle -= MC.AimTurnSpeed;
                }
                else if (Angle < -1 * MathHelper.PiOver2 & MC.Angle > MathHelper.PiOver2)
                {
                    MC.Angle += MC.AimTurnSpeed;
                }
                else if (Angle > MC.Angle)
                {
                    MC.Angle += MC.AimTurnSpeed;
                }
                else
                {
                    MC.Angle -= MC.AimTurnSpeed;
                }
            }
            else if (Distance > 14)
            {
                MC.Angle = Angle;
            }
            MC.Fire(PM, currentmouse, GT);

            // cursor colour

            if (currentmouse.LeftButton.Equals(ButtonState.Pressed))
            {
                Colour = Color.Red;
            }
            else
            {
                Colour = Color.White;
            }
            previousmouse = currentmouse;
        }
        public void Create(Texture2D Pic, Texture2D CursorAimPicture, Texture2D CharacAimPicture, Color CursorColour)
        {
            Picture = Pic;
            CursorAimPic = CursorAimPicture;
            CharacAimPic = CharacAimPicture;
            SourceRect.Width = Pic.Width;
            SourceRect.X = 0;
            Colour = CursorColour;
            AimCursorOrigin.X = (float)Math.Round(((double)CursorAimPicture.Width / 2), 0, MidpointRounding.AwayFromZero);
            AimCursorOrigin.Y = (float)Math.Round(((double)CursorAimPicture.Height / 2), 0, MidpointRounding.AwayFromZero);
            Offset = new Vector2(0f, 14f);
            AimOffset = new Vector2(0f, 11f);
            // Origin.X = (float)Math.Round((double)(Picture.Width / 2), 0, MidpointRounding.AwayFromZero);
            CursorOrigin.X = 119;
            // Origin.Y = (float)Math.Round((double)(Picture.Height / 2), 0, MidpointRounding.AwayFromZero);
        }
        public void Draw(SpriteBatch spriteBatch, MainCharacter MC)
        {
            if (!MC.Aimed)
            {
                SourceRect.Height = GetSize(MC);
                CursorOrigin.Y = (float)Math.Round(((double)SourceRect.Height / 2), 0, MidpointRounding.AwayFromZero);
                SourceRect.Y = (Picture.Height - SourceRect.Height) / 2;
                spriteBatch.Draw(Picture, Pos, SourceRect, Colour, Angle, CursorOrigin, 1.0f, SpriteEffects.None, 0);
            }
            else
            {
                spriteBatch.Draw(CursorAimPic, Pos, null, Colour, Angle, AimCursorOrigin, 1.0f, SpriteEffects.None, 0);
                Vector2 TempPos;
                TempPos.X = (float)(Distance * Math.Cos((double)MC.Angle)) + MC.ScreenPosition.X + Vector2.Transform(AimOffset, Matrix.CreateRotationZ(MC.Angle)).X;
                TempPos.Y = (float)(Distance * Math.Sin((double)MC.Angle)) + MC.ScreenPosition.Y + Vector2.Transform(AimOffset, Matrix.CreateRotationZ(MC.Angle)).Y;
                spriteBatch.Draw(CharacAimPic, TempPos, null, Colour, MC.Angle, AimCursorOrigin, 1.0f, SpriteEffects.None, 0);
            }
        }
    }
}
