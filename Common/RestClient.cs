using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using RateLimiter;
using ComposableAsync;

namespace RestClient
{
    public class RestClient
    {
        protected readonly HttpClient Request;

        public RestClient(int minuteRate, int secondRate)
        {
            // var hourConstraint = new CountByIntervalAwaitableConstraint(36000, TimeSpan.FromHours(1));
            var minuteConstraint = new CountByIntervalAwaitableConstraint(minuteRate, TimeSpan.FromMinutes(1));
            var secondConstraint = new CountByIntervalAwaitableConstraint(secondRate, TimeSpan.FromSeconds(1));
            
            var handler = TimeLimiter.Compose(minuteConstraint, secondConstraint).AsDelegatingHandler();
            
            // var handler = TimeLimiter
            //         .GetFromMaxCountByInterval(10, TimeSpan.FromSeconds(1))
            //         .AsDelegatingHandler();

            Request = new HttpClient(handler);
        }

        protected async Task<T> GetAsync<T>(string uri) where T: new()
        {
            T result = default(T);
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
            catch (NotSupportedException)
            {
                Console.WriteLine("Error: The content type is not supported.");
            }
            catch(TaskCanceledException)
            {
                Console.WriteLine("Error: Task cancled");	
            }

            RaiseMessageReceived(this, new MessageReceivedEventArgs());
            return result;
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        protected void RaiseMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            MessageReceived?.Invoke(sender, args);
        }
    }
    
    public class MessageReceivedEventArgs : EventArgs
    {
        private HttpResponseMessage responseMessage;
        public MessageReceivedEventArgs()
        {
        }

        public HttpResponseMessage ResponseMessage 
        { 
            get => responseMessage; 
            set => responseMessage = value; 
        }
    }

}