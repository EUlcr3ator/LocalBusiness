using System.Collections.Generic;
using UnityEngine;

public interface IBusinessView
{
    void ShowBusinesses(List<Business> businesses);
    void ShowBusinessDetail(Business business);
    void ShowSaveResult(string message);
}