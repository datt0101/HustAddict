using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private TMP_Text textUI; // Tham chiếu đến Text UI
    [SerializeField] private int timeAssistantDisappear = 7;
    [SerializeField] private int timeForReading = 3;
    private string fullText; // Toàn bộ văn bản
    private string currentText = ""; // Văn bản hiện tại đang được hiển thị

    void OnEnable()
    {
        fullText = textUI.text; // Lấy văn bản đầy đủ từ Text UI
        textUI.text = ""; // Xóa văn bản từ Text UI
        StartCoroutine(ShowText());
    }

    IEnumerator ShowText()
    {
        int l = fullText.Length;
        float delayBetweenCharacters = (timeAssistantDisappear - timeForReading) / l; // Thời gian trễ giữa mỗi ký tự
        for (int i = 0; i <= l; i++)
        {
            currentText = fullText.Substring(0, i);
            textUI.text = currentText;
            yield return new WaitForSeconds(delayBetweenCharacters);
        }
    }
}