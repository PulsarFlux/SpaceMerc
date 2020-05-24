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
    class ContentHolder
    {
        public List<TextureHolder> Textures = new List<TextureHolder>();
        public Texture2D GetTexture(string TexName)
        {
            Texture2D Temp;
            Temp = null;
            for (int i = 0; i < Textures.Count; i++)
            {
                if (Textures[i].Filename == TexName)
                {
                    Temp = Textures[i].Texture;
                    i = Textures.Count;
                }
            }
            return Temp;
        }
    }
}
