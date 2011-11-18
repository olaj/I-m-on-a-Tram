using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using Geo.Positions;
using System.Device.Location;
using Vasttrafik.Classes;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;

namespace Vasttrafik.Data {

    public class StopLoadingEventArgs : EventArgs {
        public IEnumerable<Model.Stop> Results { get; private set; }

        public StopLoadingEventArgs(IEnumerable<Model.Stop> results) {
            Results = results;
        }
    }

    public class TripLoadingEventArgs : EventArgs {
        public IEnumerable<Model.Trip> Results { get; private set; }

        public TripLoadingEventArgs(IEnumerable<Model.Trip> results) {
            Results = results;
        }
    }

    public class VasttrafikAPI {
        public event EventHandler<StopLoadingEventArgs> StopLoadingComplete;
        public event EventHandler<TripLoadingEventArgs> TripLoadingComplete;

        public void GetImage(Action<Stream> callback) {
            WebClient client = new WebClient();

            client.OpenReadCompleted += (s, e) => {
                //string image = e.Result;
                callback(e.Result);
            };

            if (NetworkInterface.GetIsNetworkAvailable()) {
                client.OpenReadAsync(new Uri("http://vasttrafik.se/ResizeImageHandler.aspx?ImagePath=/upload/Linjekartor_hogupplost/dec2009/sparvagnskartadec2010.png&ImageHeight=1800"));
            } else {
                callback(null);
            }
        }

        public void GetStopsBySearch(string search, Action<IEnumerable<Model.Stop>> callback) {
            try {
                //var wc = new FastWebClient();
                FastWebClient client = new FastWebClient();

                client.DownloadStringCompleted += (s, e) => {
                    string xml = e.Result;
                    var stops = GetStops(xml);
                    callback(stops);
                };
                if (NetworkInterface.GetIsNetworkAvailable()) {
                    client.DownloadStringAsync(new Uri("http://vasttrafik.se/External_Services/TravelPlanner.asmx/GetAllSuggestions?identifier=ID&searchString=" + search + "&count=20"));
                } else {
                    callback(null);
                }

            } catch (Exception) {
            }
        }

        private static IEnumerable<Model.Stop> GetStops(string xml) {
            string xmlStops = XElement.Parse(xml).Value;
            XElement realXml = XElement.Parse(xmlStops);

            var stops = from stop in realXml.Descendants("item")
                        where stop.Attribute("stop_type").Value == "Stop"
                        select new Model.Stop {
                            name = stop.Element("stop_name").Value,
                            stop_id = int.Parse(stop.Attribute("stop_id").Value),
                            county = stop.Element("county").Value
                        };

            return stops;
        }


        public void GetStopsByLocation(double latitude, double longitude, Action<IEnumerable<Model.Stop>> callback) {

            WGS84Position coordinates = new WGS84Position(latitude, longitude);
            RT90Position rtPos = new RT90Position(coordinates, RT90Position.RT90Projection.rt90_2_5_gon_v);

            FastWebClient client = new FastWebClient();

            client.DownloadStringCompleted += (s, e) => {
                string xml = e.Result;

                var stops = GetStops(xml);

                callback(stops);

            };

            if (NetworkInterface.GetIsNetworkAvailable()) {
                client.DownloadStringAsync(new Uri("http://vasttrafik.se/External_Services/TravelPlanner.asmx/GetStopListBasedOnCoordinate?identifier=ID&xCoord=" + rtPos.Latitude + "&yCoord=" + rtPos.Longitude));
            } else {
                callback(null);
            }
        }

