using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using(HotelEntities db = new HotelEntities())
            {
                Customer c = new Customer();
                c.Age = 100;
                c.Email = "someemail#mail.ru";
                c.FirstName = "Peter";
                c.LastName = "Pen";
                c.PassportID = 123456;
                c.Phone = "7-999-999-99-99";
                db.Customers.Add(c);

                //Customer m = new Customer();
                //m.Age = 26;
                //m.Email = "mikemail#mail.ru";
                //m.FirstName = "Mike";
                //m.LastName = "Ekim";
                //m.PassportID = 123456;
                //m.Phone = "7-944-341-24-12";
                //db.Customers.Add(m);



                db.SaveChanges();
            }

            using(HotelEntities db = new HotelEntities())
            {
                Customer p1 = db.Customers.Where((customer) => customer.FirstName == "Peter").FirstOrDefault();

                p1.Age = 300000;
                db.SaveChanges();
            }

            using(HotelEntities db = new HotelEntities())
            {
                Customer p1 = db.Customers.Where((customer) => customer.FirstName == "Peter").FirstOrDefault();
                if (p1 != null)
                {
                    db.Customers.Remove(p1);
                    db.SaveChanges();
                }
            }

            using(HotelEntities db = new HotelEntities())
            {
                Booking c = new Booking();
                c.ArrivalDate = new DateTime(2001, 01, 20);
                c.ArrivalTime = new TimeSpan(12, 30, 0);
                c.DepartureDate = new DateTime(2001, 01, 20);
                c.DepartureTime = new TimeSpan(12, 30, 0);
                c.CustomerId = db.Customers.Where(customer => customer.FirstName == "Mike").FirstOrDefault().Id;
                c.RoomId = db.Rooms.FirstOrDefault().Id;
                db.Bookings.Add(c);
                db.SaveChanges();
            }

            using(HotelEntities db = new HotelEntities())
            {
                var bookings = db.Bookings.Join(db.Customers,
                    booking => booking.CustomerId,
                    customer => customer.Id,
                    (booking, customer) => new
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Phone = customer.Phone,
                        ArrivalDate = booking.ArrivalDate,
                        DepartureDate = booking.DepartureDate,
                    });

                foreach (var b in bookings)
                {
                    Console.WriteLine("{0} {1} Phone: {2} ArrivalDate: {3} DepartureDate: {4}", b.FirstName, b.LastName, b.Phone, b.ArrivalDate, b.DepartureDate);
                }
            }
            Console.WriteLine();

            using(HotelEntities db = new HotelEntities())
            {
                var bookings = from booking in db.Bookings
                               join customer in db.Customers on booking.CustomerId equals customer.Id
                               join room in db.Rooms on booking.RoomId equals room.Id
                               select new
                               {
                                   Name = customer.FirstName,
                                   Price = room.Price,
                                   ArrivalDate = booking.ArrivalDate,
                                   DepartureDate = booking.DepartureDate
                               };
                foreach (var b in bookings)
                {
                    Console.WriteLine("Name: {0}\n\tPrice: {1}\n\tArrivalDate: {2}\n\tDepartureDate: {3}\n---------------------\n", b.Name, b.Price, b.ArrivalDate, b.DepartureDate);
                }
            }
            Console.ReadKey();
        }
    }
}
