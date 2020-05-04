using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using sm_coding_challenge.Models;
using sm_coding_challenge.Services.DataProvider;

namespace sm_coding_challenge.Controllers
{
    public class HomeController : Controller
    {

        private IDataProvider _dataProvider;
        public HomeController(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Player(string id)
        {
            try
            {
                return Json(await _dataProvider.GetPlayerById(id));
        }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Check your network connection: {httpRequestException.Message}");
    }
}

        [HttpGet]
        public async Task<IActionResult> Players(string ids, string id)
        {
            //Introduced Try Catch exception handling to gracefully not fail but gently warn user about unavailability of endpoint
            try
            {
                string x;
            if (string.IsNullOrEmpty(ids))
            {
                x = id;
            }
            else
            {
                x = ids;
            }
            var idList = x.Split(',');
            var returnList = new List<PlayerModel>();
            foreach (var q in idList.Distinct())
            {
                returnList.Add(await _dataProvider.GetPlayerById(q));
            }
            return Json(returnList);
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Check your network connection: {httpRequestException.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Players1000()
        {
            try
            {
                return Json(await _dataProvider.Get1000Players());
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Check your network connection: {httpRequestException.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> LatestPlayers(string ids)
        {
            try
            {
                return Json(await _dataProvider.GetLatestPlayers(ids));
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Check your network connection: {httpRequestException.Message}");
            }
        } 

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
