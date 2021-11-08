using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelSelectionUIView : MonoBehaviour
{
    private LevelSelectionUIPresenter presenter;

    [SerializeField] private VisualTreeAsset levelTemplate;

    private Label levelSelectionLabel;
    private VisualElement levelsGridContainer;

    private List<VisualElement> currentLevels = new List<VisualElement>();
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        levelSelectionLabel = root.Q<Label>("LevelSelectionLabel");
        levelsGridContainer = root.Q<VisualElement>("LevelsGridContainer");

        levelSelectionLabel.text = Localisation.GetLocalisedValue("key_LevelSelectionLabel");

        presenter = new LevelSelectionUIPresenter(this);
        presenter.SearchLevels();
    }
    public void ShowLevelsGridView(LevelElementModel[] levelModels)
    {
        GenerateLevelsGridView(levelModels);
    }

    private void GenerateLevelsGridView(LevelElementModel[] levelModels)
    {
        for (int i = 0; i < levelModels.Length; i++)
        {
            VisualElement levelTemplateClone = levelTemplate.CloneTree();

            levelTemplateClone.Q<Label>("LevelLabel").text = Localisation.GetLocalisedValue(levelModels[i].levelLabel);
            levelTemplateClone.Q<Label>("LevelLockLabel").text = Localisation.GetLocalisedValue("key_LevelLockedLabel");
            levelTemplateClone.Q<VisualElement>("LevelImage").style.backgroundImage = Resources.Load<Texture2D>($"LevelSnapshots/{levelModels[i].levelImageURL}");
            int tempindex = i;//Passing i directly results in i = levelModels.Length for all buttons
            levelTemplateClone.Q<Button>("LevelButton").clicked += () => { presenter.HandleLevelClicked(levelTemplateClone, tempindex); };
            if (levelModels[i].locked)
                levelTemplateClone.Q<VisualElement>("LevelLock").style.display = DisplayStyle.Flex;

            currentLevels.Add(levelTemplateClone);
            levelsGridContainer.Add(levelTemplateClone);
        }
    }
}
