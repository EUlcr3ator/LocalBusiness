using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BusinessDetailView : BusinessView
{
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text categoryText;
    public TMP_Text contactText;
    public TMP_Text hoursText;
    public Button backButton;

    public Transform promotionsContainer;
    public GameObject promotionItemPrefab;

    private BusinessPresenter presenter;

    void Start()
    {
        presenter = new BusinessPresenter(this);
        backButton.onClick.AddListener(() =>
            UnityEngine.SceneManagement.SceneManager.LoadScene("BusinessesScene"));

        int businessId = PlayerPrefs.GetInt("SelectedBusinessId", -1);
        if (businessId != -1)
        {
            var business = new BusinessService().GetBusinessById(businessId);
            ShowBusinessDetail(business);
        }
    }

    public override void ShowBusinessDetail(Business business)
    {
        nameText.text = business.Name;
        descriptionText.text = business.Description;
        categoryText.text = "Category: " + business.Category;
        contactText.text = "Contact: " + business.ContactInfo;
        hoursText.text = "Working Hours: " + business.WorkingHours;

        // Clear previous promotions
        foreach (Transform child in promotionsContainer)
            Destroy(child.gameObject);

        // Load promotions for this business
        var promotions = new PromotionService().GetByBusiness(business.Id);

        foreach (var promo in promotions)
        {
            var item = Instantiate(promotionItemPrefab, promotionsContainer);
            item.GetComponent<TMP_Text>().text =
                $"{promo.Title}\n{promo.Description}\n{promo.StartDate:dd.MM} - {promo.EndDate:dd.MM}";
        }
    }
}
