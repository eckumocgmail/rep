using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Google_LoginApplication.Areas.Identity.Modules.ReCaptcha
{

 

    /// </summary>
    public class ReCaptchaOptions
    {

        public bool IsActive { get; set; } = false;

        /// <summary>
        /// Закрытый ключ, полученный в результате регистрации 
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// Закрытый ключ, полученный в результате регистрации 
        /// </summary>
        public string SiteKey { get; set; }

        public ReCaptchaOptions()
        {
            SiteKey = "6Lc-MeMZAAAAAFy2KYk4UH-ZX1TJdsu6wvVLAyfY";
            PrivateKey = "6Lc-MeMZAAAAAPzBa9vDoBgUxlw0ZBCC_loabK52";
        }
    }

}
