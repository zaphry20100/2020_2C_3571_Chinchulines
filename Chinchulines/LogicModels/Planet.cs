using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.LogicModels
{
    public class Planet : ModelObject
    {
        private const string ModelPath = "Models/Venus/Venus";
        private const string EffectPath = "Effects/Shader"; // For the moment
        private const string TexturePath = "Textures/Venus/Venus-Texture";
        private float _venusRotation;
        public Planet(Camera camera) : base(camera)
        {
            World = Matrix.Identity;
        }

        public override void Load(ContentManager content)
        {
            Model = content.Load<Model>(ModelPath);
            Effect = content.Load<Effect>(EffectPath);
            Texture2D = content.Load<Texture2D>(TexturePath);
        }

        public override void Update()
        {
            _venusRotation += .005f;
        }

        public override void Draw(Matrix projection)
        {
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                    part.Effect.Parameters["World"].SetValue(World * mesh.ParentBone.Transform * Matrix.CreateScale(10f) * 
                                                             Matrix.CreateRotationY(_venusRotation) * 
                                                             Matrix.CreateTranslation(5f,2f,10));
                    part.Effect.Parameters["View"].SetValue(_camera.View);
                    part.Effect.Parameters["Projection"].SetValue(projection);
                    part.Effect.Parameters["ModelTexture"].SetValue(Texture2D);
                }
                mesh.Draw();
            }
        }

        public override void Unload()
        {
        }
    }
}