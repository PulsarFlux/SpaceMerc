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
    class AnimatedSprite
    {
        public int FramesWide;
        public int FramesHigh;
        public Texture2D SourceTexture;
        public Rectangle SourceRect;
        public int Frames;
        public Boolean Paused;
        public Boolean Playing;
        public float FPS;
        public float TotalElapsed;
        public int CurrentFrameNum;
        public float TPF;
        public void Pause()
        {
            Paused = true;
        }
        public void Unpause()
        {
            Paused = false;
        }
        public void Play()
        {
            Playing = true;
        }
        public void Stop()
        {
            Playing = false;
        }
        public void UpdateFrame(GameTime gametime)
        {
            if (Paused) return;
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;
            TotalElapsed += elapsed;
            if (TotalElapsed > TPF)
            {
                CurrentFrameNum++;
                CurrentFrameNum = CurrentFrameNum % Frames;
                TotalElapsed -= TPF;
            }
        }
        public void Create(int FW, int FH, Texture2D Pic, float fps, int Fs)
        {
            FramesWide = FW;
            FramesHigh = FH;
            SourceTexture = Pic;
            FPS = fps;
            Frames = Fs;
            Paused = false;
            Playing = true;
            TPF = (1 / FPS);
            SourceRect.Width = SourceTexture.Width / FramesWide;
            SourceRect.Height = SourceTexture.Height / FramesHigh;

        }
        public void Draw(SpriteBatch spritebatch, Vector2 MapPos, Camera C)
        {
            if (!Playing) return;
            SourceRect.X = (CurrentFrameNum % FramesWide) * SourceRect.Width;
            SourceRect.Y = (CurrentFrameNum / FramesWide) * SourceRect.Height;
            Vector2 ScreenPos;
            ScreenPos.X = MapPos.X + (C.ScreenView.Width / 2) - C.MapPosition.X;
            ScreenPos.Y = MapPos.Y + (C.ScreenView.Height / 2) - C.MapPosition.Y;
            spritebatch.Draw(SourceTexture, ScreenPos, SourceRect, Color.White, 0f, Vector2.Zero, 1.0f, SpriteEffects.None, 0);
        }
    }
}

