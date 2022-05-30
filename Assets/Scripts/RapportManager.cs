using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RapportManager : MonoBehaviour
{
    public KeyCode openRapportKey;
    [SerializeField] private GameObject rapport;
    [SerializeField] private List<GameObject> pages = new();
    [SerializeField] private List<GameObject> tabs = new();
    private bool on;
    [SerializeField] private int nbPages;
    [SerializeField] private int currentPage;

    void Start()
    {
        var child = transform.GetChild(0);
        pages.Add(child.GetChild(0).gameObject);
        
        for (var i = 0; i < nbPages - 1; i++)
        {
            var newPage = Instantiate(child.GetChild(0).gameObject, child);
            pages.Add(newPage);
        }
        
        var tabsParent = child.GetChild(1);
        tabsParent.SetSiblingIndex(child.childCount - 1);
        GameObject tab1 = tabsParent.GetChild(1).gameObject;
        tabs.Add(tab1);
        
        for (var i = 0; i < nbPages - 1; i++)
        {
            var newTab = Instantiate(tabsParent.GetChild(1).gameObject, tabsParent);
            tabs.Add(newTab);
        }
        tabsParent.GetChild(2).SetSiblingIndex(tabsParent.childCount - 1);
        tab1.GetComponent<Image>().color = Color.grey;
    }
    
    void Update()
    {
        if (Input.GetKeyDown(openRapportKey))
        {
            on = !rapport.activeSelf;
            Time.timeScale = on ? 0 : 1;
            rapport.SetActive(on);
            pages[currentPage].SetActive(on);
        }

        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        on = false;
        Time.timeScale = 1;
        rapport.SetActive(false);
        pages[currentPage].SetActive(false);
    }

    public void ChangePage(int index)
    {
        currentPage = (currentPage + index + nbPages) % nbPages;
        foreach (var page in pages)
        {
            page.SetActive(page == pages[currentPage]);
        }
        foreach (var tab in tabs)
        {
            tab.GetComponent<Image>().color = tab == tabs[currentPage] ? Color.grey : Color.white;
        }
        tabs[currentPage].GetComponent<Image>().color = Color.grey;
    }

    public void CloseCurrentPage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void OpenCurrentPage()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public Image GetCurrentPageImage()
    {
        var images = pages[currentPage].transform.GetComponentsInChildren<Image>();
        return images.FirstOrDefault(image => image.name == "Photo");
    }
}
