﻿namespace DynamicLoading
{
    public interface IDynamicLoadableExtensionConfiguration
    {
        string SetionName { get; }
        string Type { get; }
        bool Active { get; }
    }
}
