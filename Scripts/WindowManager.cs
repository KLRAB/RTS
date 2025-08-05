using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : MonoBehaviour {
    [field: SerializeField] private GameObject menuState;
    [field: SerializeField] private GameObject settingsState;
    [field: SerializeField] private GameObject screenSettingsState;
    [field: SerializeField] private GameObject audioSettingsState;
    [field: SerializeField] private GameObject templarState;
    [field: SerializeField] private GameObject campaignState;

    [field: SerializeField] private GameObject historicState;
    [field: SerializeField] private GameObject builderState;
    [field: SerializeField] private GameObject multiState;
    [field: SerializeField] private GameObject scenarioState;
    [field: SerializeField] private GameObject bookState;

    [field: SerializeField] private GameObject loadState;
    [field: SerializeField] private GameObject gameSettingsState;
    [field: SerializeField] private GameObject characterState;

    [field: SerializeField] private GameObject freeState;

    public void OnBookClicked() {
        menuState.SetActive( false );
        bookState.SetActive( true );
    }

    public void OnScenarioClicked() {
        menuState.SetActive( false );
        scenarioState.SetActive( true );
    }

    public void OnMultiClicked() {
        menuState.SetActive( false );
        multiState.SetActive( true );
    }

    public void OnBuilderClicked() {
        menuState.SetActive( false );
        builderState.SetActive( true );
    }

    public void OnHistoricClicked() {
        menuState.SetActive( false );
        historicState.SetActive( true );
    }

    public void OnLoadClicked() {
        settingsState.SetActive( false );
        loadState.SetActive( true );
    }

    public void OnGameSettingsClicked() {
        settingsState.SetActive( false );
        gameSettingsState.SetActive( true );
    }

    public void OnCharacterClicked() {
        settingsState.SetActive( false );
        characterState.SetActive( true );
    }

    public void OnFreeClicked() {
        templarState.SetActive( false );
        freeState.SetActive( true );
    }

    public void OnBackToSettings() {
        if ( screenSettingsState.activeSelf ) screenSettingsState.SetActive( false );
        else if ( audioSettingsState.activeSelf ) audioSettingsState.SetActive( false );
        else if ( loadState.activeSelf ) loadState.SetActive( false );
        else if ( gameSettingsState.activeSelf ) gameSettingsState.SetActive( false );
        else if ( characterState.activeSelf ) characterState.SetActive( false );

        settingsState.SetActive( true );
    }

    public void OnBackToTemplar() {
        if ( campaignState.activeSelf ) campaignState.SetActive( false );
        else if ( freeState.activeSelf ) freeState.SetActive( false );

        templarState.SetActive( true );
    }

    public void OnBackToMenu() {
        if ( templarState.activeSelf ) templarState.SetActive( false );
        else if ( settingsState.activeSelf ) settingsState.SetActive( false );
        else if ( historicState.activeSelf ) historicState.SetActive( false );
        else if ( builderState.activeSelf ) builderState.SetActive( false );
        else if ( multiState.activeSelf ) multiState.SetActive( false );
        else if ( scenarioState.activeSelf ) scenarioState.SetActive( false );
        else if ( bookState.activeSelf ) bookState.SetActive( false );

        menuState.SetActive( true );
    }

    public void OnTemplarClicked() {
        menuState.SetActive( false );
        templarState.SetActive( true );
    }

    public void OnCampaignClicked() {
        templarState.SetActive( false );
        campaignState.SetActive( true );
    }
    
    public void OnSettingsClicked() {
        settingsState.SetActive( true );
        menuState.SetActive( false );
    }

    public void OnScreenSettingsClicked() {
        settingsState.SetActive( false );
        screenSettingsState.SetActive( true );
    }

    public void OnAudioSettingsClicked() {
        settingsState.SetActive( false );
        audioSettingsState.SetActive( true );
    }

    public void OnExitClicked() {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }


}
