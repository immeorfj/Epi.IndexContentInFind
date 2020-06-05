using System.Collections.Generic;
using EPiServer.Core;
using EPiServer.Find.Cms;

namespace Geta.Epi.IndexContentInFind
{
    public interface IIndexService
    {
        IEnumerable<ContentIndexingResult> Index(ContentReference contentLink);
        IEnumerable<ContentIndexingResult> IndexFrom(ContentReference contentLink);
        IEnumerable<ContentIndexingResult> IndexVariations(ContentReference contentLink);
    }
}