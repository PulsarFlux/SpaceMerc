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
    class ProjectileType
    {
        public string Name;
        public Texture2D Picture;
        public Texture2D AnimationTexture;
        public Rectangle BoundingBox;
        public int LaunchSpeed;
        public int Damage;
        public Vector2 InitialOrigin;
        public void Create(string N, Texture2D Pic, Texture2D AniTex, int LS, int Dam)
        {
            Name = N;
            Picture = Pic;
            AnimationTexture = AniTex;
            LaunchSpeed = LS;
            BoundingBox = Pic.Bounds;
            Damage = Dam;
            InitialOrigin.X = (float)Math.Round((double)(BoundingBox.Width / 2), 0, MidpointRounding.AwayFromZero);
            InitialOrigin.Y = (float)Math.Round((double)(BoundingBox.Height / 2), 0, MidpointRounding.AwayFromZero);
        }
        
    }
}
