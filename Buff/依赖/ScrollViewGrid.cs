using UnityEngine;
using UnityEngine.Events;

namespace Momos.EditorTools.Control
{
    public enum E_ColumnStyle
    { 
        Single,
        Full,
    }

    public class ScrollViewColumnItem<T>
    {
        public int fix;
        public float percent;
        public E_ColumnStyle columnStyle;
        public string title;
        public UnityAction<Rect, T> colDrawEvt;

        public ScrollViewColumnItem(string title, int fix, float percent = 0,E_ColumnStyle colStyle = E_ColumnStyle.Full,  UnityAction<Rect, T> colDraw = null)
        {
            this.fix = fix;
            this.percent = percent;
            this.title = title;
            this.columnStyle = colStyle;
            this.colDrawEvt = colDraw;
        }

        public void DrawTitle(Rect rect)
        {
            GUI.TextField(rect, title);
        }
        public void DrawContent(Rect rect,T t)
        {
            colDrawEvt?.Invoke(rect, t);
        }
    }

    public class ScrollViewGrid<T>
    {
        private int titleRowHeight;
        private int rowHeight;
        private ScrollViewColumnItem<T>[] items;
        private T[] ts;
        int[] colsWidth;
        Vector2 sv;

        public T[] Ts { set => ts = value; }

        /// <summary> </summary>
        /// <param name="rowHeight"></param>
        /// <param name="items"> 动态计算宽度的Item的percent和为1 </param>
        public ScrollViewGrid(int titleRowHeight, int rowHeight,T[] ts, params ScrollViewColumnItem<T>[] items)
        {
            this.titleRowHeight = titleRowHeight;   
            this.rowHeight = rowHeight;
            this.ts = ts;
            this.items = items;
            colsWidth = new int[items.Length];
            sv = Vector2.zero;
        }

        /// <summary> 计算列宽 </summary>
        private void GetColumnsWidth(float width)
        {
            // 第一遍先把固定宽定下来
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].percent <= 0)
                { 
                    width -= items[i].fix;
                    colsWidth[i] = items[i].fix;
                }
            }
            // 第二遍把可变宽定下来
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].percent > 0)
                    colsWidth[i] = (int)Mathf.Max(items[i].fix, items[i].percent * width);
            }
        }

        public void OnGUI(Rect position,float relativeMouseY)
        {
            GetColumnsWidth(position.width);

            GUILayout.BeginArea(position);  // 1
            // 绘制悬浮高光
            SetHover(position, relativeMouseY);
            // 1.1
            GUILayout.BeginArea(new Rect(0, 0, position.width, titleRowHeight));
            Rect rect = new Rect(0, 0, 0, titleRowHeight);
            for (int i = 0; i < items.Length; i++)
            {
                rect.width = colsWidth[i];
                items[i].DrawTitle(rect);
                rect.x += rect.width;
            }
            GUILayout.EndArea();    // 1.1

            // 1.2
            sv = GUI.BeginScrollView(
                new Rect(0, titleRowHeight, position.width, position.height - titleRowHeight),
                sv,
                new Rect(0, 0, position.width, items.Length * titleRowHeight));

            rect = new Rect(0, 0, 0, 0);
            // 按列绘制
            for (int i = 0; i < items.Length; i++)
            {
                rect.width = colsWidth[i];
                rect.height = // 单行风格给24个像素, 填满风格给整个行高
                    items[i].columnStyle == E_ColumnStyle.Single ? 18 : rowHeight;
                for (int t = 0; t < ts.Length; t++)
                {
                    items[i].DrawContent(rect, ts[t]);
                    rect.y += rowHeight;
                }
                rect.y = 0;
                rect.x += rect.width;
            }

            GUI.EndScrollView();  // 1.2

            GUILayout.EndArea();    // 1
        }

        /// <summary> 设置悬浮高光 </summary>
        /// <param name="position"> 组件Rect </param>
        /// <param name="relativeMouseY"> 将鼠标位置 相对组件顶部的Y值(向下增长) </param>
        private void SetHover(Rect position, float relativeMouseY)
        {
            if (relativeMouseY >= 0)
            {
                Rect rect;
                int index = -1;
                // 标题高光
                if (relativeMouseY < titleRowHeight)
                {
                    rect = new Rect(0, 0, position.width, titleRowHeight);
                }
                else
                {
                    // 去除标题高
                    relativeMouseY -= titleRowHeight;
                    index = (int)relativeMouseY / (int)rowHeight;
                    rect = new Rect(
                        0,
                        // 归正到行的起始高度
                        titleRowHeight + index * rowHeight,
                        position.width,
                        rowHeight);
                }
                // 没有条目时不显示
                if (ts.Length > index)
                {
                    GUI.Box(rect, GUIContent.none);
                }
            }
        }
    }
}