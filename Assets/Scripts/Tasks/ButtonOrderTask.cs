using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ButtonOrderTask : Task
{
    public Transform buttonParent;
    private List<Button> buttons;
    private List<Button> shuffledButtons;
    private int counter = 0;

    public void Start()
    {
        buttons = buttonParent.GetComponentsInChildren<Button>().ToList();
        RestartTheGame();
    }

    public void RestartTheGame()
    {
        counter = 0;

        shuffledButtons = buttons.OrderBy(b => Random.Range(0, 100)).ToList();

        for (int i = 0; i < shuffledButtons.Count; i++)
        {
            shuffledButtons[i].transform.SetSiblingIndex(i);
            shuffledButtons[i].interactable = true;
            shuffledButtons[i].image.color = new Color32(177, 220, 233, 255);
        }
    }

    public void pressButton(Button button)
    {
        int selectedOrder = buttons.IndexOf(button);

        if (selectedOrder == counter)
        {
            counter++;
            button.interactable = false;
            button.image.color = Color.yellow;

            if (counter == buttons.Count)
            {
                StartCoroutine(presentResult(true));
            }
        }
        else
        {
            StartCoroutine(presentResult(false));
        }
    }

    public IEnumerator presentResult(bool win)
    {
        if (!win)
        {
            foreach (var button in buttons)
            {
                button.image.color = Color.red;
                button.interactable = false;
            }

            yield return new WaitForSeconds(2f);
            RestartTheGame();
        }
        else
        {
            foreach (var button in buttons)
            {
                button.image.color = Color.green;
                button.interactable = false;
            }

            Complete();
        }
    }
}
