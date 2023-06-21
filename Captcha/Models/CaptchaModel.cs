using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Captcha.Models
{
    public class CaptchaModel
    {
        private string _captchaValue;
        public string Captcha {
            get
            {
                return string.IsNullOrEmpty(_captchaValue) ? "" : _captchaValue;
            }
            set
            {
                _captchaValue = value;
            }
        }
        public string CaptchaID { get; set; }
    }
}