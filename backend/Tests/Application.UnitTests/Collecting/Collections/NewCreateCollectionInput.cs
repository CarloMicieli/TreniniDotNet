using TreniniDotNet.Application.Collecting.Collections.CreateCollection;

namespace TreniniDotNet.Application.Collecting.Collections
{
    public static class NewCreateCollectionInput
    {
        public static readonly CreateCollectionInput Empty = With();

        public static CreateCollectionInput With(string owner = null, string notes = null) =>
            new CreateCollectionInput(owner, notes);
    }
}