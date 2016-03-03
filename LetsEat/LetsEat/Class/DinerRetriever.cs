using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace LetsEat
{
    class DinerRetriever
    {
        public List<ListItem> searchDiner(BasicGeoposition pos)
        {
            //List<ListItem> diners = new List<ListItem>();
            //var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/json?origin={0},{1}&address=restaurant&sensor=false", Uri.EscapeDataString(pos.Latitude.ToString()), Uri.EscapeDataString(pos.Longitude.ToString()), Uri.EscapeDataString(address));

            //var request = WebRequest.Create(requestUri);
            //var response = request.GetResponseAsync();

            //string json = JsonConvert.DeserializeObject(response);

            //foreach ()
            return null;
        }
    }
}
