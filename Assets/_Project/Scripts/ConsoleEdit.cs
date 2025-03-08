using TMPro;
using UnityEngine;

public class ConsoleEdit : MonoBehaviour
{
    public TextMeshPro textBox;

    void Start()
    {
        if (textBox == null)
        {
            Debug.LogWarning("TextBox is not assigned!");
        }
    }

    public void UpdateText(string newText)
    {
        textBox.text = newText;
    }
}