using System;
using System.ComponentModel.DataAnnotations;
using BoardGames.Validators;

namespace BoardGames.Models {
    public class Game
    {
        public int ID { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Genre { get; set; }

        [Display(Name = "Min Gamers")]
        [HowManyGamersOwnValid]
        [Range(1, 25)]
        public int MinGamers { get; set; }

        [Display(Name = "Max Gamers")]
        [HowManyGamersOwnValid]
        [Range(1, 25)]
        public int MaxGamers { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Release Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [NewInStockPriceOwnValid]
        [Range(1, 100)]
        public int Price { get; set; }

        [Required]
        [Display(Name = "Publisher")]
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}
