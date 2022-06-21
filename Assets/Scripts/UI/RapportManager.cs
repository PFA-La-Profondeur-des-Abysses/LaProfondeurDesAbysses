using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RapportManager : MonoBehaviour
{
    public static RapportManager rapportManager;
    
    public KeyCode openRapportKey; // touche qui ouvre le rapport
    [SerializeField] private GameObject rapport; // rapport
    [SerializeField] private List<GameObject> pages = new(); // liste des pages du rapport
    [SerializeField] private List<GameObject> tabs = new(); // liste des onglets dans le rapport
    [SerializeField] private Dictionary<GameObject, FishNames> fishPage = new(); // liste des onglets dans le rapport
    private bool on; // true si le rapport est ouvert, false sinon
    [SerializeField] private int nbPages; // le nombre de pages du rapport
    [SerializeField] private int currentPage; // l'id de la page ouverte actuellement
    private Animator animator;

    [Header("Visuals")]
    [SerializeField] private Sprite pageNotSelectedImage; // sprite de l'onglet non sélectionné
    [SerializeField] private Sprite pageSelectedImage; // sprite de l'onglet sélectionné

    void Awake()
    {
        
    }

    /*
     * Au start, créé le nombre de pages correspondant à la valeur de nbPages
     */
    void Start()
    {
        rapportManager = this;
        animator = GetComponent<Animator>();
        
        /*var child = rapport.transform;
        pages.Add(child.GetChild(0).gameObject);
        
        for (var i = 0; i < nbPages - 1; i++)
        {
            var newPage = Instantiate(child.GetChild(0).gameObject, child);
            newPage.name = "Page" + (i + 1);
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
            newTab.name = "Tab" + (i + 1);
            tabs.Add(newTab);
        }
        tabsParent.GetChild(2).SetSiblingIndex(tabsParent.childCount - 1);
        tab1.GetComponent<Image>().sprite = pageSelectedImage;*/

        /*Dictionary<FishNames, Fish> dico = new Dictionary<FishNames, Fish>
        {
            { FishNames.DosBleu, new Fish("DosBleu", false, false, false) },
            { FishNames.Sardine, new Fish("Sardine", false, false, false) },
            { FishNames.Poisson, new Fish("Poisson", false, false, false) },
            { FishNames.Requin, new Fish("Requin", false, false, false) }
        };
        
        string path = Application.persistentDataPath + "/fishStructure.json";
        File.WriteAllText(path, JsonConvert.SerializeObject(dico));
        Debug.Log("chargé" + path);
        var dico1 = JsonConvert.DeserializeObject<Dictionary<FishNames, Fish>>(File.ReadAllText(path));
        Debug.Log(dico1);
        Debug.Log(dico1?.Keys);
        Debug.Log(dico1?[FishNames.DosBleu]);
        Debug.Log(dico1?[FishNames.DosBleu].name);*/

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
            ToggleRapport(!on);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) ToggleRapport(false);
        
        if (!on) return; //EXIT si le rapport n'est pas ouvert
        
        if(Input.GetKeyDown(KeyCode.UpArrow)) ChangePage(-1);
        if(Input.GetKeyDown(KeyCode.DownArrow)) ChangePage(1);
    }

    /*
     * Fonction qui active/désactive le rapport et mets pause/play en fonction de son booléen en paramètre
     */
    public void ToggleRapport(bool open)
    {
        on = open;
        Time.timeScale = 1;
        //rapport.SetActive(on);
        animator.SetTrigger(on ? "Open" : "Close");
        //pages[currentPage].SetActive(on);
    }

    /*
     * Fonction qui permet de changer de page lorsque les flèches d'ui sont cliquées
     */
    public void ChangePage(int index)
    {
        ActivatePage((currentPage + index + nbPages) % nbPages);
    }
    
    public void SelectPage(GameObject obj)
    {
        ActivatePage(int.Parse(obj.name.Replace("Tab", "")) - 1);
    }

    public void ActivatePage(int pageNumber)
    {
        if(nbPages > 0)
        {
            if (pages.Count > currentPage) pages[currentPage].SetActive(false);
            if (tabs.Count > currentPage) tabs[currentPage].GetComponent<Image>().sprite = pageNotSelectedImage;
            currentPage = pageNumber % nbPages;
        }
        pages[currentPage].SetActive(true);
        tabs[currentPage].SetActive(true);
        tabs[currentPage].GetComponent<Image>().sprite = pageSelectedImage;
    }

    /*
     * Ferme la page actuelle
     */
    public void CloseCurrentPage()
    {
        Time.timeScale = 1;
        animator.SetTrigger("Close");
        //rapport.SetActive(false);
    }

    /*
     * Ouvre la page actuelle
     */
    public void OpenCurrentPage()
    {
        //Time.timeScale = 0;
        animator.SetTrigger("Open");
        //rapport.SetActive(true);
    }

    /*
     * Fonction qui retourne le slot de la photo de la page actuelle
     */
    public Image GetCurrentPageImage()
    {
        var images = pages[currentPage].transform.GetComponentsInChildren<Image>();
        return images.FirstOrDefault(image => image.name == "Photo");
    }

    private TMP_InputField GetName(int nb = -1)
    {
        if (nb == -1) nb = currentPage;
        return pages[currentPage].transform.Find("Name").GetComponent<TMP_InputField>();
    }

    private TMP_InputField GetNotes(int nb = -1)
    {
        if (nb == -1) nb = currentPage;
        return pages[currentPage].transform.Find("Notes").GetComponent<TMP_InputField>();
    }

    private TMP_Dropdown GetRegime(int nb = -1)
    {
        if (nb == -1) nb = currentPage;
        return pages[currentPage].transform.Find("Regime").GetComponent<TMP_Dropdown>();
    }
    
    private TextMeshProUGUI GetInfo(int nb = -1)
    {
        if (nb == -1) nb = currentPage;
        return pages[currentPage].transform.Find("Info").GetComponent<TextMeshProUGUI>();
    }

    /*
     * fonction qui affiche/désactive le petit "+" qui indique que la page est vide
     */
    public void FillPage()
    {
        var fill = GetCurrentPageImage().sprite ||
                   GetName().text != "" ||
                   GetNotes().text != "" ||
                   GetRegime().value != 0;
        tabs[currentPage].transform.GetChild(0).gameObject.SetActive(!fill);
        Debug.Log("fill");
    }

    /*
     * lance la fonction précédente lorsque le texte des InputFields (nom/description) est changé
     */
    public void OnChangeNameValue()
    {
        FillPage();
    }

    public void OnContentSelect(string text)
    {
        Time.timeScale = 0;
    }

    public void OnContentDeselect(string text)
    {
        Time.timeScale = 1;
    }

    public void AddPage(Fish fish)
    {
        nbPages++;
        
        var child = rapport.transform;
        
        var newPage = Instantiate(child.GetChild(0).gameObject, child);
        newPage.name = "Page" + nbPages;
        pages.Add(newPage);
        fishPage.Add(newPage, Fish.GetFishEnumFromFish(fish));
        
        var tabsParent = child.GetChild(child.childCount - 2);
        tabsParent.SetSiblingIndex(child.childCount - 1);
        
        var newTab = Instantiate(tabsParent.GetChild(1).gameObject, tabsParent);
        newTab.name = "Tab" + nbPages;
        tabs.Add(newTab);
        
        tabsParent.GetChild(tabsParent.childCount - 2).SetSiblingIndex(tabsParent.childCount - 1);
        
        ActivatePage(nbPages - 1);

        GetName().placeholder.GetComponent<TextMeshProUGUI>().text = fish.name;
    }

    public void FillInfo(Fish fish)
    {
        if (fishPage[pages[currentPage]] == Fish.GetFishEnumFromFish(fish))
        {
            GetInfo().text = "Size: " + fish.size + "\n" +
                             "Robe: " + fish.robe + "\n" +
                             "Weight: " + fish.weight + "\n";
        }
        else
        {
            GetInfo().text = "Size: " + "\n" +
                             "Robe: " + "\n" +
                             "Weight: " + "\n";
        }
    }
}
