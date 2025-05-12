using System.Collections.Generic;
using UnityEngine;

public class PromotionsPresenter
{
    private readonly IPromotionsView _view;
    private readonly PromotionService _service;

    public PromotionsPresenter(IPromotionsView view)
    {
        _view = view;
        _service = new PromotionService();
    }

    public void SavePromotion(Promotions promo)
    {
        _service.Save(promo);
        _view.ShowSaveResult("Promotion saved successfully!");
    }

    // Optional: for viewing promotions later
    public List<Promotions> GetPromotions(int businessId) =>
        _service.GetByBusiness(businessId);
}

