using UnityEngine;
using UnityEngine.UIElements;

public class SplashScreenUIView : MonoBehaviour
{
    private VisualElement splashContainer;
    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        splashContainer = root.Q<VisualElement>("SplashContainer");
        splashContainer.RegisterCallback<MouseDownEvent>(OnSplashScreenClicked);
    }

    private void OnSplashScreenClicked(MouseDownEvent evt)
    {
        NavigationPhaseController.NextNavigationPhase();
    }
}
