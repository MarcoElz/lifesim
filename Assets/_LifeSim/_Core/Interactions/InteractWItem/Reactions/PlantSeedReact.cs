using UnityEngine;
using LifeSim.Core.Items;
using LifeSim.Farming;

namespace LifeSim.Core.Interaction
{
    public class PlantSeedReact : ReactionableUnknowItem
    {

        protected override void UsingItem(Item item)
        {
            if (item is Seed)
            {
                GetComponent<Soil>().Plant((Seed)item);
            }


        }
    }
}