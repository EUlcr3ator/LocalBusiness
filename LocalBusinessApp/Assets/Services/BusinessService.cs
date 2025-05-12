using SQLite;
using System.Collections.Generic;
using UnityEngine;

public class BusinessService
{
    private SQLiteConnection db => SQLiteService.GetConnection();

    public List<Business> GetAll() => db.Table<Business>().ToList();

    public List<Business> Search(string keyword) =>
        db.Table<Business>()
          .Where(b => b.Name.ToLower().Contains(keyword.ToLower()) || b.Category.ToLower().Contains(keyword.ToLower()))
          .ToList();

    public void SaveOrUpdateBusiness(Business business)
    {
        var existing = db.Table<Business>().FirstOrDefault(b => b.OwnerId == business.OwnerId);
        if (existing != null)
        {
            business.Id = existing.Id;
            db.Update(business);
        }
        else
        {
            db.Insert(business);
        }
    }

    public Business GetBusinessByOwner(int ownerId)
    {
        return db.Table<Business>().FirstOrDefault(b => b.OwnerId == ownerId);
    }
    public Business GetBusinessById(int businessId)
    {
        return db.Table<Business>().FirstOrDefault(b => b.Id == businessId);
    }

    public List<Business> GetAllBusinesses()
    {
        return db.Table<Business>().ToList();
    }
}
