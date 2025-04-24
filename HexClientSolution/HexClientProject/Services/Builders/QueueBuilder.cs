using System;
using System.Threading;
using HexClientProject.Interfaces;
using HexClientProject.Services.Api;
using Newtonsoft.Json;

namespace HexClientProject.Services.Builders
{
    public class QueueBuilder : IQueueService
    {
        public bool IsReadyCheckStatusReady()
        {
            string response = QueueApi.GetReadyCheckStatus().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            if (jsonObject == null)
            {
                throw new Exception("Is ready check status ready: Json error");
            }

            return jsonObject.state == "EveryoneReady" || jsonObject.state == "InProgress";
        }

        public bool IsMatchFound()
        {
            string response = QueueApi.GetQueueState().Result;
            dynamic jsonObject = JsonConvert.DeserializeObject<dynamic>(response) ?? throw new InvalidOperationException();

            if (jsonObject == null)
            {
                throw new Exception("Get queue state: Json error");
            }

            return jsonObject.searchState == "Found";
        }
    }
}
