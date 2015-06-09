using System.Web.Mvc;
using SportsStore.Domain.Abstract;
using System.Linq;
using SportsStore.Domain.Entities;
using System.Web;

namespace SportsStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;
        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int productId)
        {
            Product product = repository.Products
            .FirstOrDefault(p => p.ProductID == productId);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    product.ImageMimeType = image.ContentType;
                    product.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(product.ImageData, 0, image.ContentLength);
                }
                repository.SaveProduct(product);
                //I cannot use ViewBag in this situation because the user is being redirected. ViewBag passes data between the
                //controller and view, and it cannot hold data for longer than the current HTTP request. I could have used the session
                //data feature, but then the message would be persistent until I explicitly removed it, which I would rather not have to
                //do. So, the TempData feature is the perfect fit. The data is restricted to a single user’s session (so that users do not see
                //each other’s TempData) and will persist long enough for me to read it. I will read the data in the view rendered by the
                //action method to which I have redirected the user, which I define in the next section.
                //temp data is deleted at the end of the HTTP request.
                TempData["message"] = string.Format("{0} has been saved", product.Name);

                return RedirectToAction("Index");
            }
            else
            {
                // there is something wrong with the data values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View("Edit", new Product());
        }


        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deletedProduct = repository.DeleteProduct(productId);
            if (deletedProduct != null)
            {
                TempData["message"] = string.Format("{0} was deleted",
                deletedProduct.Name);
            }

            return RedirectToAction("Index");
        }
    }
}