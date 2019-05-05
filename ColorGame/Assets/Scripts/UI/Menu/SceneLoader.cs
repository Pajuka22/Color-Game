using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneLoader : ButtonParent
{
    public string toLoad;
    public float animTime = 0.5f;
    // Start is called before the first frame update
    public override void Select()
    {
        base.Select();
        StartCoroutine(loadScene(animTime));
    }
    public IEnumerator loadScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("Scenes/no_idea");
    }
}
