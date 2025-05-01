using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : BaseUI
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private Slider playerHpSlider;
    [SerializeField] private Slider castleHpSlider;

    private void Start()
    {
        UpdatePlayerHPSlider(1);
        UpdateCastleHPSlider(1);
    }
    
    public void UpdatePlayerHPSlider(float percentage)
    {
        playerHpSlider.value = percentage;
    }
    
    public void UpdateCastleHPSlider(float percentage)
    {
        castleHpSlider.value = percentage;
    }

    public void UpdateWaveText(int wave)
    {
        waveText.text = wave.ToString();
    }

    protected override UIState GetUIState()
    {
        return UIState.Game;
    }
}