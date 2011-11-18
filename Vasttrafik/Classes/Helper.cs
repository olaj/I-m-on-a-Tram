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
using System.Collections.Generic;
using Vasttrafik.Model;
using System.Xml.Linq;
using System.Linq;
using System.IO.IsolatedStorage;
using System.Collections.ObjectModel;

namespace Vasttrafik.Classes {
    public static class Helper {
        //public static IEnumerable<TramStop> GetStops(string xml) {

        //}
        public static Color InterpolateColors(Color color1, Color color2, float percentage) {
            double a1 = color1.A / 255.0, r1 = color1.R / 255.0, g1 = color1.G / 255.0, b1 = color1.B / 255.0;
            double a2 = color2.A / 255.0, r2 = color2.R / 255.0, g2 = color2.G / 255.0, b2 = color2.B / 255.0;

            byte a3 = Convert.ToByte((a1 + (a2 - a1) * percentage) * 255);
            byte r3 = Convert.ToByte((r1 + (r2 - r1) * percentage) * 255);
            byte g3 = Convert.ToByte((g1 + (g2 - g1) * percentage) * 255);
            byte b3 = Convert.ToByte((b1 + (b2 - b1) * percentage) * 255);
            return Color.FromArgb(a3, r3, g3, b3);
        }


    }

    public static class ExtensionMethods {



        public static Color hexToColor(this string hexValue) {
            try {
                hexValue = hexValue.Replace("#", "");
                byte position = 0;
                byte alpha = System.Convert.ToByte("ff", 16);

                if (hexValue.Length == 8) {
                    // get the alpha channel value
                    alpha = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                    position = 2;
                }

                // get the red value
                byte red = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                position += 2;

                // get the green value
                byte green = System.Convert.ToByte(hexValue.Substring(position, 2), 16);
                position += 2;

                // get the blue value
                byte blue = System.Convert.ToByte(hexValue.Substring(position, 2), 16);

                // create the Color object
                Color color = Color.FromArgb(alpha, red, green, blue);

                // create the SolidColorBrush object
                return color;
            } catch {
                return Color.FromArgb(255, 251, 237, 187);
            }
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> coll) {
            var c = new ObservableCollection<T>();
            foreach (var e in coll) c.Add(e);
            return c;
        }
    }
}
