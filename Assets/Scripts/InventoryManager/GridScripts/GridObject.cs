using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject
{

    private int width;
    private int height;
    private float cellsize;
    private int[,] gridArray;

    private GameObject canvas;
    private Transform canvas_;

    public GridObject(int width, int height, float cellsize)
    {
        this.width = width;
        this.height = height;
        this.cellsize = cellsize;

        gridArray = new int[width, height];

        canvas = GameObject.Find("UI");
        canvas_ = canvas.GetComponent<Transform>();

        for(int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int y = 0; y < gridArray.GetLength(1); y++)
            {
                CreateWorldText(gridArray[x, y].ToString(), canvas_, GetWorldPosition(x,y), 20, Color.white, TextAnchor.MiddleCenter);

            }

        }

        Vector3 GetWorldPosition(int x, int y)
        {
            return new Vector3(x, y) * cellsize;
        }


        TextMesh CreateWorldText(string text, Transform parent, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor)// TextAlignment textAlignment, int sortingOrder)
        {
            GameObject gameObject = new GameObject("World Text", typeof(TextMesh));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMesh textMesh = gameObject.GetComponent<TextMesh>();
            textMesh.anchor = textAnchor;
            //textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            //textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }


    }

}
