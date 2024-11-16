using System;
using UnityEngine;

public class EndGameScreen : Window
{
    public event Action RestartButtonClicked;

    public override void Close()
    {
        WindowGroup.alpha = 0f;
        ActionButton.interactable = false;
    }

    public override void Open()
    { 
        WindowGroup.alpha = 1f;
        ActionButton.interactable = true;
    }

    public void BlockRaycast()
    {
        WindowGroup.blocksRaycasts = true;
    }

    protected override void OnButtonClick()
    {
        RestartButtonClicked?.Invoke();
    }
}
