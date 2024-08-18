using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{

    [Header("Quest Info")]
    [SerializeField] private TMP_Text questTitleText;
    [SerializeField] private TMP_Text questDescriptionText;
    [SerializeField] private TMP_Text questSummaryText;
    [SerializeField] private TMP_Text questPointText;

    [Header("Quest UI in Main Screen")]
    [SerializeField] private TMP_Text outQuestTitleText;
    [SerializeField] private TMP_Text outQuestDescriptionText;

    [Header("Text Box of Assistant")]
    [SerializeField] private GameObject questAssistantUI;
    [SerializeField] private GameObject inputField;
    [SerializeField] private TMP_Text assistantDescriptionText;

    [Header("Complete Quest Effect")]
    [SerializeField] private GameObject questCompletedEffect;
    [SerializeField] private GameObject completedText;
    public void UpdateQuestInfo(QuestProfile questProfile)
    {


        if (questProfile == null)
        {
            //update Quest UI
            questTitleText.text = null;
            questDescriptionText.text = null;
            questSummaryText.text = null;
            questPointText.text = null;

            //update Quest UI at main screen
            outQuestTitleText.text = null;
            outQuestDescriptionText.text = null;

            //update text of assistant
            assistantDescriptionText.text = null;

            return;

        }
        //update Quest UI
        questTitleText.text = questProfile.QuestTitle;
        questDescriptionText.text = questProfile.QuestDescription;
        questSummaryText.text = "Tóm tắt: " + questProfile.QuestSummary;
        string typeReward = null;
        switch(questProfile.QuestReward.ToString())
        {
            case "Strength":
                typeReward = "Sức mạnh";
                break;
            case "Social":
                typeReward = "Xã hội";
                break;
            case "Intelligence":
                typeReward = "Trí tuệ";
                break;
            case "Knowledge":
                typeReward = "Hiểu biết";
                break;
            default:
                typeReward = "";
                break;
        }
        questPointText.text = "Phần thưởng: +" + questProfile.QuestPoint.ToString() + " điểm " +typeReward;

        //update Quest UI at main screen
        outQuestTitleText.text = questProfile.QuestTitle;
        outQuestDescriptionText.text = questProfile.QuestDescription;

        //
        assistantDescriptionText.text = questProfile.QuestDescription;
    }


    public void ShowAssistantUI()
    {
        questAssistantUI.SetActive(true);
        StartCoroutine(HideHelperUI());
    }

    IEnumerator HideHelperUI()
    {
        // Chờ 5 giây trước khi tắt UI
        yield return new WaitForSeconds(7);
        questAssistantUI.SetActive(false);
    }

    public void CompleteQuestEffect()
    {
        questCompletedEffect.GetComponent<ParticleSystem>().Play();
        completedText.GetComponentInChildren<TMP_Text>().text = "HOAN THANH NHIEM VU!!!";
        completedText.SetActive(true);
        completedText.transform.localScale = Vector3.zero;
        completedText.transform.DOScale(1, .5f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    completedText.transform.DOScale(0f, .5f)
                    .OnComplete(() =>
                    {
                        completedText.SetActive(false);
                    });
                });
            });
    }
}
