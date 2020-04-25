using System;
using System.Collections.Generic;
using System.Linq;
using TreniniDotNet.Application.Boundaries.Catalog.EditScale;
using TreniniDotNet.Common;

namespace TreniniDotNet.Application.InMemory.OutputPorts.Catalog
{
    public class EditScaleOutputPort : OutputPortTestHelper<EditScaleOutput>, IEditScaleOutputPort
    {
        private MethodInvocation<Slug> ScaleNotFoundMethod { set; get; }

        public EditScaleOutputPort()
        {
            ScaleNotFoundMethod = NewMethod<Slug>(nameof(ScaleNotFound));
        }

        public void ScaleNotFound(Slug slug)
        {
            ; ScaleNotFoundMethod = ScaleNotFoundMethod.Invoked(slug);
        }

        public override IEnumerable<IMethodInvocation> Methods
        {
            get
            {
                var methods = new List<IMethodInvocation>
                {
                    ScaleNotFoundMethod
                };

                return base.Methods.Concat(methods);
            }
        }
    }
}
