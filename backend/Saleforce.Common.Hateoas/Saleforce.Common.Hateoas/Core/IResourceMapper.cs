using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Saleforce.Common.Hateoas.Core
{
    public interface IResourceMapper
    {
        Task<TResource> MapAsync<T, TResource>(T source)
            where TResource : Resource;

        Task<TContainer> MapContainerAsync<T, TResource, TContainer>(IEnumerable<T> models)
            where TContainer : ResourceContainer<TResource>, new()
            where TResource : Resource;

        Task<TResource> CreateEmptyResourceAsync<TResource>(Action<TResource> beforeMap = null)
            where TResource : Resource, new();
    }
}