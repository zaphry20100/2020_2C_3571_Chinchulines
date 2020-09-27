using Chinchulines.LogicModels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Chinchulines.Utilities
{
    public class AxisLineHelper : ModelObject
    {
        private VertexPositionColor[] _vertices;
        private VertexPositionColor[] AxisLinesVertices { get; }
        private VertexBuffer Vertices { get; }
        private BasicEffect BasicEffect { get; }

        private readonly GraphicsDevice _device;
        public AxisLineHelper(GraphicsDevice device)
        {
            _device = device;
            AxisLinesVertices = new VertexPositionColor[6];
            // Red = +x Axis
            AxisLinesVertices[0] = new VertexPositionColor(Vector3.Zero, Color.Red);
            AxisLinesVertices[1] = new VertexPositionColor(Vector3.UnitX * 60, Color.Red);
            // Green = +y Axis
            AxisLinesVertices[2] = new VertexPositionColor(Vector3.Zero, Color.Green);
            AxisLinesVertices[3] = new VertexPositionColor(Vector3.UnitY * 60, Color.Green);
            // Blue = +z Axis
            AxisLinesVertices[4] = new VertexPositionColor(Vector3.UnitZ * 60, Color.Blue);
            AxisLinesVertices[5] = new VertexPositionColor(Vector3.Zero, Color.Blue);

            BasicEffect = new BasicEffect(_device);
            
            Vertices = new VertexBuffer(_device, VertexPositionColor.VertexDeclaration, 6,
                BufferUsage.WriteOnly);
            Vertices.SetData(AxisLinesVertices);
        }
        
        public override void Load(ContentManager content)
        {
            _vertices = new VertexPositionColor[4];
            _vertices[0] = new VertexPositionColor(-Vector3.UnitX, Color.Red);
            _vertices[1] = new VertexPositionColor(Vector3.UnitX, Color.Red);
            _vertices[2] = new VertexPositionColor(-Vector3.UnitY, Color.Red);
            _vertices[3] = new VertexPositionColor(Vector3.UnitY, Color.Red);
        }

        public override void Update()
        {
            throw new System.NotImplementedException();
        }

        public override void Draw(Matrix view, Matrix projection, Vector3 cameraPosition)
        {

            BasicEffect.World = Matrix.Identity;
            BasicEffect.View = view;
            BasicEffect.Projection = projection;
            BasicEffect.VertexColorEnabled = true;

            foreach (var pass in BasicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                _device.DrawUserPrimitives(PrimitiveType.LineList, AxisLinesVertices, 0, 3);
            }
        }

        public override void Unload()
        {
            // Unload Content?
        }
    }
}