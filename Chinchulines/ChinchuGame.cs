using Chinchulines.Graphics;
using Chinchulines.LogicModels;
using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines
{
    /// <summary>
    ///     Esta es la clase principal  del juego.
    ///     Inicialmente puede ser renombrado o copiado para hacer más ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar la clase que ejecuta Program <see cref="Program.Main()" /> linea 10.
    /// </summary>
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
        
        public Vector3 GlobalPosition = Vector3.Zero; 

        // private readonly List<ModelObject> _models = new List<ModelObject>();
        private AxisLineHelper _axisLines;
        private SpaceBox box;
        private SpaceshipMk1 mk1;
        private Planet venus;
        protected override void Initialize()
        {
            Projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, 800f / 600f, 0.1f, 1000f);
  
            // _models.Add(new SpaceBox(GraphicsDevice));
            // _models.Add(new SpaceshipMk1(GlobalPosition));
            _camera = new Camera(GlobalPosition, Vector3.Zero);
            
            box = new SpaceBox(GraphicsDevice, _camera);
            mk1 = new SpaceshipMk1(GlobalPosition, _camera);
            venus = new Planet(_camera);
            
            // _camera.Position = GlobalPosition;
            
            // if (ShowAxisLines)
            // {
            //     _axisLines = new AxisLineHelper(GraphicsDevice);
            // }
            
            Graphics.PreferredBackBufferWidth = 1024;
            Graphics.PreferredBackBufferHeight = 768;
            Graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            
            // foreach (var modelObject in _models)
            // {
            //     modelObject.Load(Content);
            // }
            
            box.Load(Content);
            mk1.Load(Content);
            venus.Load(Content);

            // if (ShowAxisLines)
            // {
            //     _axisLines.Load(Content);
            // }
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            // foreach (var modelObject in _models)
            // {
            //     modelObject.Update();
            // }
            
            box.Update();
            mk1.Update();
            _camera.UpdateLookAt(mk1.GlobalPosition, mk1.GlobalPosition);
            venus.Update();
            
            // _camera.Move();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            
            // foreach (var modelObject in _models)
            // {
            //     modelObject.Draw(Matrix.Identity, Projection, Vector3.Backward);
            // }
            
            box.Draw(Projection);
            mk1.Draw(Projection);
            venus.Draw(Projection);
            
            // if (ShowAxisLines)
            // {
            //     _axisLines.Draw(Matrix.Identity, Projection, Vector3.Backward);
            // }
            
            base.Draw(gameTime);
        }

        protected override void UnloadContent()
        {
            Content.Unload();
            
            // foreach (var modelObject in _models)
            // {
            //     modelObject.Unload();
            // }

            base.UnloadContent();
        }
    }
}