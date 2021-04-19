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

            Request = new HttpClient(handler);
        }

        protected async Task<T> GetAsync<T>(string uri) where T: new()
        {
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

                    T result = serializer.Deserialize<T>(jsonReader);
                    RaiseMessageReceived(this, new MessageReceivedEventArgs(result.GetType(), result));
                    return result;
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
            return default(T);
        }

        public event EventHandler<MessageReceivedEventArgs> MessageReceived;

        protected void RaiseMessageReceived(object sender, MessageReceivedEventArgs args)
        {
            MessageReceived?.Invoke(sender, args);
        }
    }
    
    public class MessageReceivedEventArgs : EventArgs
    {
        private object responseMessage;
        private Type responseType;

        public MessageReceivedEventArgs(Type type, object message)
        {
            responseMessage = message;
            responseType = type;
        }
        public object ResponseMessage 
        { 
            get => responseMessage; 
            set => responseMessage = value; 
        }
        public Type ResponseType { get => responseType; set => responseType = value; }
    }

}