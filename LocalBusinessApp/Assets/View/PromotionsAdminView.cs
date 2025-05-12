using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class PromotionsAdminView : MonoBehaviour, IPromotionsView
{
    public TMP_InputField titleInput;
    public TMP_InputField descriptionInput;
    public TMP_InputField startDateInput;
    public TMP_InputField endDateInput;
    public Button saveButton;
    public TMP_Text feedbackText;

    private PromotionsPresenter presenter;

    void Start()
    {
        presenter = new PromotionsPresenter(this);
        saveButton.onClick.AddListener(OnSaveClick);

        // Load existing promotion
        var promos = presenter.GetPromotions(SessionManager.LoggedInUser.Id);
        if (promos.Count > 0)
        {
            var promo = promos[0]; // Just load the first one for now

            titleInput.text = promo.Title;
            descriptionInput.text = promo.Description;
            startDateInput.text = promo.StartDate.ToString("yyyy-MM-dd");
            endDateInput.text = promo.EndDate.ToString("yyyy-MM-dd");
        }
    }


    private void OnSaveClick()
    {
        var promo = new Promotions
        {
            BusinessId = SessionManager.LoggedInUser.Id,
            Title = titleInput.text,
            Description = descriptionInput.text,
            StartDate = DateTime.Parse(startDateInput.text),
            EndDate = DateTime.Parse(endDateInput.text)
        };

        presenter.SavePromotion(promo);
    }

    public void ShowSaveResult(string message)
    {
        feedbackText.text = message;
    }

    public void BackToBusinessPage()
    {
        SceneManager.LoadScene("BusinessAdminScene");
    }
}
