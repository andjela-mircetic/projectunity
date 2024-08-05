#if UNITY_IOS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using TMPro;

public class ApiCalls : MonoBehaviour
{
    

    [DllImport("__Internal")]
    private static extern void initializeAPI();

    [DllImport("__Internal")]
    private static extern void generateIntegers(int n, int min, int max, GenerateIntegersCallback callback);

    [DllImport("__Internal")]
    private static extern void generateIntegerSequences(int n, int length, int min, int max, GenerateIntegerSequencesCallback callback);

    [DllImport("__Internal")]
    private static extern void generateDecimalFractions(int n, int decimalPlaces, GenerateDecimalFractionsCallback callback);

    [DllImport("__Internal")]
    private static extern void generateGaussians(int n, double mean, double standardDeviation, int significantDigits, GenerateGaussiansCallback callback);

    [DllImport("__Internal")]
    private static extern void generateStrings(int n, int length, string characters, GenerateStringsCallback callback);

    [DllImport("__Internal")]
    private static extern void generateUUIDs(int n, GenerateUUIDsCallback callback);

    [DllImport("__Internal")]
    private static extern void generateBlobs(int n, int size, string format, GenerateBlobsCallback callback);

    [DllImport("__Internal")]
    private static extern void verifySignature(string randomData, string signature, VerifySignatureCallback callback);

    [DllImport("__Internal")]
    private static extern void getUsage(GetUsageCallback callback);

    private delegate void GenerateIntegersCallback(System.IntPtr result);
    private delegate void GenerateIntegerSequencesCallback(System.IntPtr result);
    private delegate void GenerateDecimalFractionsCallback(System.IntPtr result);
    private delegate void GenerateGaussiansCallback(System.IntPtr result);
    private delegate void GenerateStringsCallback(System.IntPtr result);
    private delegate void GenerateUUIDsCallback(System.IntPtr result);
    private delegate void GenerateBlobsCallback(System.IntPtr result);
    private delegate void VerifySignatureCallback(System.IntPtr result);
    private delegate void GetUsageCallback(System.IntPtr result);

    private void Start()
    {
        print("initializing...");
         initializeAPI();
        print("api initialized");
    }

    public InputField inputFieldN;
    public InputField inputFieldMin;
    public InputField inputFieldMax;
    public Text resultText;

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


    public InputField inputN;
    public InputField inputMin;
    public InputField inputMax;
   public InputField inputLength;

   public void OnGenerateIntegerSequencesButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputN.text) ? 10 : int.Parse(inputN.text);
        int length = string.IsNullOrEmpty(inputLength.text) ? 1 : int.Parse(inputLength.text);
        int min = string.IsNullOrEmpty(inputMin.text) ? 1 : int.Parse(inputMin.text);
        int max = string.IsNullOrEmpty(inputMax.text) ? 100 : int.Parse(inputMax.text);
        print("tapped");
        generateIntegerSequences(n, length, min, max, OnGenerateIntegerSequencesCompleted);
    }

[System.Serializable]
public class IntegerSequenceWrapper
{
    public List<List<int>> sequences;
}

    [AOT.MonoPInvokeCallback(typeof(GenerateIntegerSequencesCallback))]
