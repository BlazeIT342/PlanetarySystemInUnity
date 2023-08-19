using UnityEngine;

namespace Planetery.Core
{
    [CreateAssetMenu(fileName = "NewMassClassSpecifications", menuName = "Planetery/ClassSpecifications")]
    public class MassClassSpecifications : ScriptableObject
    {
        [SerializeField] MassClass[] massClasses;

        public MassClass GetRandomMassClass()
        {
            int randomIndex = Random.Range(0, massClasses.Length);
            return massClasses[randomIndex];
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