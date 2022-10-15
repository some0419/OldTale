using UnityEngine;
using UnityEngine.UI;

public class SceneManager : SingletonMonoBehaviour<SceneManager>
{
    Animator fadeAnimator;

    void Awake()
    {
        base.Awake();

        //DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeAnimator = GetComponent<Animator>();
        FadeIn();
    }

    //釣りシーンへ（開発中）
    public void ToFishingScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Fishing");
    }

    //バトルシーンへ
    public void ToBattleScene()
    {
        /*yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));*/
        UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
    }

    //ホームシーンへ
    public void ToHomeScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Home");
    }

    //フェードインアニメーション起動
    public void FadeIn()
    {
        fadeAnimator.SetTrigger("FadeIn");
    }

    //フェードアウトアニメーション起動
    public void FadeOut()
    {
        fadeAnimator.SetTrigger("FadeOut");
    }
}
