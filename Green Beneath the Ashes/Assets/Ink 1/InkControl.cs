using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;

public class LinearInkController : MonoBehaviour
{
    [Header("Ink JSON Asset")]
    public TextAsset inkJSONAsset;

    [Header("UI")]
    public TMP_Text storyText;
    public Button continueButton;

    private Story story;

    void Start()
    {
        story = new Story(inkJSONAsset.text);
        ShowNextLine();

        continueButton.onClick.AddListener(ShowNextLine);
    }

    void ShowNextLine()
    {
        if (story.canContinue)
        {
            string text = story.Continue().Trim();
            storyText.text = text;
        }
        else
        {
            storyText.text = "[故事结束了]";
            continueButton.interactable = false;
        }
    }
}