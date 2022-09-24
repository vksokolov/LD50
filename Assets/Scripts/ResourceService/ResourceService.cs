using System;
using System.Collections.Generic;
using UniRx;

public class ResourceService
{
    private readonly Dictionary<ResourceType, ReactiveProperty<int>> _resources;
    public ResourceService()
    {
        _resources = new Dictionary<ResourceType, ReactiveProperty<int>>();
        foreach (var resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            _resources.Add((ResourceType)resourceType, new ReactiveProperty<int>(0));
        }
    }

    public int GetResource(ResourceType type) => _resources[type].Value;
    public void AddResource(ResourceType type, int value) => _resources[type].Value += value;
}