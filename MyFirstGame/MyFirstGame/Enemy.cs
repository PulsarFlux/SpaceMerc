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
    class Enemy : Creature
    {
        public int InitialHealth;
        public int CurrentHealth;
        public Texture2D HealthRectangle;
        public Color[] HRColour; 
        public Vector2 HROrigin;
        public Vector2 HROffset;
        public void Create(GraphicsDeviceManager GDM, string[] param, Camera ACamera, ContentHolder TCH)
        {
            int x = Convert.ToInt32(param[1]);
            int y = Convert.ToInt32(param[2]); 
            Texture2D Tex = TCH.GetTexture(param[3]);
            InitialHealth = Convert.ToInt32(param[4]);
            CurrentHealth = InitialHealth;
            MovementSpeed = Convert.ToInt32(param[5]);
            MapPosition.X = x;
            MapPosition.Y = y;
            ScreenPosition.X = MapPosition.X - ACamera.MapPosition.X + (ACamera.ScreenView.Width / 2);
            ScreenPosition.Y = MapPosition.Y - ACamera.MapPosition.Y + (ACamera.ScreenView.Height / 2);
            origin.X = (float)Math.Round((double)(Tex.Width / 2), 0, MidpointRounding.AwayFromZero);
            origin.Y = (float)Math.Round((double)(Tex.Height / 2), 0, MidpointRounding.AwayFromZero);
            HealthRectangle = new Texture2D(GDM.GraphicsDevice, InitialHealth + 2, 5);
            HROrigin = new Vector2((float)Math.Round((double)((InitialHealth + 2)/2), 0, MidpointRounding.AwayFromZero), 3f);
            HRColour = new Color[5 * (InitialHealth + 2)];
            for (int i = 0; i < 5 * (InitialHealth + 2); i++)
            {
                HRColour[i] = Color.Black;
            }
            for (int i = 1; i <= InitialHealth; i++)
            {
                HRColour[i + (InitialHealth + 2)] = Color.Lime;
                HRColour[i + 2 * (InitialHealth + 2)] = Color.Lime;
                HRColour[i + 3 * (InitialHealth + 2)] = Color.Lime;
            }
            HealthRectangle.SetData<Color>(HRColour);
            Pic = Tex;
            Box = Tex.Bounds;
            HROffset.Y = Box.Height/2 + 10;
            CurrentPic = Pic;
            Box.X = x - (int)(origin.X);
            Box.Y = y - (int)(origin.Y);
        }
        public void UpdatePos(Camera Cam)
        {
            ScreenPosition.X = MapPosition.X - Cam.MapPosition.X + (Cam.ScreenView.Width / 2);
            ScreenPosition.Y = MapPosition.Y - Cam.MapPosition.Y + (Cam.ScreenView.Height / 2);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(CurrentPic, ScreenPosition, null, Color.White, Angle, origin, 1.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(HealthRectangle, Vector2.Add(ScreenPosition,HROffset), null, Color.White, 0.0f, HROrigin, 1.0f, SpriteEffects.None, 0);
        }
        public void Die()
        {
        }
        public bool Hit(Projectile P)
        {
            CurrentHealth -= P.Damage;
            if (CurrentHealth <= 0)
            {
                return true;
            }
            else
            {
                for (int i = CurrentHealth; i <= InitialHealth; i++)
                {
                    HRColour[i + (InitialHealth + 2)] = Color.Black;
                    HRColour[i + 2 * (InitialHealth + 2)] = Color.Black;
                    HRColour[i + 3 * (InitialHealth + 2)] = Color.Black;
                }
                HealthRectangle.SetData<Color>(HRColour);
                return false;
            }
        }
     }
}
