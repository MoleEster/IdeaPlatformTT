using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnyLogicTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Ticket> data;

            List<TimeSpan> time;
            using (StreamReader reader = new StreamReader("tickets.json"))
            {
                string text = reader.ReadToEnd();
                data = JsonConvert.DeserializeObject<TicketsArray>(text).tickets.ToList();
            }

            time = new List<TimeSpan>();
            DateTime middle = new DateTime();
            foreach (Ticket item in data)
            {
                middle += DateTime.Parse(item.arrival_date + " " + item.arrival_time) - DateTime.Parse(item.departure_date + " " + item.departure_time);
                time.Add(DateTime.Parse(item.arrival_date + " " + item.arrival_time) - DateTime.Parse(item.departure_date + " " + item.departure_time));
            }
            time = time.OrderBy(x => x).ToList<TimeSpan>();
            int a = ((middle.Day - 1) * 24 * 60 + middle.Hour * 60 + middle.Minute) / data.Count;
            Console.WriteLine("Среднее время полета: " + $"{a / 60}:{a % 60}:00");
            Console.WriteLine("90-й процентиль: " + time[Convert.ToInt32(Math.Round(value: time.Count / 10, MidpointRounding.AwayFromZero))]);
            Console.ReadLine();
        }
    }

    public class Ticket
    {
        public string departure_date { get; set; }
        public string departure_time { get; set; }
        public string arrival_date { get; set; }
        public string arrival_time { get; set; }
    }
    public class TicketsArray
    {
        public IList<Ticket> tickets { get; set; }
    }
}
