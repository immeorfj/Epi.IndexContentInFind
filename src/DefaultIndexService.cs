using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using EPiServer;
using EPiServer.Commerce.Catalog.ContentTypes;
using EPiServer.Commerce.Catalog.Linking;
using EPiServer.Core;
using EPiServer.Find.Cms;
using EPiServer.ServiceLocation;

namespace Geta.Epi.IndexContentInFind
{
    [ServiceConfiguration(typeof(IIndexService), Lifecycle = ServiceInstanceScope.Hybrid)]
    public class DefaultIndexService : IIndexService
    {
        protected readonly IContentLoader ContentLoader;
        protected readonly IContentIndexer ContentIndexer;
        protected readonly IRelationRepository RelationRepository;

        public DefaultIndexService(IContentLoader contentLoader, IContentIndexer contentIndexer, IRelationRepository relationRepository)
        {
            ContentLoader = contentLoader ?? throw new ArgumentNullException(nameof(contentLoader));
            ContentIndexer = contentIndexer ?? throw new ArgumentNullException(nameof(contentIndexer));
            RelationRepository = relationRepository ?? throw new ArgumentNullException(nameof(relationRepository));
        }

        public virtual IEnumerable<ContentIndexingResult> Index(ContentReference contentLink)
        {
            var content = ContentLoader.Get<IContent>(contentLink);
            return ContentIndexer.Index(content);
        }

        public virtual IEnumerable<ContentIndexingResult> IndexFrom(ContentReference contentLink)
        {
            var mainContent = ContentLoader.Get<IContent>(contentLink);
            var contentReferencesToIndex = ContentLoader.GetDescendents(contentLink);
            var contentsToIndex = ContentLoader.GetItems(contentReferencesToIndex, CultureInfo.InvariantCulture).ToList();

            // Add main content to list
            contentsToIndex.Insert(0, mainContent);

            return ContentIndexer.Index(contentsToIndex);
        }

        public virtual IEnumerable<ContentIndexingResult> IndexVariations(ContentReference contentLink)
        {
            var contentReferencesToIndex = new List<ContentReference>() { contentLink };
            contentReferencesToIndex.AddRange(RelationRepository.GetChildren<ProductVariation>(contentLink).Select(x => x.Child));

            var contentsToIndex = ContentLoader.GetItems(contentReferencesToIndex, CultureInfo.InvariantCulture).ToList();
            return ContentIndexer.Index(contentsToIndex);
        }
    }
}