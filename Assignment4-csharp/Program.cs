using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Assignment4_csharp.Seat;

namespace Assignment4_csharp
{
        public enum SeatLabel { A, B, C, D }

        public class Seat
        {
            public SeatLabel Label { get; private set; }
            public bool IsBooked { get; private set; }
            public string Passenger { get; private set; }

            public Seat(SeatLabel label)
            {
                Label = label;
                IsBooked = false;
                Passenger = null;
            }

            public void BookSeat(string passenger)
            {
                IsBooked = true;
                Passenger = passenger;
            }

            public void ReleaseSeat()
            {
                IsBooked = false;
                Passenger = null;
            }
        }

        public class Passenger
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public SeatLabel? Preference { get; set; }
            public string SeatNumber { get; set; }

            public string GetInitials()
            {
                return $"{FirstName[0]}{LastName[0]}";
            }
        }

        public class Plane
        {
            private List<List<Seat>> seats;

            public Plane()
            {
                InitializeSeats();
            }

            private void InitializeSeats()
            {
                seats = new List<List<Seat>>();
                for (int i = 0; i < 12; i++)
                {
                    var row = new List<Seat>();
                    for (int j = 0; j < 4; j++)
                    {
                        row.Add(new Seat((SeatLabel)j));
                    }
                    seats.Add(row);
                }
            }

            public string BookTicket(Passenger passenger)
            {
                foreach (var row in seats)
                {
                    if (passenger.Preference.HasValue)
                    {
                        if (passenger.Preference == SeatLabel.A || passenger.Preference == SeatLabel.D)
                        {
                            if (!row[(int)SeatLabel.A].IsBooked)
                            {
                                row[(int)SeatLabel.A].BookSeat(passenger.GetInitials());
                                passenger.SeatNumber = $"{seats.IndexOf(row) + 1} {SeatLabel.A}";
                                return $"The window seat located in {passenger.SeatNumber} has been booked.";
                            }
                            if (!row[(int)SeatLabel.D].IsBooked)
                            {
                                row[(int)SeatLabel.D].BookSeat(passenger.GetInitials());
                                passenger.SeatNumber = $"{seats.IndexOf(row) + 1} {SeatLabel.D}";
                                return $"The window seat located in {passenger.SeatNumber} has been booked.";
                            }
                        }
                    }
                    foreach (var seat in row)
                    {
                        if (!seat.IsBooked)
                        {
                            seat.BookSeat(passenger.GetInitials());
                            passenger.SeatNumber = $"{seats.IndexOf(row) + 1} {seat.Label}";
                            return $"The seat located in {passenger.SeatNumber} has been booked.";
                        }
                    }
                }
                return "Sorry, the plane is fully booked.";
            }

            public void PrintSeatingChart()
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(seats[i][j].IsBooked ? $"{seats[i][j].Passenger} " : $"{((SeatLabel)j).ToString()} ");
                    }
                    Console.WriteLine();
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Plane plane = new Plane();

                while (true)
                {
                    Console.WriteLine("\nPlease enter 1 to book a ticket.");
                    Console.WriteLine("Please enter 2 to see seating chart.");
                    Console.WriteLine("Please enter 3 to exit the application.");

                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            Console.WriteLine("Please enter the passenger's first name:");
                            string firstName = Console.ReadLine();
                            Console.WriteLine("Please enter the passenger's last name:");
                            string lastName = Console.ReadLine();
                            Console.WriteLine("Please enter 1 for a Window seat preference, 2 for an Aisle seat preference, or hit enter to pick first available seat:");
                            string preferenceInput = Console.ReadLine();
                            SeatLabel? preference = null;
                            if (!string.IsNullOrEmpty(preferenceInput))
                            {
                                preference = (SeatLabel)Enum.Parse(typeof(SeatLabel), preferenceInput);
                            }
                            Passenger passenger = new Passenger
                            {
                                FirstName = firstName,
                                LastName = lastName,
                                Preference = preference
                            };
                            Console.WriteLine(plane.BookTicket(passenger));
                            break;

                        case "2":
                            Console.WriteLine("Current Seating Chart:");
                            plane.PrintSeatingChart();
                            break;

                        case "3":
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Invalid input. Please enter a valid option.");
                            break;
                    }
                }
            }
        }
    }


