using System;
using TomasHorvath.GoSms.API;

namespace TomasHorvath.GoSms.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			GoSmsConnector connector = new GoSmsConnector("https://app.gosms.cz", "8982_3csbm4ztivc40sc0o0gc04cos80gskkw4gswow0swoookkoskc", "5xvaf60bqk08os4cs0w84s0k4044sc08c4w8cgw88sokgs4wco");

			Console.Read();
		}
    }
}
