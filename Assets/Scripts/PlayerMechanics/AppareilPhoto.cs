using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class AppareilPhoto : MonoBehaviour
{
    [SerializeField] private KeyCode startPhotoModeKey; // Touche qui sera utilisée pour lancer le mode photo
    [SerializeField] private KeyCode takePictureKey; // Touche qui sera utilisée pour prendre une photo
    private Camera cam; // Variable stockant la caméra qui fera le screen
    [SerializeField] private RapportManager rapport; // Objet rapport

    public Image picture; // L'image utilisée pour l'animation

    private bool modeOn; // booléen correspondant à l'état du mode Appareil Photo

    void Awake()
    {
        cam = transform.GetChild(0).GetComponent<Camera>(); // Assigne la caméra qui prendra la photo à la variable cam
    }

    /*
     * A chaque frame:
     * si le temps est en pause, calcule la position de la souris dans le monde et l'assigne à la caméra qui prendra le screen
     * si la touche echap OU tab est cliquée ET que la caméra est activée (la caméra n'est active que quand le mode photo est lancé), sort du mode photo et réactive le rapport
     * si la touche du screen est cliquée ET que la caméra est activée, lance la fonction qui prends un screenshot
     */
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Escape) || 
             Input.GetKeyDown(KeyCode.Tab)) && cam.gameObject.activeSelf)
        {
            ClosePhotoMode();
            rapport.ToggleRapport(true);
        }
        
        if (Input.GetKeyDown(takePictureKey) && cam.gameObject.activeSelf)
        {
            cam.targetTexture = RenderTexture.GetTemporary(400, 400, 16);
            StartCoroutine(TakeScreenshot());
        }
        
        if (Time.timeScale == 1) return;
        
        var position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(position.x, position.y, 0);
    }

    /*
     * Lorsqu'on clique sur le bouton d'appareil photo, ferme le rapport et lance le mode photo
     */
    public void onClick()
    {
        modeOn = true;
        cam.gameObject.SetActive(modeOn);
        rapport.CloseCurrentPage();
    }

    /*
     * Ferme le mode photo et ouvre le rapport
     */
    public void ClosePhotoMode()
    {
        modeOn = false;
        cam.gameObject.SetActive(modeOn);
        rapport.OpenCurrentPage();
    }

    /*
     * Fonction qui prends la photo
     *
     * Créé une RenderTexture, l'assigne à la camera (les pixels vus par la camera seront retransmis sur cette texture),
     * puis va chercher le rendu de la texture, et l'enregistre sur un sprite dans les dossiers.
     * Assigne ce sprite à l'image dans le rapport, puis lance l'animation de prise de photo
     */
    private IEnumerator TakeScreenshot()
    {
        yield return new WaitForEndOfFrame();

        RenderTexture renderTexture = cam.targetTexture;
        RenderTexture.active = renderTexture;

        Texture2D renderResult =
            new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
        Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
        renderResult.ReadPixels(rect, 0, 0);

        byte[] byteArray = renderResult.EncodeToPNG();
        File.WriteAllBytes(Application.dataPath + "/test.png", byteArray);

        RenderTexture.ReleaseTemporary(renderTexture);
        cam.targetTexture = null;

        Texture2D texture = new Texture2D(400, 400);
        bool isLoaded = texture.LoadImage(byteArray);
        if(isLoaded)
        {
            Sprite sprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
            //picture.sprite = sprite;
            //rapport.GetCurrentPageImage().sprite = sprite;
            string path = Application.persistentDataPath + "/fishStructure.json";
            File.WriteAllText(path, JsonConvert.SerializeObject(new Page("DosBleu", 
                FeedingRegime.Inconnu, "", sprite)));

            string str = File.ReadAllText(path);
            Page page = JsonConvert.DeserializeObject<Page>(str);
            sprite = page.picture;
            yield return null;
            picture.sprite = sprite;
            rapport.GetCurrentPageImage().sprite = sprite;
        }
        cam.gameObject.SetActive(false);
        GetComponent<Animator>().SetTrigger("TakePicture");
    }

    /*
     * Fonction lancée à la fin de l'animation
     * réactive le rapport et désactive le mode photo
     */
    private void ReturnToNormalMode()
    {
        rapport.transform.GetChild(0).gameObject.SetActive(true);
        rapport.OpenCurrentPage();
        rapport.FillPage();
        picture.gameObject.SetActive(false);
    }
}
