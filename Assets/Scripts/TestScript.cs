using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class TestScript : MonoBehaviour
{
    public List<Canvas> canvasList;
    Canvas _canvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _canvas = GetComponent<Canvas>();
        foreach (Canvas c in canvasList)
        {
            c.gameObject.SetActive(true); //in case not active when start
            c.overrideSorting = true;
            c.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickEvent(GameObject canvasObject)
    {
        Canvas clickedCanvas = canvasObject.GetComponent<Canvas>();

        int currentI = 0;

        for (int i = 0; i < canvasList.Count; i++)
        {
            if (canvasList[i] == clickedCanvas)
            {

                foreach (Canvas canvas in canvasList) { canvas.sortingOrder = 1; }
                clickedCanvas.sortingOrder = 32;

                /*currentI = i;
                if(i == canvasList.Count - 1) //already in correct order
                {

                }
                else
                {
                    Canvas oldCanvas = canvasList[canvasList.Count - 1];
                    
                    Canvas currentCanvas;
                    foreach (Canvas canvas in canvasList) { canvas.sortingOrder = 1; Debug.Log(canvas.name + " " + canvas.sortingOrder + " " + canvas.sortingLayerID); }
                    clickedCanvas.sortingOrder = 32;
                    Debug.Log(clickedCanvas.name + " " + clickedCanvas.sortingOrder + " " + clickedCanvas.sortingLayerID);
                    
                    /* for (int j = canvasList.Count - 1;  j > i; j--)
                     {
                         currentCanvas = canvasList[j];
                         currentCanvas.sortingOrder = currentCanvas.sortingOrder - 1;
                         Debug.Log(currentCanvas.name + " " + currentCanvas.sortingOrder);
                         canvasList[j] = oldCanvas;
                         oldCanvas = currentCanvas;
                     }/


                }*/
                return;

            }
        }
    }
}
