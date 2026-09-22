using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Profiling;

public struct CircularPointer
{
    /// <summary>
    /// num不为负数
    /// </summary>
    private int num;

    private int size;
    public int Num
    {
        get
        {
            return num;
        }
    }

    public int Size
    {
        get
        {
            return size;
        }
    }

    public CircularPointer(int num, int size)
    {
        this.num = num;
        this.size = size;
    }

    public static CircularPointer operator +(in CircularPointer a, int b)
    {
        return new CircularPointer(((a.Num + b) % a.Size + a.Size) % a.Size, a.Size);
    }

    /// <summary>
    /// 会自动计算从自身正向移动到tail之间的距离，要求两个CircularPointer的Size相等，计算时认为tail指向位置为空
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public int DistanceTo(in CircularPointer tail)
    {
        if (this.size != tail.size)
        {
#if UNITY_EDITOR
            Debug.LogError("Size should be same if wanna Count Distance!");
#endif
            return default;
        }
        else
        {
            if (this.num <= tail.num)
            {
                return tail.num - this.num;
            } else
            {
                return (this.size - this.num) + tail.num;
            }
        }
    }

    /// <summary>
    /// 在循环范围内移动一格
    /// </summary>
    /// <param name="head"></param>
    /// <param name="tail"></param>
    public static void MovingOneAt(ref CircularPointer cursor, CircularPointer head, CircularPointer tail)
    {
        if (head.size != tail.size || cursor.size != head.size)
        {
#if UNITY_EDITOR
            Debug.LogError("Size should be same if wanna Move Cursor!");
#endif
        }
        else
        {
            if (head.DistanceTo(tail) == 0)
            {
#if UNITY_EDITOR
                Debug.LogError("Distance is 0!");
#endif
            }
            else
            {
                if ((cursor + 1) == tail)
                {
                    cursor.num = head.num;
                }
                else
                {
                    cursor.num = ((cursor.Num + 1) % cursor.Size + cursor.Size) % cursor.Size;
                }
            }
        }
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(in CircularPointer a, in CircularPointer b)
    {
        return a.Size == b.Size && a.Num == b.Num;
    }

    public static bool operator !=(in CircularPointer a, in CircularPointer b)
    {
        return !(a == b);
    }

    public static bool operator <(in CircularPointer a, in CircularPointer b)
    {
        return a.Size == b.Size && a.Num < b.Num;
    }

    public static bool operator >(in CircularPointer a, in CircularPointer b)
    {
        return a.Size == b.Size && a.Num > b.Num;
    }
}
public class CircularArray<T>
{
    /// <summary>
    /// 直接代表会循环的索引，从0开始，head = tail时为空
    /// </summary>
    private CircularPointer head;

    /// <summary>
    /// 直接代表会循环的索引，从0开始，tail上的值不计入
    /// </summary>
    private CircularPointer tail;

    private T[] values;

    private T nowValue;

    private CircularPointer cursor;

    public CircularArray(int size)
    {
        values = new T[size + 1];
        head = new CircularPointer(0, size + 1);
        tail = new CircularPointer(0, size + 1);
        cursor = new CircularPointer(0, size + 1);
    }

    public void AddValue(T value)
    {
        if (IsFull)
        {
            RemoveHeadValue();
        } 
        values[tail.Num] = value;
        tail += 1;
    }

    public void RemoveHeadValue()
    {
        if (!IsEmpty)
        {
            if (cursor.Num == head.Num)
            {
                cursor += 1;
            }
            head += 1;
        }
    }

    public T Read()
    {
        if (IsEmpty)
        {
            return default;
        }
        return values[head.Num];
    }
    public T ReadAndRemove()
    {
        if (IsEmpty)
        {
            nowValue = default;
        }
        else
        {
            nowValue = values[head.Num];
            RemoveHeadValue();
        }
        return nowValue;
    }

    /// <summary>
    /// 此时head = tail
    /// </summary>
    public bool IsEmpty
    {
        get
        {
            return tail == head;
        }
        
    }

    public bool IsFull
    {
        get
        {
            return (tail + 1) == head;
        }
    }

    public void Clear()
    {
        head = tail;
    }

    public int Count()
    {
        return head.DistanceTo(tail);
    }

    public void CursorMoveOne()
    {
        CircularPointer.MovingOneAt(ref cursor, head, tail);
    }

    public T GetCursorValue()
    {
        return values[cursor.Num];
    }

    public T[] ToArray()
    {
        T[] array = new T[head.DistanceTo(tail)];
        int num = 0;
        for (CircularPointer i = head; i != tail; i += 1)
        {
            array[num] = values[i.Num];
            num++;
        }
        return array;
    }
}
