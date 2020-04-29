using AutoMapper;
using TreniniDotNet.Application.Collecting.Collections.AddItemToCollection;
using TreniniDotNet.Application.Collecting.Collections.CreateCollection;
using TreniniDotNet.Application.Collecting.Collections.EditCollectionItem;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionByOwner;
using TreniniDotNet.Application.Collecting.Collections.GetCollectionStatistics;
using TreniniDotNet.Application.Collecting.Collections.RemoveItemFromCollection;
using TreniniDotNet.Web.Collecting.V1.Collections.AddItemToCollection;
using TreniniDotNet.Web.Collecting.V1.Collections.CreateCollection;
using TreniniDotNet.Web.Collecting.V1.Collections.EditCollectionItem;
using TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionByOwner;
using TreniniDotNet.Web.Collecting.V1.Collections.GetCollectionStatistics;
using TreniniDotNet.Web.Collecting.V1.Collections.RemoveItemFromCollection;

namespace TreniniDotNet.Web.Collecting.V1.Collections
{
    public class CollectionsMapperProfile : Profile
    {
        public CollectionsMapperProfile()
        {
            CreateMap<CreateCollectionRequest, CreateCollectionInput>();
            CreateMap<AddItemToCollectionRequest, AddItemToCollectionInput>();
            CreateMap<GetCollectionByOwnerRequest, GetCollectionByOwnerInput>();
            CreateMap<GetCollectionStatisticsRequest, GetCollectionStatisticsInput>();
            CreateMap<EditCollectionItemRequest, EditCollectionItemInput>();
            CreateMap<RemoveItemFromCollectionRequest, RemoveItemFromCollectionInput>();
        }
    }
}