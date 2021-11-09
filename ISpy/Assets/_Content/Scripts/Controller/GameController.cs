using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviourSingleton<GameController>
{
    private bool isGameOver;
    private int levelIndex;
    private float playerScore;
    private int playerHealth;
    private string guessObjectStingMessage;
    private Coroutine levelRoutine;

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

    [SerializeField] private Transform[] levelCamerasPositions;

    public void StartLevel(int levelIndex, float duration)
    {
        this.levelIndex = levelIndex;
        isGameOver = false;
        playerScore = 0;
        playerHealth = 3;
        guessObjectStingMessage = "";

        for (int i = 0; i < LevelScenes.Length; i++)
        {
            if(i == this.levelIndex)
                LevelScenes[i].SetActive(true);
            else
                LevelScenes[i].SetActive(false);
        }
        
        Camera.main.transform.position = levelCamerasPositions[levelIndex].position;
        Camera.main.transform.rotation = levelCamerasPositions[levelIndex].rotation;

        OnHealthChanged.AddListener(HealthChangeCheck);
        OnTimerChanged.AddListener(TimerChangeCheck);

        levelRoutine = StartCoroutine(StartTimedLevel(duration));
    }
    private IEnumerator StartTimedLevel(float duration)
    {
        yield return new WaitForSeconds(1);

        PickRandomObject();

        CoroutineWithData coroutineReturnData = new CoroutineWithData(this, Helpers.CountdownTimer(duration));

        while ((float)coroutineReturnData.result > 0)
        {
            OnTimerChanged.Invoke((float)coroutineReturnData.result);

            UpdateSelectedObject();
            UpdateCameraRotation();

            yield return null;
        }
        levelRoutine = null;
    }
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
        currentSelectedObject.ToggleOutline(false);
        currentSelectedObject = null;
    }
    private void PickRandomObject()
    {
        computerSelectedObjectData = GetRandomObjectData();
        guessObjectStingMessage = FormatObjectName(Localisation.GetLocalisedValue(computerSelectedObjectData.objectName));

        if (OnGuessMessageChanged != null)
            OnGuessMessageChanged(guessObjectStingMessage);

        Debug.Log(guessObjectStingMessage);
    }
    private void UpdateSelectedObject()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
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

    private void TimerChangeCheck(float arg0)
    {
        if (arg0 <= 0.1f)
        {
            EndGame();
        }
    }
    private void HealthChangeCheck(int arg0)
    {
        if (arg0 < 0)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("Game Over!");
            StopCoroutine(levelRoutine);
            levelRoutine = null;
            PlayerPrefs.SetFloat("CurrentScore", playerScore);
            if (playerScore > PlayerPrefs.GetFloat("HighScore"))
                PlayerPrefs.SetFloat("HighScore", playerScore);

            OnHealthChanged.RemoveListener(HealthChangeCheck);
            OnTimerChanged.RemoveListener(TimerChangeCheck);

            NavigationPhaseController.NextNavigationPhase();
        }
    }

    private void UpdateCameraRotation()
    {
        if (Input.GetMouseButton(1))
        {
            Vector3 rotateValue = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X") * -1, 0);
            Camera.main.transform.eulerAngles = Camera.main.transform.eulerAngles - rotateValue;
        }
    }
}

