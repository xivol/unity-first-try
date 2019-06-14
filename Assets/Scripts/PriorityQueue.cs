using System;
using System.Collections.Generic;
/// <summary>
/// https://visualstudiomagazine.com/articles/2012/11/01/priority-queues-with-c.aspx
/// </summary>
public class PriorityQueue<T, TComparer> where TComparer : IComparer<T>, new()
{
    private List<T> data;
    private TComparer comparer;

    public PriorityQueue()
    {
        this.data = new List<T>();
        this.comparer = new TComparer();
       
    }

    public void Enqueue(T item)
    {
        data.Add(item);
        int ci = data.Count - 1; // child index; start at end
        while (ci > 0)
        {
            int pi = (ci - 1) / 2; // parent index
            if (comparer.Compare(data[ci], data[pi]) >= 0) break; // child item is larger than (or equal) parent so we're done
            T tmp = data[ci]; data[ci] = data[pi]; data[pi] = tmp;
            ci = pi;
        }
    }

    public T Dequeue()
    {
        // assumes pq is not empty; up to calling code
        int li = data.Count - 1; // last index (before removal)
        T frontItem = data[0];   // fetch the front
        data[0] = data[li];
        data.RemoveAt(li);

        --li; // last index (after removal)
        int pi = 0; // parent index. start at front of pq
        while (true)
        {
            int ci = pi * 2 + 1; // left child index of parent
            if (ci > li) break;  // no children so done
            int rc = ci + 1;     // right child
            if (rc <= li && comparer.Compare(data[rc], data[ci]) < 0) // if there is a rc (ci + 1), and it is smaller than left child, use the rc instead
                ci = rc;
            if (comparer.Compare(data[pi], data[ci]) <= 0) break; // parent is smaller than (or equal to) smallest child so done
            T tmp = data[pi]; data[pi] = data[ci]; data[ci] = tmp; // swap parent and child
            pi = ci;
        }
        return frontItem;
    }

    public T Peek()
    {
        T frontItem = data[0];
        return frontItem;
    }

    public int Count
    {
        get { return data.Count; }
    }

    public override string ToString()
    {
        string s = "{ ";
        for (int i = 0; i < data.Count; ++i)
            s += data[i].ToString() + "; ";
        s += " }";
        return s;
    }

    public bool IsConsistent()
    {
        // is the heap property true for all data?
        if (data.Count == 0) return true;
        int li = data.Count - 1; // last index
        for (int pi = 0; pi < data.Count; ++pi) // each parent index
        {
            int lci = 2 * pi + 1; // left child index
            int rci = 2 * pi + 2; // right child index

            if (lci <= li && comparer.Compare(data[pi],data[lci]) > 0) return false; // if lc exists and it's greater than parent then bad.
            if (rci <= li && comparer.Compare(data[pi], data[rci]) > 0) return false; // check the right child too.
        }
        return true; // passed all checks
    }

}


/// <summary>
/// I'm sure there is a way to do this without creating a custom class
/// but google gave me nothing so ¯\_(ツ)_/¯
/// </summary>
public class ComparableComparer<T> : Comparer<T> where T : IComparable<T>
{
    public override int Compare(T x, T y)
    {
        return x.CompareTo(y);
    }
}

public class PriorityQueue<T> : PriorityQueue<T, ComparableComparer<T>> where T : IComparable<T> { }
