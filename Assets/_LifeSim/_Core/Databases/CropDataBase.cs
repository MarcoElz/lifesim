using UnityEngine;
using System.Collections.Generic;

namespace LifeSim.Farming
{
    public class CropDataBase : ScriptableObjectDatabase<Crop>
    {
        public override Crop GetObjectByID(int id)
        {
            for (int i = 0; i < database.Count; i++)
            {
                if (database[i].ID == id)
                    return database[i];
            }

            return null;
        }
    }
}