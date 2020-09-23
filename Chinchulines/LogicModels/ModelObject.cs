using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.LogicModels
{
    public abstract class ModelObject
    {
        public Model Model { get; set; }
        public Texture2D Texture2D { get; set; }
        
        public Vector3 Position { get; set; } 
        public Effect Effect { get; set; }
        public Matrix World { get; set; }
        public abstract void Load(ContentManager content);
        public abstract void Update();
        public abstract void Draw(Matrix view, Matrix projection);
    }
}