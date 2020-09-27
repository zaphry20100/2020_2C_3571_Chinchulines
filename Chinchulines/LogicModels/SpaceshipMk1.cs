namespace Chinchulines.LogicModels
{
    public class SpaceshipMk1 : Spaceship
    {

        private const string ModelPath = "Models/Spaceships/SpaceShip-MK1";
        private const string EffectPath = "Effects/Shader"; // For the moment
        private const string TexturePath = "Textures/Spaceships/MK1/MK1-Texture";
        
        public SpaceshipMk1() : base(ModelPath, EffectPath, TexturePath)
        {
        }
    }
}