using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviourSingleton<GameController>
{
    private int levelIndex;
    private float playerScore = 0;
    private int playerHealth = 3;
    private string guessObjectStingMessage = "";

    public UnityEvent<int> OnHealthChanged = new UnityEvent<int>();
    public UnityEvent<float> OnPlayerScoreChanged = new UnityEvent<float>();
    public UnityEvent<float> OnTimerChanged = new UnityEvent<float>();

    public delegate void AnswerCallback(string message);
    public event AnswerCallback OnGuessMessageChanged;

    private SelectableObject currentSelectedObject;
    private ObjectData computerSelectedObjectData;

    [SerializeField] private ObjectData[] levelOneSelectableObjects;
    [SerializeField] private ObjectData[] levelTwoSelectableObjects;

    [SerializeField] private GameObject[] LevelScenes;

    private ObjectData GetRandomObjectData()
    {
        int randomObjectIndex = -1;
        switch (levelIndex)
        {
            case 0:
                randomObjectIndex = Random.Range(0, levelOneSelectableObjects.Length);
                return levelOneSelectableObjects[randomObjectIndex];
            case 1:
                randomObjectIndex = Random.Range(0, levelTwoSelectableObjects.Length);
                return levelTwoSelectableObjects[randomObjectIndex];
            default:
                return new ObjectData();
        }
    }
    private string FormatObjectName(string objectName)
    {
        //Create placeholders for each letters
        string[] formattedNameArray = new string[objectName.Length];

        //Always show first character
        formattedNameArray[0] = $" {objectName[0]} ";

        //Set Placeholders in array
        for (int i = 1; i < formattedNameArray.Length; i++)
        {
            formattedNameArray[i] = " __";
        }

        //Show another letter if the object name is long enough
        //Skip second letter and choose randomly... Modify based on requirements
        if (formattedNameArray.Length > 3)
        {
            int randomLetterIndex = Random.Range(2, objectName.Length);
            formattedNameArray[randomLetterIndex] = $" {objectName[randomLetterIndex]} ";
        }

        return string.Join("", formattedNameArray);
    }
    public void StartLevel(int levelIndex, float duration)
    {
        this.levelIndex = levelIndex;
        LevelScenes[levelIndex].SetActive(true);
        StartCoroutine(StartTimedLevel(duration));
    }
    private IEnumerator StartTimedLevel(float duration)
    {
        yield return new WaitForSeconds(1);

        PickRandomObject();

        CoroutineWithData coroutineReturnData = new CoroutineWithData(this, Helpers.CountdownTimer(duration));

        while ((float)coroutineReturnData.result > 0)
        {
            OnTimerChanged.Invoke((float)coroutineReturnData.result);

            CheckIfSceneObjectSelected();

            yield return null;
        }
    }
    public void ValidateSelection()
    {
        Debug.Log("VALIDATE");
        //Validation
        if (!currentSelectedObject) return;

        if (currentSelectedObject.selectableObjectData == computerSelectedObjectData)
        {
            //Increase score
            playerScore += computerSelectedObjectData.rewardPoints;

            //Get new object
            PickRandomObject();

            //update ui
            OnPlayerScoreChanged.Invoke(playerScore);
        }
        else
        {
            //Decrease score
            playerScore = playerScore - computerSelectedObjectData.rewardPoints >= 0 ? playerScore - computerSelectedObjectData.rewardPoints : 0;

            //Decrease health points
            playerHealth--;

            //update ui
            OnPlayerScoreChanged.Invoke(playerScore);
            OnHealthChanged.Invoke(playerHealth);
        }
    }
    private void PickRandomObject()
    {
        computerSelectedObjectData = GetRandomObjectData();
        guessObjectStingMessage = FormatObjectName(Localisation.GetLocalisedValue(computerSelectedObjectData.objectName));

        if (OnGuessMessageChanged != null)
            OnGuessMessageChanged(guessObjectStingMessage);

        Debug.Log(guessObjectStingMessage);
    }
    private void CheckIfSceneObjectSelected()
    {
        if (Input.mousePosition.y < 130) return;//HACK new UI system not blocking raycasts.. Dirty hack to save time
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 200.0f))
            {
                SelectableObject selectedObject = hit.collider.gameObject.GetComponent<SelectableObject>();
                if (selectedObject != null)
                {
                    if (currentSelectedObject != null)
                        currentSelectedObject.ToggleOutline(false);

                    currentSelectedObject = selectedObject;
                    selectedObject.ToggleOutline(true);
                    Debug.Log("Hit: " + Localisation.GetLocalisedValue(currentSelectedObject.selectableObjectData.objectName));
                }
            }
        }
    }
}
