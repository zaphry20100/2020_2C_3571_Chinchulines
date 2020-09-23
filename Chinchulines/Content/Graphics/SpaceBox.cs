using Chinchulines.LogicModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.Graphics
{
    public class SpaceBox : ModelObject
    {
        private const string SpaceBoxModelPath = "Graphics/SpaceBox/cube";
        private const string SpaceBoxTexturePath = "Graphics/SpaceBox/SunInSpace";
        private const string SpaceBoxEffectPath = "Graphics/SpaceBox/SpaceBox";
        
        // Cube
        private Model _spaceBox;
        private TextureCube _spaceBoxTexture;
        private Effect _spaceBoxEffect;
        private readonly GraphicsDevice _device;

        // Size of the cube
        private const float Size = 500f;

        public SpaceBox(GraphicsDevice device)
        {
            _device = device;
        }
        
        private void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            // Go through each pass in the effect, but we know there is only one...
            foreach (EffectPass pass in _spaceBoxEffect.CurrentTechnique.Passes)
            {
                // Draw all of the components of the mesh, but we know the cube really
                // only has one mesh
                foreach (ModelMesh mesh in _spaceBox.Meshes)
                {
                    // Assign the appropriate values to each of the parameters
                    foreach (ModelMeshPart part in mesh.MeshParts)
                    {
                        part.Effect = _spaceBoxEffect;
                        part.Effect.Parameters["SkyBoxTexture"].SetValue(_spaceBoxTexture);
                        part.Effect.Parameters["World"].SetValue(Matrix.CreateScale(Size));
                        part.Effect.Parameters["View"].SetValue(view);
                        part.Effect.Parameters["Projection"].SetValue(projection);
                        part.Effect.Parameters["CameraPosition"].SetValue(cameraPosition);
                    }

                    mesh.Draw();
                }
            }
        }

        public override void Load(ContentManager content)
        {
            _spaceBox = content.Load<Model>(SpaceBoxModelPath);
            _spaceBoxTexture = content.Load<TextureCube>(SpaceBoxTexturePath);
            _spaceBoxEffect = content.Load<Effect>(SpaceBoxEffectPath);
        }

        public override void Update()
        {
            // Nothing to do
        }

        public override void Draw(Matrix view, Matrix projection)
        {
            _device.Clear(Color.CornflowerBlue);

            RasterizerState originalRasterizerState = _device.RasterizerState;
            RasterizerState rasterizerState = new RasterizerState();
            rasterizerState.CullMode = CullMode.None;
            _device.RasterizerState = rasterizerState;

            Draw(view, projection, Vector3.Zero);

            _device.RasterizerState = originalRasterizerState;

        }
    }
}
