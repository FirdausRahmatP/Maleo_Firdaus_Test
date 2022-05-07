using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveUI : MonoBehaviour
{
    private Text text;
    private void OnEnable()
    {
        Spawner.Instance.onWaveStarted.AddListener(OnWaveStarted);
    }
    private void OnDisable()
    {
        Spawner.Instance.onWaveStarted.RemoveListener(OnWaveStarted);
    }

    public void OnWaveStarted(Wave wave)
    {
        if (text == null)
        {
            text = GetComponent<Text>();
        }
        text.text = wave.name;
    }
}
