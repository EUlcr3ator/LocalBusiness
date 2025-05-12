using SQLite;
using System.Collections.Generic;
using System.Linq;

public class PromotionService
{
    private readonly SQLiteConnection _connection;

    public PromotionService()
    {
        _connection = SQLiteService.GetConnection();
        _connection.CreateTable<Promotions>();
    }

    public void Save(Promotions promo)
    {
        var existing = _connection.Table<Promotions>().FirstOrDefault(p => p.Id == promo.Id);
        if (existing == null)
            _connection.Insert(promo);
        else
        {
            promo.Id = existing.Id;
            _connection.Update(promo);
        }
    }

    public List<Promotions> GetByBusiness(int businessId)
    {
        return _connection.Table<Promotions>()
                          .Where(p => p.Id == businessId)
                          .ToList();
    }
}
