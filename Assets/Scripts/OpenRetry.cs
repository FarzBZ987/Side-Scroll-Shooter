using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenRetry : MonoBehaviour
{
    [SerializeField] private GameObject controlCanvas;
    [SerializeField] private GameObject retryCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        Player.onDie += OpenCanvas;
    }

    private void OpenCanvas()
    {
        controlCanvas.SetActive(false);
        retryCanvas.SetActive(true);

        Player.onDie -= OpenCanvas;
    }

    // Update is called once per frame
    private void Update()
    {
    }
}