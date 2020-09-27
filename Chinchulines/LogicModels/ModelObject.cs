using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.LogicModels
{
    public abstract class ModelObject
    {
        public Model Model { get; set; }
        public Texture2D Texture2D { get; set; }
        
        public Vector3 GlobalPosition { get; set; } 
        public Effect Effect { get; set; }
        public Matrix World { get; set; }

        protected Camera _camera;
        public abstract void Load(ContentManager content);
        public abstract void Update();
        public abstract void Draw(Matrix projection);
        public abstract void Unload();

        public ModelObject(Camera camera)
        {
            _camera = camera;
        }
    }
}