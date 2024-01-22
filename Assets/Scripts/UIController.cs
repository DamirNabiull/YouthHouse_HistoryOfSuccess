using Models;
using Notifications;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, ISubscriber
{
    public GameObject startPage;
    
    public RawImage block1;
    public RawImage block2;

    public GameObject profilePage;
    public RawImage profileImage;

    public GameObject nextButton;
    public GameObject backButton;

    private int _currentPageId = 0;
    private int _maxPages = 0;

    private Profile _leftProfile;
    private Profile _rightProfile;
    
    private void Start()
    {
        ConfigReader.ReadConfig();
        ConfigReader.ReadImages();
        
        var config = ConfigReader.AppConfig;
        _maxPages = config.pages.Count;
        
        profilePage.SetActive(false);
        
        SetPage();
    }

    private void SetProfile()
    {
        var config = ConfigReader.AppConfig;
        var page = config.pages[_currentPageId];

        _leftProfile = ConfigReader.Profiles[page.idLeft];
        _rightProfile = ConfigReader.Profiles[page.idRight];
    }

    private void SetPage()
    {
        nextButton.SetActive(true);
        backButton.SetActive(true);
        
        if (_currentPageId == _maxPages - 1)
        {
            nextButton.SetActive(false);
        }
        if (_currentPageId == 0)
        {
            backButton.SetActive(false);
        }
        
        SetProfile();

        block1.texture = _leftProfile.PreviewImage;
        block2.texture = _rightProfile.PreviewImage;
    }

    public void SetNextPage()
    {
        _currentPageId++;
        SetPage();
    }
    
    public void SetPreviousPage()
    {
        _currentPageId--;
        SetPage();
    }

    public void ShowProfile(bool isLeft)
    {
        profileImage.texture = isLeft 
            ? _leftProfile.ProfileInfoImage 
            : _rightProfile.ProfileInfoImage;

        profilePage.SetActive(true);
    }
    
    public void HideProfile()
    {
        profilePage.SetActive(false);
    }

    void ISubscriber.MakeNotificationAction()
    {
        startPage.SetActive(true);
        
        _currentPageId = 0;
        SetPage();
        profilePage.SetActive(false);
    }
}
