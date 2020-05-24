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
    class AutoWeapon : Weapon
    {
        private Random Rand = new Random();
        public override void Create(string[] param, ContentHolder TCH)
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
        public override void Fire(MainCharacter MC, ProjectileManager PM, MouseState MS, GameTime GT)
        {
            MC.ShootTimeCounter += (float)GT.ElapsedGameTime.TotalSeconds;
            if ((MC.ShootTimeCounter >= ShotTime) & (MS.LeftButton.Equals(ButtonState.Pressed)))
            {
                MC.ShootTimeCounter = 0.0f;
                Projectile TempProj = new Projectile();
                TempProj.Type = Get(MC.CurrentProjectileType);
                TempProj.Speed = TempProj.Type.LaunchSpeed;
                TempProj.Damage = TempProj.Type.Damage;
                TempProj.Origin = TempProj.Type.InitialOrigin;
                Vector2 Temp;
                if (MC.Aimed)
                {
                    Temp = Vector2.Transform(AimGunEnd, Matrix.CreateRotationZ(MC.Angle));
                }
                else
                {
                    Temp = Vector2.Transform(GunEnd, Matrix.CreateRotationZ(MC.Angle));
                }
                TempProj.Position = MC.MapPosition + Temp;
                if (MC.Aimed)
                {
                    TempProj.Angle = MC.Angle;
                }
                else
                {
                    TempProj.Angle = MC.Angle + (float)(Rand.NextDouble() * 0.16 - 0.08);
                }

                PM.Projectiles.Add(TempProj);
            }
            
        }
    }
}
