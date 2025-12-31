using eShopLite.StoreFx.Services;
using Microsoft.AspNetCore.Mvc;

namespace eShopLite.StoreFx.Controllers
{
    /// <summary>
    /// Handles requests for the home page and main views.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IStoreService _storeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="HomeController"/> class.
        /// </summary>
        /// <param name="storeService">The store service for business operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when storeService is null.</exception>
        public HomeController(IStoreService storeService)
        {
            _storeService = storeService ?? throw new ArgumentNullException(nameof(storeService));
        }

        /// <summary>
        /// Displays the home page.
        /// </summary>
        /// <returns>The home view.</returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Displays the products page with all available products.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the products view.</returns>
        public async Task<IActionResult> Products()
        {
            ViewBag.Message = "Browse our outdoor product catalog";

            var products = await _storeService.GetProductsAsync();

            return View(products);
        }

        /// <summary>
        /// Displays the stores page with all store locations.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation, containing the stores view.</returns>
        public async Task<IActionResult> Stores()
        {
            ViewBag.Message = "Find a store near you";

            var stores = await _storeService.GetStoresAsync();

            return View(stores);
        }

        /// <summary>
        /// Displays the error page.
        /// </summary>
        /// <returns>The error view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }

        /// <summary>
        /// Displays the status error code page.
        /// </summary>
        /// <param name="code">The HTTP status code.</param>
        /// <returns>The status error code view.</returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult StatusErrorCode(int code)
        {
            return View("StatusErrorCode", code);
        }
    }
}