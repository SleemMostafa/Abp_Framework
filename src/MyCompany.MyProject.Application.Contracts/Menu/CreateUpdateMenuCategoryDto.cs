using System.ComponentModel.DataAnnotations;

namespace MyCompany.MyProject.Menu;

public sealed class CreateUpdateMenuCategoryDto
{
    [Required]
    [MaxLength(MenuConsts.MaxCategoryNameLength)]
    public string Name { get; set; } = string.Empty;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; } = true;
}
