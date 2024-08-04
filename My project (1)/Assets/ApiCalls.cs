using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;

public class ApiCalls : MonoBehaviour
{
    public InputField inputFieldN;
    public InputField inputFieldMin;
    public InputField inputFieldMax;
    public Text resultText;

    //[DllImport("APIIntegration")]
    [DllImport("__Internal")]
    private static extern void initializeAPI();


    //[DllImport("APIIntegration")]
    [DllImport("__Internal")]
    private static extern void generateIntegers(int n, int min, int max, GenerateIntegersCallback callback);

    private delegate void GenerateIntegersCallback(System.IntPtr result);

    private void Start()
    {
         initializeAPI();
        print("api initialized");
    }

    public void OnGenerateIntegersButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputFieldN.text) ? 10 : int.Parse(inputFieldN.text);
        int min = string.IsNullOrEmpty(inputFieldMin.text) ? 1 : int.Parse(inputFieldMin.text);
        int max = string.IsNullOrEmpty(inputFieldMax.text) ? 100 : int.Parse(inputFieldMax.text);
        print("tapped");
        generateIntegers(n, min, max, OnGenerateIntegersCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateIntegersCallback))]
    private static void OnGenerateIntegersCompleted(System.IntPtr result)
    {
        string resultString = Marshal.PtrToStringAuto(result);
        string[] resultArray = resultString.Split(',');
        
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }

    private void DisplayResult(string[] resultArray)
    {
        resultText.text = "Result: " + string.Join(", ", resultArray);
    }
}
