using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BusinessListView : BusinessView
{
    public TMP_InputField searchInput;
    public Button searchButton;
    public Transform listContainer;
    public GameObject businessItemPrefab;

    private BusinessPresenter presenter;

    void Start()
    {
        presenter = new BusinessPresenter(this);
        searchButton.onClick.AddListener(OnSearchClicked);
        presenter.LoadBusinesses();
    }

    private void OnSearchClicked()
    {
        string keyword = searchInput.text;
        presenter.SearchBusinesses(keyword);
    }

    public override void ShowBusinesses(List<Business> businesses)
    {
        foreach (Transform child in listContainer)
            Destroy(child.gameObject);

        foreach (var business in businesses)
        {
            var item = Instantiate(businessItemPrefab, listContainer);
            item.transform.Find("NameText").GetComponent<TMP_Text>().text = business.Name;
            item.transform.Find("Description").GetComponent<TMP_Text>().text = business.Description;

            var detailsButton = item.transform.Find("Details").GetComponent<Button>();
            int id = business.Id;
            detailsButton.onClick.AddListener(() => ShowBusinessDetails(id));
        }
    }

    private void ShowBusinessDetails(int id)
    {
        PlayerPrefs.SetInt("SelectedBusinessId", id);
        UnityEngine.SceneManagement.SceneManager.LoadScene("BusinessDetailScene");
    }
}