        public void GetNextTrips(string stop_id, Action<IEnumerable<Model.Trip>> callback) {

            FastWebClient client = new FastWebClient();

            //WebClient nextTripClient = new WebClient();

            client.DownloadStringCompleted += (s, e) => {
                XNamespace ns = "http://vasttrafik.se/";
                string xmlStops = XElement.Parse(e.Result).Value;
                XElement realXml = XElement.Parse(xmlStops);


                var trips = from stop in realXml.Descendants("item")
                            where stop.Element("destination") != null
                            select new Model.Trip {
                                Destination = stop.Element("destination").Value ?? "",
                                NextTripMinutes = int.Parse(stop.Attribute("next_trip").Value),
                                NextTripDateTime = DateTime.Parse(stop.Attribute("next_trip_planned_time").Value),
                                LineForegroundColor = stop.Attribute("line_number_foreground_color").Value ?? "",
                                LineBackgroundColor = stop.Attribute("line_number_background_color").Value ?? "",
                                LineImage = stop.Attribute("line_image").Value ?? "",
                                //NextTripImage = stop.Attribute("next_trip_image").Value ?? "",
                                Line = stop.Attribute("line_number").Value ?? ""
                            };

                //if (TripLoadingComplete != null) {
                //    TripLoadingComplete(this, new TripLoadingEventArgs(trips.OrderBy(x => x.NextTripMinutes)));
                //    }
                callback(trips.OrderBy(x => x.NextTripMinutes));
            };

            if (NetworkInterface.GetIsNetworkAvailable()) {
                client.DownloadStringAsync(new Uri("http://vasttrafik.se/External_Services/NextTrip.asmx/GetForecast?identifier=ID&stopId=" + stop_id));
            } else {
                callback(null);
            }
        }

        //void stop_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {


        //    //stopCollection = ;
        //}

        //void trip_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
        //    XNamespace ns = "http://vasttrafik.se/";
        //    string xmlStops = XElement.Parse(e.Result).Value;
        //    XElement realXml = XElement.Parse(xmlStops);


        //    var trips = from stop in realXml.Descendants("item")
        //                where stop.Element("destination") != null
        //                select new Model.Trip {
        //                    Destination = stop.Element("destination").Value ?? "",
        //                    NextTripMinutes = int.Parse(stop.Attribute("next_trip").Value),
        //                    NextTripDateTime = DateTime.Parse(stop.Attribute("next_trip_planned_time").Value),
        //                    LineForegroundColor = stop.Attribute("line_number_foreground_color").Value ?? "",
        //                    LineBackgroundColor = stop.Attribute("line_number_background_color").Value ?? "",
        //                    LineImage = stop.Attribute("line_image").Value ?? "",
        //                    //NextTripImage = stop.Attribute("next_trip_image").Value ?? "",
        //                    Line = stop.Attribute("line_number").Value ?? ""
        //                };

        //    if (TripLoadingComplete != null) {
        //        TripLoadingComplete(this, new TripLoadingEventArgs(trips.OrderBy(x => x.NextTripMinutes)));
        //    }
        //    //listTrips.ItemsSource = test.OrderBy(x => x.NextTripMinutes);
        //}

        //public void GetRoute(string stopid1, string stopid2) {
        //    try {
        //        ///External_Services/TravelPlanner.asmx/GetRoute?identifier=string&fromId=string&toId=string&dateTimeTravel=string&whenId=string&priorityId=string&numberOfResultBefore=string&numberOfResulsAfter=string
        //        //WebClient routeClient = new WebClient();
        //        FastWebClient searchStopClient = new FastWebClient();

        //        searchStopClient.DownloadStringCompleted += (s, e) => {
        //        };

        //        //routeClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(routeClient_DownloadStringCompleted);
        //        routeClient.DownloadStringAsync(new Uri("http://vasttrafik.se/External_Services/TravelPlanner.asmx/GetRoute?identifier=7e9fca85-a149-410d-a78a-58c5f2cb1fce&fromId=" + stopid1 + "&toId=" + stopid2 + "&dateTimeTravel=" + DateTime.Now + "&whenId=1&priorityId=1&numberOfResultBefore=0&numberOfResulsAfter=6"));
        //    } catch (Exception) {
        //    }
        //}

        //void routeClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e) {
        //    string xml = e.Result;

        //    string xmlStops = XElement.Parse(xml).Value;
        //    XElement realXml = XElement.Parse(xmlStops);
        //}

        internal void GetFavorites(Action<IEnumerable<Model.Stop>> callback) {

            var stops = (App.Current as App).UserFavorites;

            callback(stops);

            //if (StopLoadingComplete != null) {
            //    StopLoadingComplete(this, new StopLoadingEventArgs(stops));
            //}
        }
    }
}
