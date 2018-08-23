using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using BeaconLib;

namespace UnityCE.DeviceManagment
{
    class DiscoveryService
    {

        private Beacon beacon;
        private Probe probe;

       public void InitDiscoveryService()
        {
            beacon = new Beacon("myApp", 1234);
            beacon.BeaconData = "My Application Server on " + Dns.GetHostName();
            beacon.Start();
            Console.WriteLine("Server Started");
            //SearchInit();
        }

        public void SearchInit()
        {
            probe = new Probe("myApp");
            // Event is raised on separate thread so need synchronization
            probe.BeaconsUpdated += Probe_BeaconsUpdated;

            probe.Start();
            Console.WriteLine("Searching");
        }

        private void Probe_BeaconsUpdated(IEnumerable<BeaconLocation> obj)
        {
            foreach (var beaconLocation in obj)
            {
                Console.WriteLine(beaconLocation.Data);
            }
        }

        ~DiscoveryService()
        {
            beacon.Stop();
            probe.Stop();
        }

    }
}
