namespace DocsSamples;

public class StaticData
{
    public static Booking[] Bookings =>
        [
            new Booking(
                Id: "TRP-4821",
                Status: BookingStatus.Confirmed,
                Destination: "London, UK",
                Amount: 1249.99M
            ),
            new Booking(
                Id: "TRP-4822",
                Status: BookingStatus.Pending,
                Destination: "Kyoto, Japan",
                Amount: 2450.00M
            ),
            new Booking(
                Id: "TRP-4823",
                Status: BookingStatus.Confirmed,
                Destination: "Cancun, Mexico",
                Amount: 850.50M
            ),
            new Booking(
                Id: "TRP-4824",
                Status: BookingStatus.Confirmed,
                Destination: "Cape Town, SA",
                Amount: 1500.00M
            ),
            new Booking(
                Id: "TRP-4825",
                Status: BookingStatus.Cancelled,
                Destination: "Rome, Italy",
                Amount: 185.00M
            ),
        ];
}

public record Booking(string Id, BookingStatus Status, string Destination, decimal Amount);

public enum BookingStatus
{
    Confirmed,
    Pending,
    Cancelled,
}
