using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using UnityEngine.SceneManagement;

public class LinearInkController : MonoBehaviour
{
    [Header("Ink JSON Asset")]
    public TextAsset inkJSONAsset;

    [Header("UI")]
    public TMP_Text nameText;      // 角色名字显示框
    public TMP_Text storyText;     // 对话内容显示框
    public Button continueButton;  // 点击继续按钮

    [Header("Character Sprites")]
    public CharacterManager characterManager; // 控制立绘切图的系统

    [Header("Card Panel UI")]
    public GameObject cardPanel;   // 整个卡片面板
    public Image cardImage;        // 卡片图片展示
    public CardEntry[] cardEntries; // 卡片标签与图片列表

    private Story story;
    private bool isWaitingForInput = true;

    [System.Serializable]
    public class CardEntry
    {
        public string tag;    // show_card 后面的标签名
        public Sprite sprite; // 对应的图片资源
    }

    void Start()
    {
        Debug.Log("Initializing LinearInkController...");
        story = new Story(inkJSONAsset.text);
        continueButton.onClick.AddListener(ShowNextLine);

        if (cardPanel != null)
        {
            cardPanel.SetActive(false);
            Debug.Log("Card panel hidden at start.");
        }

        ShowNextLine();
    }

    void Update()
    {
        if (Input.anyKeyDown && isWaitingForInput)
        {
            isWaitingForInput = false;
            Debug.Log("User input detected, advancing story.");
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        Debug.Log($"ShowNextLine() called. canContinue={story.canContinue}");
        if (story.canContinue)
        {
            string text = story.Continue().Trim();
            Debug.Log($"Ink line: '{text}'");

            if (text.Contains(':'))
            {
                var parts = text.Split(new[] { ':' }, 2);
                nameText.text = parts[0].Trim();
                storyText.text = parts[1].Trim();
                Debug.Log($"Speaker: {parts[0].Trim()} | Dialogue: {parts[1].Trim()}");
            }
            else
            {
                nameText.text = string.Empty;
                storyText.text = text;
                Debug.Log("Narration: " + text);
            }

            Debug.Log("Processing tags...");
            foreach (var tag in story.currentTags)
            {
                Debug.Log("Found tag: " + tag);
                HandleTag(tag);
            }
        }
        else
        {
            Debug.Log("No more lines in story. Triggering scene transition.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        isWaitingForInput = true;
    }

    void HandleTag(string tag)
    {
        Debug.Log("HandleTag called with tag: " + tag);
        var parts = tag.Split(' ');
        if (parts.Length == 3 && parts[0] == "sprite")
        {
            Debug.Log($"Switching sprite: character={parts[1]}, tag={parts[2]}");
            characterManager.ChangeSprite(parts[1], parts[2]);
        }
        else if (parts.Length == 2 && parts[0] == "show_card")
        {
            Debug.Log($"Showing card for tag: {parts[1]}");
            ShowCard(parts[1]);
        }
        else if (parts.Length == 1 && parts[0] == "hide_card")
        {
            Debug.Log("Hiding card panel.");
            if (cardPanel != null)
                cardPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning($"Unhandled tag format: '{tag}'");
        }
    }

    void ShowCard(string cardTag)
    {
        Debug.Log("ShowCard called with tag: " + cardTag);
        if (cardPanel == null || cardImage == null)
        {
            Debug.LogError("CardPanel or CardImage is not assigned in the inspector.");
            return;
        }

        foreach (var entry in cardEntries)
        {
            if (entry.tag == cardTag)
            {
                Debug.Log($"Card found: {cardTag}, setting sprite.");
                cardImage.sprite = entry.sprite;
                cardPanel.SetActive(true);
                return;
            }
        }

        Debug.LogWarning($"No card found for tag '{cardTag}'");
    }
}
