using UnityEngine;


public interface ICompletable
{
    void Complete(GameObject player);

    void Cancel(GameObject player);

}

