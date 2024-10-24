using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private RectTransform rect;
    [SerializeField] private RectTransform[] options;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //Position
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            ChangePosition(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            ChangePosition(1);
        }
        //Interaction
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.E))
        {
            Interact();
        }
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if (currentPosition < 0)
        {
            Debug.Log("a");
            currentPosition = options.Length - 1;
        }
        else if (currentPosition > options.Length - 1)
        {
            currentPosition = 0;
        }

        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}

