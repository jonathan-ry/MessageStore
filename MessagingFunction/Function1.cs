using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using MessagingFunction.Models;

namespace MessagingFunction
{
    public static class Function1
    {
        [FunctionName("ErrorMessagingFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            var isCritical = false;

            log.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<ErrorResponse>(requestBody);

            if(data != null) 
            {
                log.LogInformation(requestBody);

                //Check the status code to determine the email recipient of the notification
                var emails = EmailAssigner(data.HttpStatusCode);

                //Message Analysis
                if (data.HttpStatusCode >= 500 && 
                    (data.Message.Contains("system failure") || data.Message.Contains("server error"))) isCritical = true;

                ErrorAnalysisResponse response = new ErrorAnalysisResponse()
                {
                    ErrorResponse = data,
                    Emails = emails,
                    IsCritical = isCritical
                };

                string responseJson = JsonConvert.SerializeObject(response);

                log.LogInformation(responseJson);

                return new OkObjectResult(responseJson);
            }

            return new BadRequestResult();
        }

        private static List<string> EmailAssigner(int code)
        {
            var emails = new List<string>();
            switch (code)
            {
                case 400:
                    emails.Add("dev@accentureph.com");
                    emails.Add("support@accentureph.com");
                    break;

                case 401:
                    emails.Add("security@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                case 403:
                    emails.Add("security@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                case 404:
                    emails.Add("dev@accentureph.com");
                    emails.Add("support@accentureph.com");
                    break;
                case 500:
                    emails.Add("devops@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                case 502:
                    emails.Add("devops@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                case 503:
                    emails.Add("devops@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                case 504:
                    emails.Add("devops@accentureph.com");
                    emails.Add("dev@accentureph.com");
                    break;
                default: 
                    break;
            }

            return emails;
        }
    }
}
