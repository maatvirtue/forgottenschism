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

using ForgottenSchism.engine;
using ForgottenSchism.screen;

namespace ForgottenSchism
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch sb;

        public StateManager stateMng;

        public MainMenu mainMenu;
        public CharCre charCre;
        public WorldMap worldMap;
        public Region region;
        public Load load;
        public ArmyManage armyManage;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            mainMenu = new MainMenu(this);
            charCre = new CharCre(this);
            worldMap = new WorldMap(this);
            region = new Region(this);
            load = new Load(this);
            armyManage = new ArmyManage(this);
            //dont forget to add screen here and in components.add

            stateMng = new StateManager(mainMenu);

            Components.Add(load);
            Components.Add(region);
            Components.Add(mainMenu);
            Components.Add(charCre);
            Components.Add(worldMap);
            Components.Add(armyManage);
            Components.Add(new InputHandler(this));

            graphics.PreferredBackBufferWidth = 12 * 64;
            graphics.PreferredBackBufferHeight = (int)(8.5 * 64.0);
        }

        public int WindowWidth
        {
            get { return graphics.PreferredBackBufferWidth; }
        }

        public int WindowHeight
        {
            get { return graphics.PreferredBackBufferHeight; }
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            sb = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
