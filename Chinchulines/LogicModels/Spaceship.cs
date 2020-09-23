using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines.LogicModels
{
    public class Spaceship : ModelObject
    {
        private readonly string _modelPath;
        private readonly string _effectPath;
        private readonly string _texturePath;
        private Vector3 _rotation = Vector3.Zero;
        private Vector3 _position = Vector3.Zero;
        private const float MovementSpeed = .5f;
        private InputActions InputActions { get; }
        protected Spaceship(string modelPath, string effectPath, string texturePath)
        {
            _modelPath = modelPath;
            _effectPath = effectPath;
            _texturePath = texturePath;

            Position = new Vector3(0, 0, 0);
            
            // Set the controls for Spaceships
            InputActions = new InputActions
            {
                Left = Keys.A,
                Right = Keys.D,
                Up = Keys.W,
                Down = Keys.S,
                Accelerate = Keys.Space,
                Shoot = Keys.F
            };

            // TODO Same Matrix for all spaceships?
            World = Matrix.CreateScale(15f) * Matrix.CreateRotationX(MathHelper.PiOver2);
        }
        
        public override void Load(ContentManager content)
        {
            // if (modelPath == null || effectPath == null || texturePath == null)
            // {
            //     throw new Exception();
            // }
            
            Model = content.Load<Model>(_modelPath);
            Effect = content.Load<Effect>(_effectPath);
            Texture2D = content.Load<Texture2D>(_texturePath);
        }

        public override void Update()
        {
            Move(Keyboard.GetState());
        }
        
        public override void Draw(Matrix view, Matrix projection)
        {
            
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                    part.Effect.Parameters["World"].SetValue(World * mesh.ParentBone.Transform * Matrix.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z));
                    part.Effect.Parameters["View"].SetValue(view);
                    part.Effect.Parameters["Projection"].SetValue(projection);
                    part.Effect.Parameters["ModelTexture"].SetValue(Texture2D);
                }
                mesh.Draw();
            }
        }

        // Private methods
        // TODO Improve movements
        private void Move(KeyboardState state)
        {
            if (InputActions == null) return;

            var isMoving = false;
            const float rotationSpeed = .01f;
            
            if (state.IsKeyDown(InputActions.Up))
            {
                // TODO
                _position.Y += MovementSpeed;
                if (_rotation.Y < MathHelper.PiOver4)
                {
                    _rotation.Y += rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Down))
            {
                // TODO 
                _position.Y -= MovementSpeed;
                if (_rotation.Y > -MathHelper.PiOver4)
                {
                    _rotation.Y += -rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Left))
            {
                // TODO 
                _position.X -= MovementSpeed;
                if (_rotation.Z < MathHelper.PiOver2)
                {
                    _rotation.Z += rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Right))
            {
                // TODO 
                _position.X += MovementSpeed;
                if (_rotation.Z > -MathHelper.PiOver2)
                {
                    _rotation.Z += -rotationSpeed;
                }
                isMoving = true;
            }
            if (!isMoving)
            {
                // _rotation = new Vector3(0, 0, 0);
            }
        }
    }
}