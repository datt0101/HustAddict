using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/LessonProfile")]
public class LessonProfile : ScriptableObject
{
    [SerializeField] private string lessonName;
    [SerializeField] private string lessonTheory;
    [SerializeField] private string lessonFormula;
    [SerializeField] private QuestionProfile[] questionProfileList;

    public string LessonName { get => lessonName; set => lessonName = value; }
    public string LessonTheory { get => lessonTheory; set => lessonTheory = value; }
    public string LessonFormula { get => lessonFormula; set => lessonFormula = value; }
    public QuestionProfile[] QuestionProfileList { get => questionProfileList; set => questionProfileList = value; }
}
