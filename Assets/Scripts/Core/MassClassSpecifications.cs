using UnityEngine;

namespace Planetary.Core
{
    [CreateAssetMenu(fileName = "NewMassClassSpecifications", menuName = "Planetary/ClassSpecifications")]
    public class MassClassSpecifications : ScriptableObject
    {
        [SerializeField] MassClass[] massClasses;

        public MassClass GetRandomMassClass()
        {
            int randomIndex = Random.Range(0, massClasses.Length);
            return massClasses[randomIndex];
        }

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