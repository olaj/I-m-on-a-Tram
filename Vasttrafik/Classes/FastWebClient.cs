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
using System.IO;
using System.ComponentModel;

namespace Vasttrafik.Classes {
    /// <summary>
    /// Simple implementation of a helpful web client wrapper that does a 
    /// majority of its work on the background thread as opposed to the standard
    /// WebClient implementation that favors the user interface thread for all
    /// of its work.
    /// </summary>
    public class FastWebClient {
        /// <summary>
        /// The HTTP web request instance.
        /// </summary>
        private HttpWebRequest _webRequest;

        /// <summary>
        /// Download string completed event.
        /// </summary>
        public event EventHandler<DownloadStringCompletedEventArgs> DownloadStringCompleted;

        /// <summary>
        /// Begins an asynchronous download of a string from a URI resource.
        /// </summary>
        /// <param name="uri">The URI to download from.</param>
        public void DownloadStringAsync(Uri uri) {
            _webRequest = (HttpWebRequest)WebRequest.Create(uri);
            _webRequest.AllowReadStreamBuffering = true;
            _webRequest.BeginGetResponse(BeginResponse, null);
        }

        private void BeginResponse(IAsyncResult ar) {
            string responseStr = null;
            var handler = DownloadStringCompleted;

            try {
                using (WebResponse webResponse = _webRequest.EndGetResponse(ar)) {
                    using (var sr = new StreamReader(webResponse.GetResponseStream())) {
                        responseStr = sr.ReadToEnd();
                    }
                }
            } catch (Exception e) {
                if (handler != null) {
                    handler(this, new DownloadStringCompletedEventArgs(null, e));
                }

                return;
            }

            if (handler != null) {
                Exception error = null;
                string result = responseStr;
                if (string.IsNullOrEmpty(responseStr)) {
                    error = new Exception("Nothing came back from the server-side.");
                }
                handler(this, new DownloadStringCompletedEventArgs(result, error));
            }
        }
    }

    public class DownloadStringCompletedEventArgs : AsyncCompletedEventArgs {
        public DownloadStringCompletedEventArgs(string result,
                                                Exception ex)
            : base(ex, false, null) {
            Result = result;
        }

        public string Result { get; private set; }
    }
}
