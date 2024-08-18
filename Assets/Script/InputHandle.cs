using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class InputHandle : MonoBehaviour
{
    public static InputHandle instance;
    [SerializeField] private InputField inputField;
    [SerializeField] private string input;
    [SerializeField] private bool isSubmit;

    public string Input { get => input; set => input = value; }
    public bool IsSubmit { get => isSubmit; set => isSubmit = value; }
    public InputField InputField { get => inputField; set => inputField = value; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    public void SubmitInput()
    {
        
        IsSubmit = true;
        Input = InputField.text;

    }
}
