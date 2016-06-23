using UnityEngine;
using System.Collections;

public static class UserInput
{
    public static bool IsClicked()
    {
        if (Input.GetMouseButtonUp(0))
        {
            return true;
        }
        return false;
    }

    public static GameObject GetClickedObject()
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 5000))
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
}