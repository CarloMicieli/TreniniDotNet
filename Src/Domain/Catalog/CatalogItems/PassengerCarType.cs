namespace TreniniDotNet.Domain.Catalog.CatalogItems
{
    public enum PassengerCarType
    {
        /// <summary>
        /// An "open coach" has a central aisle; the car's interior is often filled
        /// with row upon row of seats as in a passenger airliner.
        /// </summary>
        OpenCoach,

        /// <summary>
        /// "closed" coaches or "compartment" cars have a side corridor to connect individual
        /// compartments along the body of the train, each with two rows of seats facing each other.
        /// </summary>
        CompartmentCoach,

        /// <summary>
        /// A dining car (or diner) is used to serve meals to the passengers.
        /// </summary>
        DiningCar,

        /// <summary>
        /// Lounge cars carry a bar and public seating.
        /// </summary>
        Lounge,

        /// <summary>
        /// The observation car almost always operated as the last car in a passenger train, in US practice.
        /// Its interior could include features of a coach, lounge, diner, or sleeper.
        ///
        /// The main spotting feature was at the tail end of the car.
        /// </summary>
        Observation,

        /// <summary>
        /// Often called "sleepers" or "Pullman cars", these cars provide sleeping arrangements
        /// for passengers travelling at night.
        ///
        /// Early models were divided into sections, where coach seating converted at night into semi-private berths.
        /// </summary>
        SleepingCar,

        /// <summary>
        /// The baggage car is a car that was normally placed between the train's motive power and the remainder
        /// of the passenger train.
        ///
        /// The car's interior is normally wide open and is used to carry passengers' checked baggage.
        /// </summary>
        BaggageCar,

        /// <summary>
        ///
        /// </summary>
        DoubleDecker,

        /// <summary>
        ///
        /// </summary>
        CombineCar,

        /// <summary>
        ///
        /// </summary>
        DrivingTrailer,

        /// <summary>
        ///
        /// </summary>
        RailwayPostOffice
    }
}
