using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BoardGames.Validators;

namespace BoardGames.Models {
    public class Publisher
    {
        public int ID { get; set; }

        [Display(Name = "Company Name")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CompanyName { get; set; }

        [DataType(DataType.Date)]
        [DateAccuracyOwnValid]
        [Display(Name = "Founding Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime FoundingDate { get; set; }

        [Display(Name = "Country Of Origin")]
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string CountryOfOrigin { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Telephone { get; set; }

        public List<Game> Games { get; set; }
    }
}
