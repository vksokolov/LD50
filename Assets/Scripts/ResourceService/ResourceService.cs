using System;
using System.Collections.Generic;

public class ResourceService
{
    private readonly Dictionary<ResourceType, int> _resources;
    public ResourceService()
    {
        _resources = new Dictionary<ResourceType, int>();
        foreach (var resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            _resources.Add((ResourceType)resourceType, 0);
        }
    }

    public int GetResource(ResourceType type) => _resources[type];
    public void AddResource(ResourceType type, int value) => _resources[type] += value;
}