using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProductivityTools.SportsTracker.SDK.Exceptions;
using ProductivityTools.SportsTracker.SDK.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace ProductivityTools.SportsTracker.SDK
{
    public class SportsTracker
    {
        string UserName, Password;
        bool Logging;
        private string Address = "https://api.sports-tracker.com/apiserver/v1/";

        HttpClient client;

        HttpClient AnonymousClient
        {
            get
            {
                if (client == null)
                {
                    if (Logging)
                    {
                        client = new HttpClient(new LoggingHandler(new HttpClientHandler()));
                    }
                    else
                    {
                        client = new HttpClient();
                    }

                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                }
                return client;
            }
        }

        HttpClient Client
        {
            get
            {
                if (AnonymousClient.DefaultRequestHeaders.Contains("STTAuthorization") == false)
                {
                    string sessionKey = Login(this.UserName, this.Password);
                    client.DefaultRequestHeaders.Add("STTAuthorization", sessionKey);
                }

                return AnonymousClient;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username">User name to the Sports-Tracker page</param>
        /// <param name="password">Password to the Sports-Tracker page</param>
        /// <param name="logging">When set to true it will print all webrequest content to the console</param>
        public SportsTracker(string username, string password, bool logging = false)
        {
            this.UserName = username;
            this.Password = password;
            this.Logging = logging;
        }

        private Uri GetUri(string end)
        {
            Uri url = new Uri($"{Address}{end.Trim('/')}");
            return url;
        }

        public string Login(string login, string password)
        {

            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("l", login),
                new KeyValuePair<string, string>("p", password)
            });

            var anonymous = new { l = login, p = password };
            var dataAsString = JsonConvert.SerializeObject(anonymous);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/x-www-form-urlencoded");

            HttpResponseMessage response = AnonymousClient.PostAsync(GetUri("login"), formContent).Result;
            var resultAsString = response.Content.ReadAsStringAsync().Result;
            JObject jobject = (JObject)JsonConvert.DeserializeObject(resultAsString);
            if (jobject["error"] != null)
            {
                string code = jobject["error"]["code"].ToString();
                if (code == "403")
                {
                    throw new ForbiddenException("403 - Wrong user name or password");
                }
            }
            string sessionKey = jobject["sessionkey"].ToString();
            return sessionKey;
        }

        public List<Training> GetTrainingList()
        {
            var trainings = new List<Training>();
            string resultAsString = Client.GetAsync(GetUri("workouts?limited=true&limit=1000000")).Result.Content.ReadAsStringAsync().Result;
            var jobject = JsonConvert.DeserializeObject<ProductivityTools.SportsTracker.SDK.DTO.TrainingList.Rootobject>(resultAsString);
            foreach (var sttraining in jobject.payload)
            {
                var training = new Training(sttraining);
                trainings.Add(training);
            }
            return trainings;
        }

        public string AddTraining(Training training)
        {
            string r = AddTraining(training, null, null);
            return r;
        }

        public string AddTraining(Training training, byte[] gpxFile)
        {
            string r = AddTraining(training, gpxFile, null);
            return r;
        }

        public string AddTraining(Training training, List<byte[]> image)
        {
            string r = AddTraining(training, null, image);
            return r;
        }

        public string AddTraining(Training training, byte[] gpxFile, List<byte[]> image)
        {
            string result = null;

            var addTraining = new ProductivityTools.SportsTracker.SDK.DTO.ImportTraining.Training();
            addTraining.activityId = (int)training.TrainingType;
            addTraining.description = training.Description;
            addTraining.energyConsumption = training.EnergyConsumption;
            addTraining.sharingFlags = training.SharingFlags;
            addTraining.startTime = training.StartTime;
            addTraining.totalDistance = training.TotalDistance;


            if (gpxFile != null && gpxFile.Length > 0)
            {
                string workoutKey = ImportGpxFile(gpxFile);
                addTraining.workoutKey = workoutKey;
                addTraining.totalTime = training.TotalTime;
                var dataAsString = JsonConvert.SerializeObject(new List<DTO.ImportTraining.Training> { addTraining });
                var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
                var stringresult = this.Client.PostAsync(GetUri("workouts/header"), content).Result.Content.ReadAsStringAsync().Result;
                //var o=JObject.Parse(stringresult);
                result = workoutKey;

            }
            else
            {
                addTraining.duration = training.TotalTime;
                var dataAsString = JsonConvert.SerializeObject(addTraining);
                var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
                var stringresult = this.Client.PostAsync(GetUri("workout"), content).Result.Content.ReadAsStringAsync().Result;
                var o = JObject.Parse(stringresult);
                var jobject = JsonConvert.DeserializeObject<ProductivityTools.SportsTracker.SDK.DTO.ImportGpx.Rootobject>(stringresult);
                result = jobject.payload.workoutKey;
                //result = o["metadata"]["ts"].ToString();
            }

            if (image != null)
            {
                var trainingId = result;

                foreach (var i in image)
                {
                    ImportFile(GetUri($"workouts/{trainingId}/image/web"), "image", i);
                }
            }
            return result;
        }

        public string ImportGpxFile(byte[] content)
        {
            var byteArray = new ByteArrayContent(content);
            //byteArray.Headers.ContentType = new MediaTypeHeaderValue("multipart/form-data");
            byteArray.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(byteArray, "file", "pawel.gpx");
            this.Client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            string resultAsString = this.Client.PostAsync(GetUri("workout/importGpx"), form).Result.Content.ReadAsStringAsync().Result;
            var jobject = JsonConvert.DeserializeObject<ProductivityTools.SportsTracker.SDK.DTO.ImportGpx.Rootobject>(resultAsString);
            return jobject.payload.workoutKey;
            this.Client.DeleteAsync(GetUri($"workouts/{jobject.payload.workoutKey}/delete"));
        }

        private string ImportFile(Uri url, string fileName, byte[] content)
        {
            var byteArray = new ByteArrayContent(content);
            byteArray.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            MultipartFormDataContent form = new MultipartFormDataContent();
            form.Add(byteArray, "file", fileName);
            this.Client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
            string resultAsString = this.Client.PostAsync(url, form).Result.Content.ReadAsStringAsync().Result;
            return resultAsString;
        }

        public void AddTraining(TrainingType trainingType, string description, int duration, DateTime startTime)
        {
            var newTraining = new ProductivityTools.SportsTracker.SDK.DTO.NewTraining.Rootobject();
            newTraining.activityId = (int)trainingType;
            newTraining.description = description;
            newTraining.duration = duration;
            newTraining.energy = 0;
            newTraining.sharingFlags = (int)SharingType.Public;
            newTraining.timeZoneOffset = 0;
            newTraining.totalDistance = 0;
            newTraining.startTime = ConvertToUnixTimestamp(startTime) * 1000;
            var dataAsString = JsonConvert.SerializeObject(newTraining);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");

            var result = this.Client.PostAsync(GetUri("workout").ToString(), content).Result.Content.ReadAsStringAsync().Result;
        }

        public void DeleteTraining(string workoutKey)
        {
            this.Client.DeleteAsync(GetUri($"workouts/{workoutKey}/delete"));
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        //public void ImportGpxFile(string path)
        //{
        //   var x= UploadFile(GetUri("workout/importGpx").ToString(), path).Result;
        //}

        //public async Task<string> UploadFile(string actionUrl, string filePath)
        //{

        //    FileStream fileStream = File.OpenRead(filePath);
        //    var streamContent = new StreamContent(fileStream);
        //    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
        //    streamContent.Headers.ContentDisposition.Name = "\"file\"";
        //    streamContent.Headers.ContentDisposition.FileName = "\"" + Path.GetFileName(filePath) + "\"";
        //    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
        //    string boundary = "----WebKitFormBoundaryAFqTXNjdlhl0zwKi";
        //    var content = new MultipartFormDataContent(boundary);
        //    content.Headers.Remove("Content-Type");
        //    content.Headers.TryAddWithoutValidation("Content-Type", "multipart/form-data; boundary=" + boundary);
        //    content.Add(streamContent);
        //    HttpResponseMessage response = null;
        //    try
        //    {
        //        this.Client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        //        //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        //        //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        //        //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));

        //        response = await this.Client.PostAsync(actionUrl, content);
        //    }
        //    catch (WebException ex)
        //    {
        //        // handle web exception
        //        return null;
        //    }
        //    catch (TaskCanceledException ex)
        //    {

        //    }

        //    try
        //    {
        //        response.EnsureSuccessStatusCode();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    };

        //    string res = await response.Content.ReadAsStringAsync();
        //    return await Task.Run(() => res);
        //}
    }
}
