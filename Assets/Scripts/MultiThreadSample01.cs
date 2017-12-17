using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[RequireComponent(typeof(TextMesh))]
public class MultiThreadSample01 : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;

	private CancellationTokenSource tokenSource;
    private int mainThreadId;
    private int anotherThreadId;
    private int loopCount;

	void Start()
	{
		tokenSource = new CancellationTokenSource();
		RunAnotherThread(tokenSource.Token);

        mainThreadId = Thread.CurrentThread.ManagedThreadId;

        if (textMesh == null)
        {
            textMesh = GetComponent<TextMesh>();
        }
	}

	void Update()
	{
        string displayString = "Main Thread Id: " + mainThreadId + "\n"
                             + "Another Thread Id: " + anotherThreadId + "\n"
                             + "Another Thread Loop Count: " + loopCount;

        if (textMesh != null)
        {
            textMesh.text = displayString;
        }
	}

    private void RunAnotherThread(CancellationToken token)
    {
        Task.Run(() =>
        {
            loopCount = 0;
            anotherThreadId = Thread.CurrentThread.ManagedThreadId;

            /* Main loop */
            while(true)
            {
                token.ThrowIfCancellationRequested();

                GetPrimeNumbers(2, 2000000);

                loopCount++;
            }
        });
    }

	void OnDestroy()
	{
		tokenSource.Cancel();
	}

    private List<int> GetPrimeNumbers(int minimum, int maximum)
    {
        List<int> primes = new List<int>();
        for(int i = minimum; i <= maximum; i++)
        {
            if(IsPrimeNumber(i))
            {
                primes.Add(i);
            }
        }
        return primes;
    }

    private bool IsPrimeNumber(int p)
    {
        if (p < 2)
        {
            return false;
        }
        else if (p == 2)
        {
            return true;
        }

        if (p % 2 == 0)
        {
            return false;
        }

        int topLimit = (int)Math.Sqrt(p);
        for (int i = 3; i <= topLimit; i += 2)
        {
            if (p % i == 0)
            {
                return false;
            }
        }

        return true;
    }

}
