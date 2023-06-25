using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using Telerik.Web.Captcha;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc.Models;
using System.Speech.Synthesis;
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Text;
using SRVTextToImage;
using Captcha.Models;

namespace Captcha.Controllers
{
    [Route("[controller]")]
    public class CaptchaController : Controller
    {
        // GET: Captcha
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FeedbackForm(FeedbackModel model, string captchaText)
        {
            var captchaTextSession = this.Session["CaptchaImageText"].ToString();

            if (captchaTextSession.Equals(captchaText))
            {
                ViewBag.Message = "Captcha Validation Success!";
            }
            else
            {
                ViewBag.Message = "Captcha Validation Failed!";
            }

            return View("../Home/Index");
        }

        // This action for get Captcha Image
        [HttpGet]
        // This is for output cache false
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")] 
        public FileResult GetCaptchaImage()
        {
            CaptchaRandomImage CI = new CaptchaRandomImage();

            // here 5 means I want to get 5 char long captcha
            //CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75);
            // Or We can use another one for get custom color Captcha Image
            var captchaText = CI.GetRandomString(5);

            this.Session["CaptchaImageText"] = captchaText.ToLower();

            CI.GenerateImage(this.Session["CaptchaImageText"].ToString(), 300, 75, Color.DarkGray, Color.White);

            MemoryStream stream = new MemoryStream();
            CI.Image.Save(stream, ImageFormat.Png);
            stream.Seek(0, SeekOrigin.Begin);

            return new FileStreamResult(stream, "image/png");
        }

        [HttpGet]
        public async Task<ActionResult> GetCaptchaAudio()
        {
            Task.Run(() => ReadCaptcha(captchaText));

            return null;
        }

        private void ReadCaptcha(string captchaText)
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer
            {
                Rate = -7
            };

            // Configure the audio output.
            synth.SetOutputToDefaultAudioDevice();

            // Speak a string.
            synth.SpeakAsync(captchaText);
        }
    }
}