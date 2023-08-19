using UnityEngine;

namespace Planetery.Core
{
    [CreateAssetMenu(fileName = "NewMassClassSpecifications", menuName = "Planetery/ClassSpecifications")]
    public class MassClassSpecifications : ScriptableObject
    {
        [SerializeField] MassClass[] massClasses;

        [System.Serializable]
        class MassClass
        {
            [SerializeField] MassClassEnum massClassEnum;
            [SerializeField] float massFrom; 
            [SerializeField] float massTo;
            [SerializeField] float radiusFrom;
            [SerializeField] float radiusTo;
        }
    }
}