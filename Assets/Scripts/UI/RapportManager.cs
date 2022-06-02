using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RapportManager : MonoBehaviour
{
    public KeyCode openRapportKey; // touche qui ouvre le rapport
    [SerializeField] private GameObject rapport; // rapport
    [SerializeField] private List<GameObject> pages = new(); // liste des pages du rapport
    [SerializeField] private List<GameObject> tabs = new(); // liste des onglets dans le rapport
    private bool on; // true si le rapport est ouvert, false sinon
    [SerializeField] private int nbPages; // le nombre de pages du rapport
    [SerializeField] private int currentPage; // l'id de la page ouverte actuellement

    [Header("Visuals")]
    [SerializeField] private Sprite pageNotSelectedImage; // sprite de l'onglet non sélectionné
    [SerializeField] private Sprite pageSelectedImage; // sprite de l'onglet sélectionné

    /*
     * Au start, créé le nombre de pages correspondant à la valeur de nbPages
     */
    void Start()
    {
        var child = transform.GetChild(0);
        pages.Add(child.GetChild(0).gameObject);
        
        for (var i = 0; i < nbPages - 1; i++)
        {
            var newPage = Instantiate(child.GetChild(0).gameObject, child);
            pages.Add(newPage);
            newPage.SetActive(false);
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
        tab1.GetComponent<Image>().sprite = pageSelectedImage;
    }
    
    /*
     * A chaque frame:
     * si la touche du rapport est cliquée, ouvre le rapport si il est déjà ouvert, le ferme sinon
     * si la touche echap est cliquée, ferme le rapport
     */
    void Update()
    {
        if (Input.GetKeyDown(openRapportKey))
        {
            ToggleRapport(!rapport.activeSelf);
        }

        if (!Input.GetKeyDown(KeyCode.Escape)) return;
        
        ToggleRapport(false);
    }

    /*
     * Fonction qui active/désactive le rapport et mets pause/play en fonction de son booléen en paramètre
     */
    public void ToggleRapport(bool open)
    {
        on = open;
        Time.timeScale = on ? 0 : 1;
        rapport.SetActive(on);
        pages[currentPage].SetActive(on);
    }

    /*
     * Fonction qui permet de changer de page lorsque les flèches d'ui sont cliquées
     */
    public void ChangePage(int index)
    {
        currentPage = (currentPage + index + nbPages) % nbPages;
        foreach (var page in pages)
        {
            page.SetActive(page == pages[currentPage]);
        }
        foreach (var tab in tabs)
        {
            tab.GetComponent<Image>().sprite = tab == tabs[currentPage] ? pageSelectedImage : pageNotSelectedImage;
        }
    }

    /*
     * Ferme la page actuelle
     */
    public void CloseCurrentPage()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    /*
     * Ouvre la page actuelle
     */
    public void OpenCurrentPage()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    /*
     * Fonction qui retourne le slot de la photo de la page actuelle
     */
    public Image GetCurrentPageImage()
    {
        var images = pages[currentPage].transform.GetComponentsInChildren<Image>();
        return images.FirstOrDefault(image => image.name == "Photo");
    }

    /*
     * fonction qui affiche/désactive le petit "+" qui indique que la page est vide
     */
    public void FillPage()
    {
        var pageTransform = pages[currentPage].transform;
        var fill = pageTransform.Find("Photo").GetComponent<Image>().sprite ||
                   pageTransform.Find("Name").GetComponent<TMP_InputField>().text != "" ||
                   pageTransform.Find("Notes").GetComponent<TMP_InputField>().text != "" ||
                   pageTransform.Find("Regime").GetComponent<TMP_Dropdown>().value != 0;
        tabs[currentPage].transform.GetChild(0).gameObject.SetActive(!fill);
    }

    /*
     * lance la fonction précédente lorsque le texte des InputFields (nom/description) est changé
     */
    public void OnChangeNameValue()
    {
        FillPage();
    }
}
