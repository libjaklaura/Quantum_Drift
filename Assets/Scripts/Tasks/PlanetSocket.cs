using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlanetSocket : XRSocketInteractor
{
    public string expectedPlanetName;
    public PlanetTask planetTask;

    private bool isCorrect = false;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        string placedPlanet = args.interactableObject.transform.name;

        if (placedPlanet == expectedPlanetName && !isCorrect)
        {
            isCorrect = true;
            planetTask.IncrementCorrect();
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        string removedPlanet = args.interactableObject.transform.name;

        if (removedPlanet == expectedPlanetName && isCorrect)
        {
            isCorrect = false;
            planetTask.DecrementCorrect();
        }
    }
}
