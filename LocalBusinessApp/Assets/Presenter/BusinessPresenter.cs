using UnityEngine;

public class BusinessPresenter
{
    private readonly IBusinessView _view;
    private readonly BusinessService _service;

    public BusinessPresenter(IBusinessView view)
    {
        _view = view;
        _service = new BusinessService();
    }

    // For general list (citizens)
    public void LoadBusinesses() =>
        _view.ShowBusinesses(_service.GetAll());

    public void SearchBusinesses(string keyword) =>
        _view.ShowBusinesses(_service.Search(keyword));

    // For business owner (admin view)
    public void LoadMyBusiness()
    {
        int ownerId = SessionManager.LoggedInUser.Id;
        var business = _service.GetBusinessByOwner(ownerId);
        _view.ShowBusinessDetail(business);
    }

    public void SaveBusiness(Business business)
    {
        business.OwnerId = SessionManager.LoggedInUser.Id;
        _service.SaveOrUpdateBusiness(business);
        _view.ShowSaveResult("Business saved!");
    }
}
