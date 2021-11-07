using UnityEngine;

public class GameController : MonoBehaviour
{
    private SelectableObject currentSelectedObject;

    void Update()
    {
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
