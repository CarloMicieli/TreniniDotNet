namespace TreniniDotNet.Domain.Catalog.ValueObjects
{
    /// <summary>
    /// In rail transport, track gauge or track gage is the spacing of the rails on a 
    /// railway track and is measured between the inner faces of the load-bearing rails. 
    /// </summary>
    public enum TrackGauge
    {
        /// <summary>
        /// In common usage the term "standard gauge" refers to 1,435 mm (4 ft 8 1⁄2 in)
        /// </summary>
        Standard,

        /// <summary>
        /// In modern usage, broad gauge generally refers to track spaced significantly 
        /// wider than 1,435 mm (4 ft 8 1⁄2 in). 
        /// </summary>
        Broad,

        Medium,

        /// <summary>
        /// As the gauge of a railway is reduced the costs of construction can be reduced since narrow 
        /// gauges allow smaller-radius curves, allowing obstacles to be avoided rather than having to be 
        /// built over or through (valleys and hills)
        /// </summary>
        Narrow,
    }
}
