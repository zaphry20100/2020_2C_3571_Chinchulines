using System.Collections.Generic;
using Chinchulines.Graphics;
using Chinchulines.LogicModels;
using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines
{
    public class ChinchuGame : Game
    {
        private const bool ShowAxisLines = true;
        // public const string ContentFolderMusic = "Music/";
        // public const string ContentFolderSounds = "Sounds/";
        // public const string ContentFolderSpriteFonts = "SpriteFonts/";
        public ChinchuGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            // Graphics.IsFullScreen = true;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        private GraphicsDeviceManager Graphics { get; }
        private SpriteBatch SpriteBatch { get; set; }
        private Matrix Projection { get; set; }

        private Camera _camera;
        
        private readonly List<ModelObject> _models = new List<ModelObject>();
        private AxisLineHelper _axisLines;
        protected override void Initialize()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 800f / 600f, 0.1f, 1000f);
  
            _models.Add(new SpaceBox(GraphicsDevice));
            _models.Add(new SpaceshipMk1());
            
            _camera = new Camera();
            
            if (ShowAxisLines)
            {
                _axisLines = new AxisLineHelper(GraphicsDevice);
            }
            
            Graphics.PreferredBackBufferWidth = 1024;
            Graphics.PreferredBackBufferHeight = 768;
            Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            foreach (var modelObject in _models)
            {
                modelObject.Load(Content);
            }

            if (ShowAxisLines)
            {
                _axisLines.Load(Content);
            }
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            foreach (var modelObject in _models)
            {
                modelObject.Update();
            }
            
            _camera.Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            foreach (var modelObject in _models)
            {
                modelObject.Draw(_camera.View, Projection, _camera.Position);
            }
            
            if (ShowAxisLines)
            {
                _axisLines.Draw(_camera.View, Projection, _camera.Position);
            }
            
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            
            foreach (var modelObject in _models)
            {
                modelObject.Unload();
            }

            base.UnloadContent();
        }
    }
}