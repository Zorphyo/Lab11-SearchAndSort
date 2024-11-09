using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    List<InventoryItem> inventoryItems = new List<InventoryItem>();

    // Start is called before the first frame update
    void Start()
    {
        int random = UnityEngine.Random.Range(10000, 100000);

        AddItemToInventory(UnityEngine.Random.Range(10000, 100000), "Sword", UnityEngine.Random.Range(1, 1000));
        AddItemToInventory(UnityEngine.Random.Range(10000, 100000), "Shield", UnityEngine.Random.Range(1, 1000));
        AddItemToInventory(random, "Potion", UnityEngine.Random.Range(1, 1000));
        AddItemToInventory(UnityEngine.Random.Range(10000, 100000), "Armor", UnityEngine.Random.Range(1, 1000));
        AddItemToInventory(UnityEngine.Random.Range(10000, 100000), "Dagger", UnityEngine.Random.Range(1, 1000));

        foreach (InventoryItem item in inventoryItems)
        {
            PrintItemInfo(item);
        }

        int total = CalculateTotalInventoryValue();
        Debug.Log(total);

        List<InventoryItem> itemsInRange = FilterItemsByValueRange(200, 500);

        foreach (InventoryItem item in itemsInRange)
        {
            PrintItemInfo(item);
        }

        InventoryItem positiveSearch = LinearSearchByName("Shield");
        InventoryItem nullSearch = LinearSearchByName("Bow");

        PrintItemInfo(positiveSearch);

        if (nullSearch == null)
        {
            Debug.Log("Null");
        }

        QuickSortByID(0, inventoryItems.Count - 1);

        foreach (InventoryItem item in inventoryItems)
        {
            PrintItemInfo(item);
        }

        InventoryItem positiveBinarySearch = BinarySearchByID(random);
        InventoryItem nullBinarySearch = BinarySearchByID(99999);

        PrintItemInfo(positiveBinarySearch);

        if (nullBinarySearch == null)
        {
            Debug.Log("Null");
        }

        QuickSortByValue(0, inventoryItems.Count - 1);

        foreach (InventoryItem item in inventoryItems)
        {
            PrintItemInfo(item);
        }
    }
    
    void PrintItemInfo(InventoryItem item)
    {
        Debug.Log(item.id + " " + item.name + " " + item.value);
    }

    void AddItemToInventory(int id, string name, int value)
    {
        InventoryItem item = new InventoryItem();

        item.id = id;
        item.name = name;
        item.value = value;

        inventoryItems.Add(item);
    }

    int CalculateTotalInventoryValue()
    {
        int sum = 0;

        foreach (InventoryItem item in inventoryItems)
        {
            sum += item.value;
        }

        return sum;
    }

    List<InventoryItem> FilterItemsByValueRange(int minValue, int maxValue)
    {
        List<InventoryItem> itemsInRange = new List<InventoryItem>();

        foreach (InventoryItem item in inventoryItems)
        {
            if (item.value >= minValue && item.value <= maxValue)
            {
                itemsInRange.Add(item);
            }
        }

        return itemsInRange;
    }

    InventoryItem LinearSearchByName(string name)
    {
        foreach (InventoryItem item in inventoryItems)
        {
            if (string.Equals(item.name, name))
            {
                return item;
            }
        }

        return null;
    }

    InventoryItem BinarySearchByID(int target)
    {
        int left = 0;
        int right = inventoryItems.Count - 1;

        while (left <= right)
        {
            int mid = left + (right - left) / 2;

            if (inventoryItems[mid].id == target)
            {
                return inventoryItems[mid];
            }

            else if (inventoryItems[mid].id < target)
            {
                left = mid + 1;
            }

            else 
            {
                right = mid - 1;
            }
        }

        return null;
    }

    void QuickSortByValue(int first, int last)
    {
        if (first < last)
        {
            int pivot = PartitionByValue(first, last);

            QuickSortByValue(first, pivot - 1);
            QuickSortByValue(pivot + 1, last);
        }
    }

    int PartitionByValue(int first, int last)
    {
        int pivot = inventoryItems[last].value;
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            if (inventoryItems[element].value < pivot)
            {
                smaller++;

                InventoryItem temp = inventoryItems[smaller];
                inventoryItems[smaller] = inventoryItems[element];
                inventoryItems[element] = temp;

            }
        }

        InventoryItem tempNext = inventoryItems[smaller + 1];
        inventoryItems[smaller + 1] = inventoryItems[last];
        inventoryItems[last] = tempNext;

        return smaller + 1;
    }

    void QuickSortByID(int first, int last)
    {
        if (first < last)
        {
            int pivot = PartitionByID(first, last);

            QuickSortByID(first, pivot - 1);
            QuickSortByID(pivot + 1, last);
        }
    }

    int PartitionByID(int first, int last)
    {
        int pivot = inventoryItems[last].id;
        int smaller = first - 1;

        for (int element = first; element < last; element++)
        {
            if (inventoryItems[element].id < pivot)
            {
                smaller++;

                InventoryItem temp = inventoryItems[smaller];
                inventoryItems[smaller] = inventoryItems[element];
                inventoryItems[element] = temp;

            }
        }

        InventoryItem tempNext = inventoryItems[smaller + 1];
        inventoryItems[smaller + 1] = inventoryItems[last];
        inventoryItems[last] = tempNext;

        return smaller + 1;
    }
}