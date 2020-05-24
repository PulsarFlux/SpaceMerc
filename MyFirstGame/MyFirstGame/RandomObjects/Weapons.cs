using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceMerc.RandomObjects;

namespace SpaceMerc.RandomObjects
{
    class Weapons
    {
        public List<Weapon> WeaponList = new List<Weapon>();
        public void Create(List<string> paramlist, ContentHolder TCH)
        {
            foreach (string paramline in paramlist)
            {
                AddWeapon(FileServices.LineCommaSplit(paramline), TCH);
            }
        }
        void AddWeapon(string[] param, ContentHolder TCH)
        {
            if (param[0] == "0")
            {
                Weapon Temp = new Weapon();
                Temp.Create(param, TCH);
                WeaponList.Add(Temp);
            }
            else
            {
                AutoWeapon Temp = new AutoWeapon();
                Temp.Create(param, TCH);
                WeaponList.Add(Temp);
            }
        }
    }
}
