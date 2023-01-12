using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGroupLayout : LayoutGroup
{
    [SerializeField] private FitType fitType;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private int maxRows = 1;
    [SerializeField] private int maxColumns = 1;
    [SerializeField] private Vector2 cellsize;
    [SerializeField] private Vector2 spacing;
    [SerializeField] private bool fitX;
    [SerializeField] private bool fitY;

    public override void CalculateLayoutInputVertical()
    {

    }

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType == FitType.Width || fitType == FitType.Height || fitType == FitType.Uniform)
        {
            fitX = true;
            fitY = true;

            float sqrRT = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrRT);
            columns = Mathf.CeilToInt(sqrRT);
        }

        if (fitType == FitType.Width || fitType == FitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float) columns);
        }

        if (fitType == FitType.Height || fitType == FitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float)rows);
        }


        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        //float cellWidth = (parentWidth - padding.left - padding.right - spacing.x * (columns - 1)) / (float)columns;
        float cellWidth = (parentWidth / (float)columns) - ((spacing.x / (float)columns) * (columns - 1));
        float cellHeight = (parentHeight / (float)rows) - ((spacing.y / (float)rows) * (rows - 1));

        cellsize.x = fitX ? cellWidth : cellsize.x;
        cellsize.y = fitY ? cellHeight : cellsize.y;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellsize.x * columnCount) + (spacing.x * columnCount);
            var yPos = (cellsize.y * rowCount) + (spacing.y * rowCount);

            SetChildAlongAxis(item, 0, xPos, cellsize.x);
            SetChildAlongAxis(item, 1, yPos, cellsize.y);
        }
    }

    public override void SetLayoutHorizontal()
    {
    }

    public override void SetLayoutVertical()
    {
    }

    public void SetRows(int rows)
    {
        if (rows <= 0)
        {
            Debug.LogError("Setting less than 0 Rows");
            return;
        }

        if (rows > maxRows)
        {
            columns = (maxRows / rows) + 1;
            this.rows = maxRows;
        }
        else
        {
            this.rows = rows;
        }
    }

    public void SetColumns(int columns)
    {
        if (columns <= 0)
        {
            Debug.LogError("Setting less than 0 Columns");
            return;
        }

        if (columns > maxColumns)
        {
            if (columns % maxColumns >= 1)
            {
                rows = (maxColumns / columns) + 1;
                this.columns = maxColumns;
            }

        }
        else
        {
            this.columns = columns;
        }
    }
}
