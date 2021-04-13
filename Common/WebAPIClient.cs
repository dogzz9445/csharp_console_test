using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;

namespace WebAPIClient
{
    public class WebAPIClient
    {
        protected readonly HttpClient Request;
        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        protected WebAPIClient()
        {
            Request = new HttpClient();
        }

        protected async Task<T> GetAsync<T>(string uri) where T: new()
        {
            T result;
            try
            {
                using var httpResponse = await Request.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);

                httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    using var streamReader = new StreamReader(contentStream);
                    using var jsonReader = new JsonTextReader(streamReader);

                    JsonSerializer serializer = new JsonSerializer();

                    result = serializer.Deserialize<T>(jsonReader);
                }
                else
                {
                    Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                }
            }
            catch(JsonReaderException)
            {
                Console.WriteLine("Error: Invalid JSON.");
            } 
            catch(HttpRequestException)
            {
                Console.WriteLine("Error: Failed to HTTP request.");	
            }
            catch(TaskCanceledException)
            {
                Console.WriteLine("Error: Task cancled");	
            }

            RaiseMessageReceived(this, new MessageReceivedEventArgs());
            return result == null ? default(T) : result;
        }

        protected RaiseMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            MessageReceived?.Invoke(sender, args);
        }
    }
    
    public class MessageReceivedEventArgs : EventArgs
    {
        private HttpResponseMessage responseMessage;
        public MessageReceivedEventArgs(HttpResponseMessage responseMessage)
        {
            ResponseMessage = responseMessage;
        }

        public HttpResponseMessage ResponseMessage 
        { 
            get => responseMessage; 
            set => responseMessage = value; 
        }
    }

}