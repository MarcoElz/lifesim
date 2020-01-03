using LifeSim.Farming;
using System.Collections.Generic;
using UnityEngine;

public class FarmLand : MonoBehaviour 
{

    [SerializeField] GameObject soilPrefab;
    [SerializeField] CropDataBase cropDB;
    private List<SoilData> fobj;

    private void Awake()
	{
        FarmData data;
        if (GameManager.Instance != null)
        {
             data = GameManager.Instance.GetActualData();
        }
        else
        {
            Debug.LogWarning("No existe Game Manager");
            return;
        }

        fobj = data.farmObjects;
        for (int i = 0; i < fobj.Count; i++)
        {
            Vector3 pos = fobj[i].position.ToVector3();
            pos.y = 0f;
            GameObject s = Instantiate(soilPrefab, pos, Quaternion.identity) as GameObject;
            s.transform.SetParent(this.transform);
            Crop crop = cropDB.GetObjectByID(fobj[i].cropid);
            if (crop != null)
                s.GetComponentInChildren<Soil>().SetSoilData(fobj[i].lives, fobj[i].days, cropDB.GetObjectByID(fobj[i].cropid), fobj[i].wasWatered);
        }
    }

    public void ReSaveLand()
    {
        List <SoilData> newSoil = new List<SoilData>();

        for (int i = 0; i < FarmObject.farmObjects.Count; i++)
        {
            newSoil.Add(((Soil)FarmObject.farmObjects[i]).GetSoilData());
            Debug.Log("watered? " + newSoil[0].wasWatered);
        }
        if(GameManager.Instance != null)
            GameManager.Instance.SetNewFarmObjects(newSoil);
    }
}
