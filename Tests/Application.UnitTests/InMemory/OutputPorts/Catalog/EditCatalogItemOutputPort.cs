using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.EditCatalogItem;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public class EditCatalogItemOutputPort : OutputPortTestHelper<EditCatalogItemOutput>, IEditCatalogItemOutputPort
    {
        public void CatalogItemNotFound(Slug slug)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
