using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ProiectLicenta.DTO
{
    public class EMailMessage
    {
        [Display(Name = "Adresa destinatar")]
        [Required]
        public string To { get; set; }

        [Display(Name = "Subiect")]
        public string Subject { get; set; }

        [Display(Name = "Mesajul Dvs")]
        public string MessageContent { get; set; }

        //public HttpPostedFileBase File { get; set; }
    }
}