namespace LanguageExt
{
    public static class OptionExtensions
    {
        public static TValue? AsNullaleRef<TValue>(this Option<TValue> opt)
            where TValue : class
        {
            if (opt.IsNone)
            {
                return null;
            }
            else
            {
                return opt.IfNone(() => throw new System.InvalidOperationException("Option:AsNullaleRef() on a None value"));
            }
        }

        public static TValue? AsNullale<TValue>(this Option<TValue> opt)
            where TValue : struct
        {
            if (opt.IsNone)
            {
                return null;
            }
            else
            {
                return opt.IfNone(() => throw new System.InvalidOperationException("Option:AsNullaleRef() on a None value"));
            }
        }
    }
}