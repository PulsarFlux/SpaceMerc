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
    class Weapon
    {
        // public WeaponType Type;
        public List<ProjectileType> ProjectileTypes;
        public Texture2D Picture;
        public Vector2 GunEnd;
        public Vector2 AimGunEnd;
        public float Reload;
        public float ROF;
        public float ShotTime;
        public int Capacity;
        public ProjectileType Get(string Name)
        {
            ProjectileType Result = new ProjectileType();
            for (int i = 0; i < ProjectileTypes.Count(); i++)
            {
                if (ProjectileTypes[i].Name == Name)
                {
                    Result = ProjectileTypes[i];
                    i = ProjectileTypes.Count();
                }
            }
            return Result;
        }
        public virtual void Create(string[] param, ContentHolder TCH)
        {
            int rof = Convert.ToInt32(param[0]);
            string PTName = param[1];
            Texture2D PTPic = TCH.GetTexture(param[2]);
            int LaunchSpeed = Convert.ToInt32(param[3]);
            int Damage = Convert.ToInt32(param[4]);
            int GunEndX = Convert.ToInt32(param[5]);
            int GunEndY = Convert.ToInt32(param[6]);
            int AimGunEndX = Convert.ToInt32(param[7]);
            int AimGunEndY = Convert.ToInt32(param[8]);    
            ProjectileTypes = new List<ProjectileType>();
            ProjectileType PT = new ProjectileType();
            PT.Create(PTName, PTPic, null, LaunchSpeed, Damage);
            ProjectileTypes.Add(PT);
            ROF = (float)rof;
            ShotTime = 1 / ROF;
            GunEnd = new Vector2((float)GunEndX, (float)GunEndY);
            AimGunEnd = new Vector2((float)AimGunEndX, (float)AimGunEndY);
        }
        public virtual void Fire(MainCharacter MC, ProjectileManager PM, MouseState MS, GameTime GT)
        {
            Projectile TempProj = new Projectile();
            TempProj.Type = Get(MC.CurrentProjectileType);
            TempProj.Speed = TempProj.Type.LaunchSpeed;
            TempProj.Origin = TempProj.Type.InitialOrigin;
            TempProj.Position = MC.MapPosition;
            TempProj.Angle = MC.Angle;
            PM.Projectiles.Add(TempProj);
        }

    }
}
