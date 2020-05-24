using System;
using System.Collections.Generic;
using System.Linq;
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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Cursor TheCursor = new Cursor();
        MainCharacter TheMainCharacter = new MainCharacter();
        LocationHolder Areas = new LocationHolder();
        Camera TheCamera = new Camera();
        ProjectileManager TheProjectileManager = new ProjectileManager();
        KeyboardState PrevKeyState = new KeyboardState();
        KeyboardState CurrentKeyState = new KeyboardState();
        ContentHolder TheContentHolder = new ContentHolder();
        Weapons TheWeapons = new Weapons();
        const int ScreenWidth = 1300;
        const int ScreenHeight = 700;
        
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this); 
            this.graphics.PreferredBackBufferHeight = ScreenHeight;
            this.graphics.PreferredBackBufferWidth = ScreenWidth;
            this.graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            TheCamera.MapPosition.X = ScreenWidth / 2;
            TheCamera.MapPosition.Y = ScreenHeight / 2;
            TheCamera.ScreenView.Width = ScreenWidth;
            TheCamera.ScreenView.Height = ScreenHeight;
            TheCamera.XBuffer = (ScreenWidth / 2) - 5;
            TheCamera.YBuffer = (ScreenHeight / 2) - 5;
            base.Initialize();
            //    List<MainCharacter> Creatures = new List<MainCharacter>();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>  

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            TheCursor.Create(Content.Load<Texture2D>("ASCursor"), Content.Load<Texture2D>("CursorAim"),Content.Load<Texture2D>("CharacAim"), Color.White);
           

            // TODO: use this.Content to load your game content here
                       
            List<String> TextureNames = FileServices.ReadFileLines(@"Files\TextureNames.txt");
            foreach (string TexName in TextureNames)
            {
                TextureHolder temp = new TextureHolder();
                temp.Texture = Content.Load<Texture2D>(TexName);
                temp.Filename = TexName;
                TheContentHolder.Textures.Add(temp);
            }

            Areas.Create(TheContentHolder, graphics, TheCamera);

            List<string> WeaponLines = FileServices.ReadFileLines(@"Files\Weapons.txt");
            TheWeapons.Create(WeaponLines, TheContentHolder);

            //must make sure character spawns within normal camera boundaries
            TheMainCharacter.Create(150, 150, 0f, TheContentHolder.GetTexture("Man2"), TheContentHolder.GetTexture("Man_Aim2"), TheWeapons.WeaponList[0], TheCamera);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
                   
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
           
            // TODO: Add your update logic here
            TheProjectileManager.Update(Areas.Locations[0].Width, Areas.Locations[0].Height, Areas.Locations[0].Zones, Areas.Locations[0].Enemies, Areas.Locations[0].ObjectZones);
            TheMainCharacter.Move();  
            TheMainCharacter.BoundsCheck(TheCamera, Areas.Locations[0].Width,Areas.Locations[0].Height);
            Areas.Locations[0].Update(TheCamera);
            TheMainCharacter.ObjectCollisions(Areas.Locations[0],TheCamera);
            TheMainCharacter.EnemyCollisions(Areas.Locations[0], TheCamera);
            TheMainCharacter.UpdateBox();
            TheCursor.SetMouse(TheMainCharacter,TheProjectileManager, gameTime);
            TheMainCharacter.Aim(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
      
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw the sprite.
            
           
            spriteBatch.Begin(SpriteSortMode.Immediate,BlendState.AlphaBlend,SamplerState.LinearWrap,DepthStencilState.Default,RasterizerState.CullNone);
            Areas.Locations[0].Draw(spriteBatch, TheCamera, gameTime);
            TheMainCharacter.Draw(spriteBatch);
            TheProjectileManager.Draw(spriteBatch, TheCamera);
            TheCursor.Draw(spriteBatch, TheMainCharacter);
            //spriteBatch.DrawString(spriteFont, "Hello", Vector2.Zero, Color.Red);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
