using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableObjectDatabase<T> : ScriptableObject
{
    [SerializeField]
    protected List<T> database = new List<T>();

    public void Add(T tObject)
    {
        database.Add(tObject);
    }

    public abstract T GetObjectByID(int id);

    public int GetLastIndex()
    {
        if (!(database.Count > 0))
        {
            return 0;
        }
        else
        {
            return database.Count;
        }
    }
    
}
