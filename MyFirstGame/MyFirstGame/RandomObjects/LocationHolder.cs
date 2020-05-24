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

namespace SpaceMerc.RandomObjects
{
    class LocationHolder
    {
        public List<Location> Locations = new List<Location>();
        public void Create(ContentHolder TCH, GraphicsDeviceManager GDM, Camera ACamera)
        {
            List<String> AreaNames = FileServices.ReadFileLines(@"Files\AreaNames.txt");
            foreach (string AreaName in AreaNames)
            {
                AddLocation(AreaName, TCH, GDM, ACamera);
            }
        }
        void AddLocation(string AreaName, ContentHolder TCH, GraphicsDeviceManager GDM, Camera ACamera)
        {
            Location Temp = new Location();
            List<String> FileLines = FileServices.ReadFileLines(@"Files\" + AreaName + ".txt");
            List<String> TilePlanRows = FileServices.ReadFileLines(@"Files\" + AreaName + "TilePlan.txt");
            foreach (String CurrentLine in FileLines)
            {
                String[] Line = FileServices.LineCommaSplit(CurrentLine);
                if (Line[0] == "0")
                { 
                   Temp.Create(Line, TCH, TilePlanRows);
                }
                else if (Line[0] == "1")
                {
                    Temp.AddObject(Line, TCH);
                }
                else if (Line[0] == "3")
                {
                    Temp.AddEnemy(GDM, Line, ACamera, TCH);
                }
               
            }
            Locations.Add(Temp);
            
        }
    }
}
