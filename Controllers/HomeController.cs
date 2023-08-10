using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkiaSharp;
using Task_2.Models;

namespace Task_2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
     private readonly List<ImageInfo> imageList = new List<ImageInfo>
        {
            new ImageInfo { id = 1,Ans = "B", Link = "https://i.pinimg.com/originals/95/11/e5/9511e5f3f36381829c3cc72c6347785d.jpg" },
            new ImageInfo { id = 2,Ans = "A", Link = "https://res.cloudinary.com/picked/image/upload/v1597396477/cms/how-to-ace-spatial-reasoning-tests-1597396476.jpg" },
            new ImageInfo { id = 3,Ans = "C", Link = "/https://res.cloudinary.com/picked/image/upload/q_60,h_600,f_auto/v1645098985/cms/spatial-reasoning-tests-1645098985" },
            new ImageInfo { id = 4,Ans = "A", Link = "/https://res.cloudinary.com/picked/image/upload/q_60,h_600,f_auto/v1645098985/cms/spatial-reasoning-tests-1645098985" },
            new ImageInfo { id = 5,Ans = "B", Link = "/https://res.cloudinary.com/picked/image/upload/q_60,h_600,f_auto/v1645098985/cms/spatial-reasoning-tests-1645098985" }
        };
         private int GetCurrentImageIndex()
        {
            int index = HttpContext.Session.GetInt32("ImageIndex") ?? 0;
            return index;
        }

        private void SetCurrentImageIndex(int index)
        {
            HttpContext.Session.SetInt32("ImageIndex", index);
        }

      public IActionResult Index()
        { int currentIndex = GetCurrentImageIndex();
            ImageInfo currentImage = imageList[currentIndex];
            SetCurrentImageIndex(currentIndex + 1);
                return View("Index", currentImage);
    }

        public IActionResult RandomImage()
        {
            SKBitmap randomImage = GenerateRandomImage();
            using (SKImage img = SKImage.FromBitmap(randomImage))
            {
                using (var ms = new MemoryStream())
                {
                    img.Encode(SKEncodedImageFormat.Png, 100).SaveTo(ms);
                    return File(ms.ToArray(), "image/png");
                }
            }
        }

        private SKBitmap GenerateRandomImage()
        {
            int width = 100;
            int height = 50;

            SKBitmap bitmap = new SKBitmap(width, height);

            Random random = new Random();

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    SKColor pixelColor = new SKColor(
                        (byte)random.Next(256),   // Red
                        (byte)random.Next(256),   // Green
                        (byte)random.Next(256),   // Blue
                        255                       // Alpha (fully opaque)
                    );

                    bitmap.SetPixel(x, y, pixelColor);
                }
            }

            return bitmap;
        }
        public IActionResult LoadNextImage(int step)
{
    int currentIndex = GetCurrentImageIndex();
    int newIndex = (currentIndex + step + imageList.Count) % imageList.Count;
    ImageInfo nextImage = imageList[newIndex];
    SetCurrentImageIndex(newIndex);
    return Json(new { link = nextImage.Link });
}
public IActionResult NextImage()
        {
            int currentIndex = GetCurrentImageIndex();
            currentIndex = (currentIndex + 1) % imageList.Count;
            SetCurrentImageIndex(currentIndex);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult PreviousImage()
        {
            int currentIndex = GetCurrentImageIndex();
            currentIndex = (currentIndex - 1 + imageList.Count) % imageList.Count;
            SetCurrentImageIndex(currentIndex);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
