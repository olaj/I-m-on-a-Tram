using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using Vasttrafik.Classes;

namespace Vasttrafik.Model {
    public class Trip : INotifyPropertyChanged {

        private int nextTripMinutes;
        public int NextTripMinutes {
            get { return nextTripMinutes; }
            set {
                nextTripMinutes = value;
                OnPropertyChanged("NextTripMinutes");
                OnPropertyChanged("DisplayNextTrip");
            }
        }

        private string line;
        public string Line {
            get { return line; }
            set {
                line = value;
                OnPropertyChanged("Line");
            }
        }

        private string destination;
        public string Destination {
            get { return destination; }
            set {
                destination = value;
                OnPropertyChanged("Destination");
            }
        }

        private string lineForegroundColor;
        public string LineForegroundColor {
            get { return lineForegroundColor; }
            set {
                lineForegroundColor = value;
                OnPropertyChanged("LineForegroundColor");

            }
        }

        private string lineBackgroundColor;
        public string LineBackgroundColor {
            get { return lineBackgroundColor; }
            set {
                lineBackgroundColor = value;
                OnPropertyChanged("LineBackgroundColor");
            }
        }

        private string lineImage;
        public string LineImage {
            get { return lineImage; }
            set {
                lineImage = value;
                OnPropertyChanged("LineImage");
            }
        }

        private DateTime nextTripDateTime;
        public DateTime NextTripDateTime {
            get { return nextTripDateTime; }
            set {
                nextTripDateTime = value;
                OnPropertyChanged("NextTripDateTime");
            }
        }

        public string LineBorderBackgroudColor {
            get {
                Color c = lineBackgroundColor.hexToColor();

                if (c.B > 125)
                    c.B = Convert.ToByte(c.B - 30);
                else
                    c.B = Convert.ToByte(c.B + 30);

                if (c.G > 125)
                    c.G = Convert.ToByte(c.G - 30);
                else
                    c.G = Convert.ToByte(c.G + 30);

                if (c.R > 125)
                    c.R = Convert.ToByte(c.R - 30);
                else
                    c.R = Convert.ToByte(c.R + 30);

                //c.A = Convert.ToByte(c.A - 30);

                return c.ToString();
            }
        }




        public string DisplayNextTrip {
            get {
                if (NextTripMinutes == 0) {
                    return "Nu";
                } else {
                    return NextTripMinutes.ToString();
                }
            }
        }

        protected void OnPropertyChanged(string propertyName) {
            if (PropertyChanged != null) {
                var args = new PropertyChangedEventArgs(propertyName);
                PropertyChanged(this, args);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
