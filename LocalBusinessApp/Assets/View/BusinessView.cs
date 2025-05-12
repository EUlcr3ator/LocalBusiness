using System.Collections.Generic;
using UnityEngine;

public class BusinessView : MonoBehaviour, IBusinessView
{
    public virtual void ShowBusinesses(List<Business> businesses)
    {
        Debug.Log("Business list loaded: " + businesses.Count);
    }

    public virtual void ShowBusinessDetail(Business business)
    {
        Debug.Log("Showing details for: " + business.Name);
    }

    public virtual void ShowSaveResult(string message)
    {
        Debug.Log("Save result: " + message);
    }
}
