using WebApi.Hal.Interfaces;
using WebApi.Hal.Web.Api.Resources;

namespace WebApi.Hal.Web.Api.Linkers
{
    public class BeerLinker : IResourceLinker<BeerResource>
    {
        public void CreateLinks(BeerResource resource, IResourceLinker resourceLinker)
        {
            var selfLink = LinkTemplates.Beers.Beer.CreateLink(id => resource.Id);
            resource.Href = selfLink.Href;
            resource.Rel = selfLink.Rel;

            if (resource.StyleId != null)
                resource.Links.Add(LinkTemplates.BeerStyles.Style.CreateLink(id => resource.StyleId));
            if (resource.BreweryId != null)
                resource.Links.Add(LinkTemplates.Breweries.Brewery.CreateLink(id => resource.BreweryId));
        }
    }
}