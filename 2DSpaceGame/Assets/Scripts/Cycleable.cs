using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cycleable<T>
{
    List<T> items;
    int currentItem;

    public Cycleable()
    {
        items = new List<T>();
        currentItem = 0;
    }

    public void SetList(List<T> list)
    {
        items = list;
    }

    public void AddItem(T item)
    {
        if (item == null) { return; }

        items.Add(item);
    }

    public bool RemoveItem(T item)
    {
        if (item == null) { return false; }
        return items.Remove(item);
    }

    public T GetSelected()
    {
        return items[currentItem];
    }

    public void ChooseNext()
    {
        ++currentItem;
        if (currentItem >= items.Count)
        {
            currentItem = 0;
        }
    }
}
