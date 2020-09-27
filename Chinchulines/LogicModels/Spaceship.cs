using Chinchulines.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Chinchulines.LogicModels
{
    /**
     * ○ Crear un escenario en el espacio en el cual un usuario controla una nave espacial
        en tercera persona se mueve por pasillos y esquiva obstáculos y disparos
        enemigos.
        ○ Los pasillos pueden ser generados offline pero deben dar sensación de variable.
        ○ La nave del usuario debe contar con los siguientes movimientos:
        ■ Acelerar y bajar velocidad.
        ■ Poder subir y bajar.
        ■ Rotar 90º y volver a la posición horizontal.
        ○ La nave puede disparar rayos láseres a algunos obstáculos para romperlos.
        ○ Deben aparecer torres y/o Tie Fighters de forma aleatoria que ataquen al jugador.
        ○ Hacer una vuelta de barril (Do a barrell roll), se vuelve invulnerable el tiempo que
        dure la vuelta porque esquiva todos los rayos enemigos.
     */
    
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

            // Set the controls for Spaceships
            InputActions = new InputActions
            {
                Left = Keys.A,
                Right = Keys.D,
                Up = Keys.W,
                Down = Keys.S,
                Accelerate = Keys.Space,
                Shoot = Keys.F,
                Roll = Keys.Tab
            };

            // TODO Same Matrix for all spaceships?
            // Rotation => MK1 bad initial position
            World = Matrix.CreateScale(15f) * Matrix.CreateRotationY(MathHelper.Pi) * Matrix.CreateRotationX(-MathHelper.PiOver2);
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
        
        public override void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {
            
            foreach (ModelMesh mesh in Model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = Effect;
                    part.Effect.Parameters["World"].SetValue(World * mesh.ParentBone.Transform * 
                                                             Matrix.CreateTranslation(_position) * 
                                                             Matrix.CreateFromYawPitchRoll(_rotation.X, _rotation.Y, _rotation.Z));
                    part.Effect.Parameters["View"].SetValue(view);
                    part.Effect.Parameters["Projection"].SetValue(projection);
                    part.Effect.Parameters["ModelTexture"].SetValue(Texture2D);
                }
                mesh.Draw();
            }
        }

        public override void Unload()
        {
            // Unload Content?
        }

        // Private methods
        // TODO Improve movements
        private void Move(KeyboardState state)
        {
            if (InputActions == null) return;

            var isAccelerating = false;
            var isMoving = false;
            const float rotationSpeed = .01f;
            
            if (state.IsKeyDown(InputActions.Up))
            {
                // TODO
                if (_rotation.Y > -MathHelper.PiOver4)
                {
                    _rotation.Y -= rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Down))
            {
                // TODO 
                if (_rotation.Y < MathHelper.PiOver4)
                {
                    _rotation.Y += rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Left))
            {
                // TODO 
                if (_rotation.Z > -MathHelper.PiOver2)
                {
                    _rotation.Z -= rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Right))
            {
                // TODO 
                if (_rotation.Z < MathHelper.PiOver2)
                {
                    _rotation.Z += rotationSpeed;
                }
                isMoving = true;
            }
            if (state.IsKeyDown(InputActions.Roll))
            {
            }
            if (state.IsKeyDown(InputActions.Accelerate))
            {
                isAccelerating = true;
                if (_position.Z < 3f)
                {
                    _position.Z += .15f;
                }
            }
            if (!isAccelerating)
            {
                if (_position.Z > 0)
                {
                    _position.Z -= .15f;
                }
            }
            if (!isMoving)
            {
                // _rotation = new Vector3(0, 0, 0);
            }
        }
    }
}