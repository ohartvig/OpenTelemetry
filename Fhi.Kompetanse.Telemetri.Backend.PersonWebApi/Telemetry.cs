using System.Diagnostics;
using System.Diagnostics.Metrics;

namespace Fhi.Kompetanse.Telemetri.Backend.PersonWebApi
{
    public static class Telemetry
    {
        //...

        // Name it after the service name for your app.
        // It can come from a config file, constants file, etc.
        public static readonly ActivitySource MyActivitySource = new("PersonWebApi");

        public static Meter greeterMeter= new Meter("Jan.Example", "1.0.0");
        public static  Counter<int> CountCreatePerson = greeterMeter.CreateCounter<int>("CreatePerson.count", description: "Counts the number of CreatePerson calls");


        //...
    }
}
