using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/QuestionProfile")]
public class QuestionProfile : ScriptableObject
{
    [SerializeField] private string questionAsk; // yeu cau cua cau hoi
    [SerializeField] private string questionContent; // noi dung cau hoi
    [SerializeField] private string[] questionSelect;// cac lua chon
    [SerializeField] private string questionAnswer; // dap an
    [SerializeField] private int questionPoint; // diem thuong

    public string QuestionAsk { get => questionAsk; set => questionAsk = value; }
    public string QuestionContent { get => questionContent; set => questionContent = value; }
    public string[] QuestionSelect { get => questionSelect; set => questionSelect = value; }
    public string QuestionAnswer { get => questionAnswer; set => questionAnswer = value; }
    public int QuestionPoint { get => questionPoint; set => questionPoint = value; }
}
   

