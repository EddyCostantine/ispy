using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectionUIPresenter : MonoBehaviour
{
    private LevelSelectionUIView view;
    private LevelElementsData levelsData;
    public LevelSelectionUIPresenter(LevelSelectionUIView view)
    {
        this.view = view;
    }

    internal void HandleLevelClicked(VisualElement levelTemplateClone, int index)
    {
        Debug.Log("Go to level: " + index);
        PlayerPrefs.SetInt("desiredLevel", index);
        NavigationPhaseController.NextNavigationPhase();
    }

    public void SearchLevels()
    {
        if (levelsData == null)
        {
            //TODO call API
            var jsonTextFile = Resources.Load<TextAsset>("LevelsJSON");
            levelsData = JsonUtility.FromJson<LevelElementsData>(jsonTextFile.text);
        }

        view.ShowLevelsGridView(levelsData.levels);
    }
}
