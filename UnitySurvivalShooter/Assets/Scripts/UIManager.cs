using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject hitEffectPanel;
    public GameObject pauseUI;
    public bool IsPause { get; private set; } = false;
    // Start is called before the first frame update
    void Start()
    {
        hitEffectPanel.SetActive(false);
        pauseUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            IsPause = !IsPause;

            Time.timeScale = IsPause ? 0.0f : 1.0f;
            pauseUI.SetActive(IsPause);
        }
    }
    public IEnumerator HitEffect()
    {
        hitEffectPanel.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        hitEffectPanel.SetActive(false);
    }
}
