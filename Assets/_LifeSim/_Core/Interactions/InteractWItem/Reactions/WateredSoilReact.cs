using UnityEngine;
using LifeSim.Farming;

namespace LifeSim.Core.Interaction
{
    public class WateredSoilReact : ChangeMaterialReact
    {
        protected override void Reaction(Vector3 point)
        {
            base.Reaction(point);
            GetComponent<Soil>().Water();
        }
    }
}
