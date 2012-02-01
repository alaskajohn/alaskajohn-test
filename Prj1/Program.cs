using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Web;
using System.Xml;

namespace myNamespace
{
    public class cRESTfulQuery
    {
        public static string Get_URI()
        {
            string uri2 = "http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php?whichClient=NDFDgen&lat=38.99&lon=-77.01&listLatLon=&lat1=&lon1=&lat2=&lon2=&resolutionSub=&listLat1=&listLon1=&listLat2=&listLon2=&resolutionList=&endPoint1Lat=&endPoint1Lon=&endPoint2Lat=&endPoint2Lon=&listEndPoint1Lat=&listEndPoint1Lon=&listEndPoint2Lat=&listEndPoint2Lon=&zipCodeList=&listZipCodeList=&centerPointLat=&centerPointLon=&distanceLat=&distanceLon=&resolutionSquare=&listCenterPointLat=&listCenterPointLon=&listDistanceLat=&listDistanceLon=&listResolutionSquare=&citiesLevel=&listCitiesLevel=&sector=&gmlListLatLon=&featureType=&requestedTime=&startTime=&endTime=&compType=&propertyName=&product=time-series&begin=2004-01-01T00%3A00%3A00&end=2016-01-20T00%3A00%3A00&Unit=e&maxt=maxt&Submit=Submit";
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri2);

            HttpWebResponse response = (HttpWebResponse)req.GetResponse(); //This does not exit cleanly if bad request.
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            string responseString = reader.ReadToEnd();

            reader.Close();
            responseStream.Close();
            response.Close();

            return responseString;

        }

        public static String function2(String xmlString)
        {
            StringBuilder output = new StringBuilder();

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                if (reader.ReadToFollowing("name"))
                    output.AppendLine(reader.ReadElementContentAsString());
                if (reader.ReadToFollowing("value"))
                    output.AppendLine(reader.ReadElementContentAsString());

            }
            return output.ToString();
        }
    }

    class Program
    {
        static void Main()
        {
            string xmlResult = cRESTfulQuery.Get_URI();
            string processedResult = cRESTfulQuery.function2(xmlResult);
            Console.WriteLine(processedResult);
            //Console.WriteLine(xmlResult);

            System.Threading.Thread.Sleep(20000);
        }
    }
}
