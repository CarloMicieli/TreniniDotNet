using LanguageExt;
using static LanguageExt.Prelude;

namespace TreniniDotNet.Common
{
    public class Error : NewType<Error, string>
    {
        public Error(string e) : base(e) { }
    }
}