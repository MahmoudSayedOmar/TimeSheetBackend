using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using Serilog;
using EmployeeTimeOff.ErrorHandlers;
using Newtonsoft.Json.Linq;
using EmployeeTimeOff.Data;
using Newtonsoft.Json;

namespace EmployeeTimeOff.Controllers
{
    [Route("api/[controller]")]
    public class TimeSheetController : Controller
    {

        public TimeSheetController()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\myapp.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        [HttpGet]
        public async Task<object> GetAsync(string userId)
        {
            try {
                using (var client = new HttpClient())
                {
                    if(userId is null)
                    {
                        throw new NullIdCustomException("unable to request null user id");
                    }
                    client.BaseAddress = new Uri("https://sandbox.api.sap.com/");
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("apikey", "OmY1P6JWwGe8AGcijSMCB0KPPCRgrxYk");
                    // HTTP GET 100096
                    
                    var response = client.GetAsync($"successfactors/odata/v2/EmployeeTime?%24filter=userId%20eq%20{userId}&%24orderby=approvalStatus&%24select=approvalStatus,endDate,startDate,timeType");
                    Log.Information("Request Sent");
                    string Result = await response.Result.Content.ReadAsStringAsync();
                    Test TimeSheets= JsonConvert.DeserializeObject<Test>(Result);
                    if (TimeSheets.d.results.Count != 0)
                    {
                        Log.Information($"Request Completed for user : {userId}");
                        return Ok(Result);
                    }
                }
                throw new NotFoundCustomException("No data found", new Exception($"There is no time sheet for a user with ID : {userId}"));

            }
            catch(NotFoundCustomException e)
            {
                Log.Information($"Please check your parameters userId: {userId}");
                return e.InnerException.Message;

            }
            catch(NullIdCustomException e)
            {
                Log.Information("User Id was sent by null");
                return BadRequest(e.Message);
            }
        }
    }
}