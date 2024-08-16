using System.ComponentModel.DataAnnotations;

public class GenreDTO
{
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Enter the {0}")]
    [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
    public string Title { get; set; }
}

public class CreateGenreDTO
{
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Enter the {0}")]
    [MaxLength(50,ErrorMessage = "{0} can not be more than {1} characters")]
    public string Title { get; set; }
}

public class UpdateGenreDTO
{
    public long Id { get; set; }
    [Display(Name = "Title")]
    [Required(ErrorMessage = "Enter the {0}")]
    [MaxLength(50, ErrorMessage = "{0} can not be more than {1} characters")]
    public string Title { get; set; }
}