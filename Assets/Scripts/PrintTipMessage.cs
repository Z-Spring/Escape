using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class PrintTipMessage : MonoBehaviour
{
    public static PrintTipMessage Instance { get; private set; }
    [SerializeField] private TextMeshProUGUI messageHolder;
    [SerializeField] private float messageDisplayTime = 3f;
    [SerializeField] private float printSpeed = 0.1f;
    [Range(1, 5)] [SerializeField] private int ADDITION_NUM = 1;

    private bool isPrinting;
    private readonly Queue<string> tipMessages = new Queue<string>();
    

    private void Awake()
    {
        Instance = this;
    }

    // private void Start()
    // {
    //     messageHolder.text = "";
    // }

    private void Update()
    {
        PrintMessage();
    }

    private void PrintMessage()
    {
        if (tipMessages.Count > 0 && messageHolder != null)
        {
            if (!isPrinting)
            {
                isPrinting = true;
                StartCoroutine(PrintMessageCoroutine(tipMessages.Dequeue(), messageHolder));
            }
        }
    }

    private IEnumerator PrintMessageCoroutine(string message, TextMeshProUGUI messageHolder)
    {
        messageHolder.text = "";
        char[] chars = message.ToCharArray();
        StringBuilder sb = new StringBuilder(messageHolder.text);
        int numAddition = tipMessages.Count + ADDITION_NUM;
        for (int i = 0; i < chars.Length; i += numAddition)
        {
            for (int j = 0; j < numAddition; j++)
            {
                if (i + j >= chars.Length)
                {
                    break;
                }

                sb.Append(chars[i + j]);
            }

            messageHolder.text = sb.ToString();
            float delay = 1f / printSpeed;
            yield return new WaitForSeconds(delay);

            // yield return new WaitForSeconds(printSpeed);
        }

        isPrinting = false;
        yield return new WaitForSeconds(messageDisplayTime);
        messageHolder.text = "";
    }
    
    public void SetPrintMessageInfos(string message,TextMeshProUGUI messageHolder)
    {
        tipMessages.Enqueue(message);
        this.messageHolder = messageHolder;
    }
}