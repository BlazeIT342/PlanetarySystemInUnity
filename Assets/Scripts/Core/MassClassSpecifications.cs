using UnityEngine;

namespace Planetary.Core
{
    [CreateAssetMenu(fileName = "NewMassClassSpecifications", menuName = "Planetary/ClassSpecifications")]
    public class MassClassSpecifications : ScriptableObject
    {
        [SerializeField] MassClass[] massClasses;

        public MassClass GenerateClassByMass(float mass)
        {
            foreach (MassClass massClass in massClasses)
            {
                if (mass >= massClass.massFrom && mass <= massClass.massTo)
                {
                    return massClass;
                }
            }
            return null;
        }

        public float GetMaxMass()
        {
            float maxValue = -1;
            foreach (var massClass in massClasses)
            {
                if (massClass.massTo > maxValue)
                {
                    maxValue = massClass.massTo;
                }
            }
            return maxValue;
        }

        public float GetMinMass()
        {
            float minValue = 9999;
            foreach (var massClass in massClasses)
            {
                if (massClass.massFrom < minValue)
                {
                    minValue = massClass.massFrom;
                }
            }
            return minValue;
        }

        [System.Serializable]
        public class MassClass
        {
            [SerializeField] public MassClassEnum massClassEnum;
            [SerializeField] public float massFrom; 
            [SerializeField] public float massTo;
            [SerializeField] public float radiusFrom;
            [SerializeField] public float radiusTo;
        }
    }
}