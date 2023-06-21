using System;
using System.Speech.Synthesis;
using System.Threading.Tasks;
using System.Web;

namespace Captcha.HttpHandler
{
    public class CaptchaAudioHandler : IHttpHandler
    {
        /// <summary>
        /// You will need to configure this handler in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpHandler Members

        public bool IsReusable
        {
            // Return false in case your Managed Handler cannot be reused for another request.
            // Usually this would be false in case you have some state information preserved per request.
            get { return true; }
        }

        public void ProcessRequest(HttpContext context)
        {
            var captchaText = "";
            if (context.Session["CaptchaImageText"] != null)
            {
                captchaText = context.Session["CaptchaImageText"].ToString();
                Task.Run(() => GetAudio()).GetAwaiter();
            }
            context.ApplicationInstance.CompleteRequest();
        }

        private async Task GetAudio()
        {
            // Initialize a new instance of the SpeechSynthesizer.
            SpeechSynthesizer synth = new SpeechSynthesizer
            {
                Rate = -7
            };
            // Configure the audio output.
            synth.SetOutputToDefaultAudioDevice();
            var captchaText = HttpContext.Current.Session["CaptchaImageText"].ToString();
            
            // Speak a string.
            synth.SpeakAsync(captchaText);
        }
        #endregion
    }
}
