using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BusinessAdminView : BusinessView
{
    public TMP_InputField nameInput;
    public TMP_InputField descriptionInput;
    public TMP_InputField categoryInput;
    public TMP_InputField contactInput;
    public TMP_InputField hoursInput;
    public Button saveButton;
    public TMP_Text feedbackText;

    private BusinessPresenter presenter;

    void Start()
    {
        presenter = new BusinessPresenter(this);        
        saveButton.onClick.AddListener(OnSaveClick);
        presenter.LoadMyBusiness();
    }

    private void OnSaveClick()
    {
        var business = new Business
        {
            Name = nameInput.text,
            Description = descriptionInput.text,
            Category = categoryInput.text,
            ContactInfo = contactInput.text,
            WorkingHours = hoursInput.text,
            OwnerId = SessionManager.LoggedInUser.Id
        };

        presenter.SaveBusiness(business);
    }

    public override void ShowBusinessDetail(Business business)
    {
        if (business == null)
        {
            feedbackText.text = "No business profile found. Fill in the details to create one.";
            return;
        }

        nameInput.text = business.Name;
        descriptionInput.text = business.Description;
        categoryInput.text = business.Category;
        contactInput.text = business.ContactInfo;
        hoursInput.text = business.WorkingHours;
    }


    public override void ShowSaveResult(string message)
    {
        feedbackText.text = message;
    }

    public void LoadPromotionsScene()
    {
        SceneManager.LoadScene("PromotionsAdminScene");
    }
}
