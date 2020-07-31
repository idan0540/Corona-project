using System.Linq;
using System.Web.Mvc;
using System;
using CoronaStore.Models;
using CoronaStore.Services;

namespace CoronaStore.Controllers
{
    public class HomeController : Controller
    {
        private CoronaPageContext ct = new CoronaPageContext();
        ShopService shopService = new ShopService();
        ProductsPageModel model = new ProductsPageModel();
        LocationsService _locationService = new LocationsService();

        public ActionResult Index()
        {
            return View();
        }
       
        public ActionResult Locations(string searchTermName, int? searchTermPopulation)
        {
            if (searchTermName == null && searchTermPopulation == null)
            {
                return View(ct.Locations.AsEnumerable());
            }
            else
            {
                 var model = _locationService.SearchLocations(searchTermName ?? string.Empty, searchTermPopulation);
                 return View(model);
            }

        }

        public ActionResult Manager()
        {
            return View();
        }

        public ActionResult Shop()
        {
            return View();
        }

        public ActionResult UserProducts()
        {
            return View();
        }

        public ActionResult Products(string searchTermName,  int? searchTermPrice,  int categoryId = -1)
        {
            if (Session["UserID"] != null)
            {
                int userId = (int)Session["UserID"];

                model.Recommended = shopService.RecommendProducts(userId);
            }

            model.Products = shopService.SearchProducts(searchTermName ?? string.Empty, searchTermPrice, categoryId);

            if (model.Categories == null)
            {
                model.Categories = shopService.GetAllCategories();
            }

            model.TopSale = shopService.getTopSaleProduct();

            return View(model);
        }


        public ActionResult SearchProducts(string termName, int price)
        {
            return View(shopService.SearchProducts(termName, price));
        }

        public ActionResult Error()
        {
           return null;
        }

        [HttpGet]
        public ActionResult GetProduct(int id)
        {
            var product = shopService.GetProduct(id);

            if (product == null)
            {
                return HttpNotFound();
            }

            return Json(product, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult BuyProduct(int userId,int id)
        {
            return Json(shopService.BuyProduct(userId, id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product product)
        {
            return Json(shopService.UpdateProduct(product));
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            return Json(shopService.AddProduct(product));
        }
        [HttpPost]
        public ActionResult UpdateLocation(Location location)
        {
            return Json(shopService.UpdateLocation(location));
        }
        [HttpPost]
        public ActionResult UpdateInventory(Inventory inventory)
        {
            return Json(shopService.UpdateInventory(inventory));
        }
        [HttpDelete]
        public ActionResult DeleteLocation(int id)
        {
            if (shopService.DeleteLocation(id))
                return Json(id);
            else
                return Json(false);
        }
    }
}