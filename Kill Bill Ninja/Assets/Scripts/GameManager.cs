using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public Image fadeImage;
    public Text playerNameText;

    private Blade blade;
    private Spawner spawner;

    private int score;
    private string playerName;

    [SerializeField] public IndirectionLogin indirectionLogin;

    private void Awake()
    {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<Spawner>();
        playerName = getPlayerName();
    }

    private string getPlayerName(){
        string name = "";
        if(indirectionLogin==null){
            indirectionLogin = FindObjectOfType<IndirectionLogin>();
        }
        if(indirectionLogin!=null){
            name = indirectionLogin.getPlayerName();
            Destroy(indirectionLogin.gameObject);
        }
        return name;
    }

    private void NewGame(){
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();
        playerNameText.text = playerName;

        ClearScene();

    }

    private void ClearScene()
    {
        Frutto[] frutti = FindObjectsOfType<Frutto>();

        foreach (Frutto frutto in frutti) {
            Destroy(frutto.gameObject);
        }

        Bomb[] bombs = FindObjectsOfType<Bomb>();

        foreach (Bomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }

    private  void Start(){
        NewGame();
    }


    public void IncreaseScore(int amount)
    {
        score += amount;
        scoreText.text = score.ToString();

    }

    public void Explode()
    {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
