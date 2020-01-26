using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using SemManagement.UWP.Model;
using SemManagement.UWP.Model.Api;

namespace SemManagement.UWP.Configurations
{
    public abstract class WebApiProvider
    {
        private readonly IRestClient _restClient;
        protected readonly IRestEndpoints RestEndpoint;

        protected WebApiProvider(IRestEndpoints restEndpoints, PublicApiConfiguration publicApiConfig)
        {
            RestEndpoint = restEndpoints;

            _restClient = new RestClient(publicApiConfig.BaseUrl);
        }

        protected Task<List<T>> TakeAsync<T>(string endpoint, int take, int skip)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            if (skip > 0)
                request.AddParameter("skip", skip);

            request.AddParameter("take", take);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> GetPlaylistsByStationAsync<T>(string endpoint, int stationId)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<Count> CountAsync<T>(string endpoint)
        {
            TaskCompletionSource<Count> tcs = new TaskCompletionSource<Count>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            _restClient.ExecuteAsync(request, (IRestResponse<Count> response) => ResponseHandler<Count>(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> GetDeletedSongsAsync<T>(string endpoint, int stationId)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> GetSongsByPlaylistAsync<T>(string endpoint, int playlistId)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("playlistId", playlistId);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> GetStationSongsAsync<T>(string endpoint, int stationId)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> GetStationQueueAsync<T>(string endpoint, int stationId)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> MostPopularSongs<T>(string endpoint, int stationId, int top)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            request.AddParameter("top", top);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task<User> GetStationUserAsync(string endpoint, int stationId) 
        {
            TaskCompletionSource<User> tcs = new TaskCompletionSource<User>();

            IRestRequest request = new RestRequest(endpoint, Method.GET, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddParameter("stationId", stationId);

            _restClient.ExecuteAsync(request, (IRestResponse<User> response) => ResponseHandler<User>(response, tcs));

            return tcs.Task;
        }

        protected Task<List<T>> AddAsync<T>(string endpoint, List<T> entities, DateTime lastSyncDate)
        {
            TaskCompletionSource<List<T>> tcs = new TaskCompletionSource<List<T>>();

            IRestRequest request = new RestRequest(endpoint, Method.POST, DataFormat.Json)
            {
                Timeout = 30 * 60 * 1000,
                ReadWriteTimeout = 30 * 60 * 1000
            };

            request.AddJsonBody(entities);

            _restClient.ExecuteAsync(request, (IRestResponse<List<T>> response) => ResponseHandler(response, tcs));

            return tcs.Task;
        }

        protected Task AddAsync<T>(string endpoint, T entity)
        {
            return Task.Run(() =>
            {
                IRestRequest request = new RestRequest(endpoint, Method.POST, DataFormat.Json)
                {
                    Timeout = 30 * 60 * 1000,
                    ReadWriteTimeout = 30 * 60 * 1000
                };

                request.AddJsonBody(entity);

                _restClient.ExecuteAsync(request, (IRestResponse response) => { });
            });
        }

        protected Task RemovePlaylistFromStationAsync(string endpoint, int playlistId, int stationId)
        {
            return Task.Run(() =>
            {
                IRestRequest request = new RestRequest(endpoint, Method.PUT, DataFormat.Json)
                {
                    Timeout = 30 * 60 * 1000,
                    ReadWriteTimeout = 30 * 60 * 1000
                };

                request.AddParameter("playlistId", playlistId, ParameterType.QueryString);

                request.AddParameter("stationId", stationId, ParameterType.QueryString);

                _restClient.ExecuteAsync(request, (IRestResponse response) => 
                {
                    // handle response
                });
            });
        }

        protected Task AddPlaylistToStationAsync(string endpoint, int playlistId, int stationId)
        {
            return Task.Run(() =>
            {
                IRestRequest request = new RestRequest(endpoint, Method.PUT, DataFormat.Json)
                {
                    Timeout = 30 * 60 * 1000,
                    ReadWriteTimeout = 30 * 60 * 1000
                };

                request.AddParameter("playlistId", playlistId, ParameterType.QueryString);

                request.AddParameter("stationId", stationId, ParameterType.QueryString);

                _restClient.ExecuteAsync(request, (IRestResponse response) =>
                {
                    // handle response
                });
            });
        }

        private void ResponseHandler<T>(IRestResponse<T> response, TaskCompletionSource<T> tcs) where T : class
        {
            Console.WriteLine(response.StatusCode);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    tcs.SetResult(response.Data);
                    break;
                default:
                    tcs.SetException(new InvalidOperationException($"{nameof(T)}Provider. Response failed. Response info: {response.StatusCode}, {response.ErrorException} "));
                    break;
            }
        }
    }
}
