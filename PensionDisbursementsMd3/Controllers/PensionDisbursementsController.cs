using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PensionDisbursementsMd3.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PensionDisbursementsMd3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PensionDisbursementsController : ControllerBase
    {
        

        //[HttpGet]
        [HttpGet("{id}/{salaryCalculated}")]
        public ActionResult Get([FromRoute]long id,[FromRoute]double salaryCalculated)
        {
            PensionerDetails pd = new PensionerDetails();
            using (var client = new HttpClient())
            {
                //withoutOcelot
               // client.BaseAddress = new Uri("http://localhost:20268");
                client.BaseAddress = new Uri("https://pensionerdetailsdep1.azurewebsites.net");
                var responseTask = client.GetAsync(string.Format("/api/pensionerdetails/{0}", id));
                //string url = string.Format("https://localhost:44380/pdetails/{0}", id);
                //client.BaseAddress = new Uri(url);
                //var responseTask = client.GetAsync("");

                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var result1 = result.Content.ReadAsStringAsync().Result;
                    pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
                }
                if(pd.AadharNumber==0)
                {
                    return BadRequest("Check Aadhar Number");
                }

            }
            double actualPension;
            if (pd.pensionType.Equals("self"))
               actualPension = pd.salaryEarned * 0.8 + pd.allowances;
            else
                actualPension = pd.salaryEarned * 0.5 + pd.allowances;
            if (pd.bankType.Equals("publicbank"))
                actualPension += 500;
            else
                actualPension += 550;
           
            if (salaryCalculated==actualPension)
                return Ok("yay");
            else
                return BadRequest(20);
        }

        
    }
}
// GET: api/<PensionDisbursementsController>
//[HttpGet]
//public ActionResult Get()
//{
//    PensionerDetails pd = new PensionerDetails();
//    using (var client = new HttpClient())
//    {
//        client.BaseAddress = new Uri("http://localhost:20268/api/pensionerdetails/");
//        var responseTask = client.GetAsync($"{123456789}"/*{receivedDetails.aadhaar}*/);
//        responseTask.Wait();
//        var result = responseTask.Result;
//        if (result.IsSuccessStatusCode)
//        {
//            var result1 = result.Content.ReadAsStringAsync().Result;
//           pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
//        }

//    }
//    return Ok(pd);
//}

// GET api/<PensionDisbursementsController>/5
//[HttpGet("{id}")]
//public ActionResult Get(int id)
//{
//    PensionerDetails pd = new PensionerDetails();
//    using (var client = new HttpClient())
//    {
//        client.BaseAddress = new Uri("http://localhost:20268/api/pensionerdetails/");
//        var responseTask = client.GetAsync($"{123456789}"/*{receivedDetails.aadhaar}*/);
//        responseTask.Wait();
//        var result = responseTask.Result;
//        if (result.IsSuccessStatusCode)
//        {
//            var result1 = result.Content.ReadAsStringAsync().Result;
//            pd = JsonConvert.DeserializeObject<PensionerDetails>(result1);
//        }

//    }
//    if (pd.AadharNumber == id)
//        return Ok(10);
//    else
//        return BadRequest(20);
//
//}
//[TestFixture]
//public class Tests
//{
//    [SetUp]
//    public void Setup()
//    {

//    }

//    [TestCase(123456789, 23500)]
//    [TestCase(178945623, 61000)]
//    public void TestPensionCalculatedCorrect(int id, double pensionCalculated)
//    {
//        PensionDisbursementsController rd = new PensionDisbursementsController();
//        ActionResult ac = rd.Get(id, pensionCalculated);
//        Assert.IsInstanceOf<OkObjectResult>(ac);
//    }
//    [TestCase(123456789, 69590)]
//    [TestCase(178945623, 99090)]
//    public void TestPensionCalculatedWrong(int id, double pensionCalculated)
//    {
//        PensionDisbursementsController rd = new PensionDisbursementsController();
//        ActionResult ac = rd.Get(id, pensionCalculated);
//        Assert.IsInstanceOf<BadRequestObjectResult>(ac);
//    }
//}
