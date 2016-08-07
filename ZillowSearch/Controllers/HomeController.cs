using System;
using System.Linq;
using System.Web.Mvc;
using ZillowSearch.Models;
using ZillowSearch.ZillowPropertySerach;

namespace ZillowSearch.Controllers
{
    public class HomeController : Controller
    {
        private readonly IZillowPropertySearchGateway _zillowPropertySerachGateway;

        public HomeController(IZillowPropertySearchGateway zillowPropertySearchGateway )
        {
            _zillowPropertySerachGateway = zillowPropertySearchGateway;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public JsonResult Search(HomeViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ZillowPropertySearchResponse serviceResponse = _zillowPropertySerachGateway.GetPropertyDetails(
                        model.Address, model.CityAndStateOrZipCode, model.IncludeRentEstimate);

                    if (serviceResponse.IsSuccessful)
                    {
                        return Json(new
                        {
                            Success = true,
                            Data = serviceResponse.Data
                        }, JsonRequestBehavior.AllowGet);
                    }
                    return Json(new
                    {
                        Success = false,
                        Data = new[] { serviceResponse.Message }
                    }, JsonRequestBehavior.AllowGet);
                }

                // If we got this far, something failed, send back model errors
                return Json(new
                {
                    Success = false,
                    Data = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()
                }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(new
                {
                    Success = false,
                    Data = new [] { "An unknown error has occurred, please try again later." }
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}