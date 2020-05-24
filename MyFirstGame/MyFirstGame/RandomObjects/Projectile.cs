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
    class Projectile
    {
        public ProjectileType Type;
        public Vector2 Position;
        public int Speed;
        public float Angle;
        public int Damage;
        public Vector2 Origin;
    }
}
