using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Woodsman : MonoBehaviour
{
    private Cell _targetTree;
    private WoodsmanStats _woodsmanStats;

    private IEnumerator _currentAction;
    
    public void Init(WoodsmanStats woodsmanStats)
    {
        _woodsmanStats = woodsmanStats;
        _currentAction = FindTree();
    }

    public void OnTick()
    {
        _currentAction.MoveNext();
    }

    private IEnumerator FindTree()
    {
        do
        {
            Debug.Log("Finding a tree...");
            _targetTree = _woodsmanStats.GetTree(transform.position);
            yield return null;
        } while (_targetTree == null);


        _currentAction = GoToTree();
        
        yield return null;
    }

    private IEnumerator GoToTree()
    {
        Debug.Log("Going to the tree...");
        var treePosition = _targetTree.transform.position;
        var goToTree = GoTo(treePosition);
        while (goToTree.MoveNext())
        {
            yield return null;
        }
        
        _currentAction = ChopTree();
    }
    
    private IEnumerator GoTo(Vector3 targetPosition)
    {
        Debug.Log($"Going to the {targetPosition}...");
        var dir = (targetPosition - transform.position).normalized;
        while ((targetPosition - transform.position).sqrMagnitude > .1f)
        {
            var step = dir * _woodsmanStats.Speed;
            if (step.sqrMagnitude > dir.sqrMagnitude)
                step = dir;
            transform.position += step;
            yield return null;
        }

        Debug.Log($"(targetPosition - transform.position).sqrMagnitude = {(targetPosition - transform.position).sqrMagnitude}");
        yield return null;
    }
    
    private IEnumerator ChopTree()
    {
        Debug.Log("Chopping the tree...");
        float tickRemains = _woodsmanStats.ChopTimeTicks;

        while (tickRemains > 0)
        {
            tickRemains--;
            yield return null;
        }

        _targetTree.OnTreeChopped();
        _currentAction = GoToHome();
    }

    private IEnumerator GoToHome()
    {
        Debug.Log("Going home...");
        var goToHome = GoTo(_woodsmanStats.HomePosition);
        while (goToHome.MoveNext())
        {
            yield return null;
        }
        
        _currentAction = FindTree();
    }
}