private static void OnGenerateIntegerSequencesCompleted(System.IntPtr result)
{
    string resultString = Marshal.PtrToStringAuto(result);
    string[] sequences = resultString.Split(';');
    List<List<int>> sequenceList = new List<List<int>>();

    foreach (string seq in sequences)
    {
        string[] numbers = seq.Trim('[', ']').Split(',');
        List<int> intList = new List<int>();
        foreach (string number in numbers)
        {
            if (int.TryParse(number, out int value))
            {
                intList.Add(value);
            }
        }
        sequenceList.Add(intList);
    }

    IntegerSequenceWrapper wrapper = new IntegerSequenceWrapper();
    wrapper.sequences = sequenceList;

    List<string> resultStrings = new List<string>();
    if (wrapper != null && wrapper.sequences != null)
    {
        foreach (var sequence in wrapper.sequences)
        {
            resultStrings.Add(string.Join(", ", sequence));
        }
    }

    ApiCalls instance = FindObjectOfType<ApiCalls>();
    if (instance != null)
    {
        instance.DisplayResult(resultStrings.ToArray());
    }
}

    public InputField inputN2;
    public InputField decimalPlaces2;

   public void OnGenerateDecimalFractionsButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputN2.text) ? 10 : int.Parse(inputN2.text);
        int decimalPlaces = string.IsNullOrEmpty(decimalPlaces2.text) ? 2 : int.Parse(decimalPlaces2.text);
        Debug.Log("Generating decimal fractions...");
        generateDecimalFractions(n, decimalPlaces, OnGenerateDecimalFractionsCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateDecimalFractionsCallback))]
    private static void OnGenerateDecimalFractionsCompleted(System.IntPtr result)
    {
        string resultString = Marshal.PtrToStringAuto(result);

        string[] resultArray = resultString.Split(',');

        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }


    public InputField inputFieldN3;
    public InputField inputFieldMean;
    public InputField inputFieldStandardDeviation;
    public InputField inputFieldSignificantDigits;

    public void OnGenerateGaussiansButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputFieldN3.text) ? 10 : int.Parse(inputFieldN3.text);
        double mean = string.IsNullOrEmpty(inputFieldMean.text) ? 0.0 : double.Parse(inputFieldMean.text);
        double standardDeviation = string.IsNullOrEmpty(inputFieldStandardDeviation.text) ? 1.0 : double.Parse(inputFieldStandardDeviation.text);
        int significantDigits = string.IsNullOrEmpty(inputFieldSignificantDigits.text) ? 2 : int.Parse(inputFieldSignificantDigits.text);

        Debug.Log("Generating Gaussians...");
        generateGaussians(n, mean, standardDeviation, significantDigits, OnGenerateGaussiansCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateGaussiansCallback))]
    private static void OnGenerateGaussiansCompleted(System.IntPtr result)
    {
       
        string resultString = Marshal.PtrToStringAuto(result);

        string[] resultArray = resultString.Split(',');

      
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }


    public InputField inputFieldN4;
    public InputField inputFieldLength2;
    public InputField inputFieldCharacters;
    

   public void OnGenerateStringsButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputFieldN4.text) ? 10 : int.Parse(inputFieldN4.text);
        int length = string.IsNullOrEmpty(inputFieldLength2.text) ? 5 : int.Parse(inputFieldLength2.text);
        string characters = string.IsNullOrEmpty(inputFieldCharacters.text) ? "abcdefghijklmnopqrstuvwxyz" : inputFieldCharacters.text;

        Debug.Log("Generating strings...");
        generateStrings(n, length, characters, OnGenerateStringsCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateStringsCallback))]
    private static void OnGenerateStringsCompleted(System.IntPtr result)
    {
       
        string resultString = Marshal.PtrToStringAuto(result);

        
        string[] resultArray = resultString.Split(new[] { ", " }, System.StringSplitOptions.None);

        
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }


     public InputField inputFieldN5;

     public void OnGenerateUUIDsButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputFieldN5.text) ? 1 : int.Parse(inputFieldN5.text);

        Debug.Log("Generating UUIDs...");
        generateUUIDs(n, OnGenerateUUIDsCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateUUIDsCallback))]
    private static void OnGenerateUUIDsCompleted(System.IntPtr result)
    {
      
        string resultString = Marshal.PtrToStringAuto(result);

        
        string[] resultArray = resultString.Split(new[] { ", " }, System.StringSplitOptions.None);

       
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }


 public InputField inputFieldN6;
    public InputField inputFieldSize3;
    public InputField inputFieldFormat;

 public void OnGenerateBlobsButtonClicked()
    {
        int n = string.IsNullOrEmpty(inputFieldN6.text) ? 1 : int.Parse(inputFieldN6.text);
        int size = string.IsNullOrEmpty(inputFieldSize3.text) ? 1024 : int.Parse(inputFieldSize3.text);
        string format = string.IsNullOrEmpty(inputFieldFormat.text) ? "base64" : inputFieldFormat.text;

        Debug.Log("Generating Blobs...");
        generateBlobs(n, size, format, OnGenerateBlobsCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GenerateBlobsCallback))]
    private static void OnGenerateBlobsCompleted(System.IntPtr result)
    {
        
        string resultString = Marshal.PtrToStringAuto(result);

    
        string[] resultArray = resultString.Split(new[] { ", " }, System.StringSplitOptions.None);

        
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult(resultArray);
        }
    }



    public InputField inputFieldRandomData;
    public InputField inputFieldSignature;

    public void OnVerifySignatureButtonClicked()
    {
        string randomData = inputFieldRandomData.text;
        string signature = inputFieldSignature.text;

        Debug.Log("Verifying signature...");
        verifySignature(randomData, signature, OnVerifySignatureCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(VerifySignatureCallback))]
    private static void OnVerifySignatureCompleted(System.IntPtr result)
    {
        
        string resultString = Marshal.PtrToStringAuto(result);

        
        bool isValid = resultString == "true";

   
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult2("" + isValid);
        }
    }
    

   public void OnGetUsageButtonClicked()
    {
        Debug.Log("Fetching usage...");
        getUsage(OnGetUsageCompleted);
    }

    [AOT.MonoPInvokeCallback(typeof(GetUsageCallback))]
    private static void OnGetUsageCompleted(System.IntPtr result)
    {
        
        string resultString = Marshal.PtrToStringAuto(result);

        
        ApiCalls instance = FindObjectOfType<ApiCalls>();
        if (instance != null)
        {
            instance.DisplayResult2(resultString);
        }
    }


    public void DisplayResult(string[] resultArray)
    {
        resultText.text = "Result: " + string.Join(", ", resultArray);
    }

    public void DisplayResult2(string result)
    {
        resultText.text = "Result: " + result;
    }

}
#endif