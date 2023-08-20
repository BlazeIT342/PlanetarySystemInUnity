using Planetary.Core;

namespace Planetary.Interfaces
{
    public interface IPlanetaryObject
    {
        public MassClassEnum massClass { get; set; }
        public double mass { get; set; }
        public float size { get; set; }
        public float orbitalOffset { get; set; }
        public void RotationUpdate(float deltaTime);
    }
}