using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceMerc.RandomObjects;

namespace SpaceMerc.RandomObjects
{
    public class FileServices
    {
        public static List<string> ReadFileLines(string filepath)
        {
            string[] lines = System.IO.File.ReadAllLines(filepath);
            List<string> temp = new List<string>();
            foreach (string line in lines)
            {
                // allow for commented lines in files
                if (line[0] != '@')
                {
                    temp.Add(line);
                }
            }
            return temp;
        }
        public static string[] LineCommaSplit(string line)
        {
            string[] temp;
            temp = line.Split(',');
            return temp;
        }
    }
}
