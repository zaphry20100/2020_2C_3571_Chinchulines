using Chinchulines.Cameras;
using Chinchulines.Geometrics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.Graphics
{
    public class DeathStarTrench : DrawableGameComponent
    {
        private readonly ChinchuGame _game;
        private BasicEffect BasicEffect;

        private FullScreenQuad FullScreenQuad;

        private RenderTarget2D MainSceneRenderTarget;

        public DeathStarTrench(ChinchuGame game) : base(game)
        {
            _game = game;
        }

        private FreeCamera Camera { get; set; }
        private Model Model { get; set; }

        public override void Initialize()
        {
            var screenSize = new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            Camera = new FreeCamera(GraphicsDevice.Viewport.AspectRatio, new Vector3(-350, 50, 400), screenSize);
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // We load the city meshes into a model
            Model = Game.Content.Load<Model>("Scene/city/city");
            BasicEffect = (BasicEffect)Model.Meshes[0].Effects[0];

            // Create a full screen quad to post-process
            FullScreenQuad = new FullScreenQuad(GraphicsDevice);

            // Create render targets. 
            // MainRenderTarget is used to store the scene color
            // BloomRenderTarget is used to store the bloom color and switches with MultipassBloomRenderTarget
            // depending on the pass count, to blur the bloom color
            MainSceneRenderTarget = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width,
                GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.Depth24Stencil8, 0,
                RenderTargetUsage.DiscardContents);

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            // Update the state of the camera
            Camera.Update(gameTime);

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            DrawRegular();

            base.Draw(gameTime);
        }

        private void DrawRegular()
        {
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;

            foreach (var modelMesh in Model.Meshes)
                foreach (var part in modelMesh.MeshParts)
                    part.Effect = BasicEffect;

            Model.Draw(Matrix.Identity, Camera.View, Camera.Projection);
        }

        protected override void UnloadContent()
        {
            base.UnloadContent();
            FullScreenQuad.Dispose();
            MainSceneRenderTarget.Dispose();
        }
    }
}
