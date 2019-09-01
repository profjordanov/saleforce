using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RiskFirst.Hateoas;
using Saleforce.Common.Hateoas.Core;

namespace Saleforce.Common.Hateoas.Business
{
    /// <summary>
    /// The main entry-point into the framework,
    /// this interface is injected into user code to apply links to api resources.
    /// </summary>
    public class ResourceMapper : IResourceMapper
    {
        private readonly IMapper _mapper;
        private readonly ILinksService _linksService;

        public ResourceMapper(IMapper mapper, ILinksService linksService)
        {
            _mapper = mapper;
            _linksService = linksService;
        }

        /// <summary>
        /// Converts model to resource and adds links to it.
        /// This should give complete control of
        /// which links are applied at any point within your api code.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public async Task<TResource> MapAsync<T, TResource>(T source)
            where TResource : Resource
        {
            try
            {
                var resource = _mapper.Map<TResource>(source);
                await _linksService.AddLinksAsync(resource);
                return resource;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(
                    "A problem occured while trying to convert a type to a resource. " +
                    "Make sure that you have set up AutoMapper properly and your link policies don't " +
                    "contain any unexisting routes.",
                    e);
            }
        }

        /// <summary>
        /// Convert collection of models to <see cref="Resource"/>
        /// and applies links to it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResource"></typeparam>
        /// <typeparam name="TContainer"></typeparam>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<TContainer> MapContainerAsync<T, TResource, TContainer>(IEnumerable<T> models)
            where TResource : Resource
            where TContainer : ResourceContainer<TResource>, new()
        {
            var nestedResources = await Task.WhenAll(
                models.Select(MapAsync<T, TResource>));

            var container = new TContainer
            {
                Items = nestedResources
            };

            await _linksService.AddLinksAsync(container);

            return container;
        }

        public async Task<TResource> CreateEmptyResourceAsync<TResource>(Action<TResource> beforeMap = null)
            where TResource : Resource, new()
        {
            var resource = new TResource();
            beforeMap?.Invoke(resource);
            await _linksService.AddLinksAsync(resource);
            return resource;
        }
    }
}