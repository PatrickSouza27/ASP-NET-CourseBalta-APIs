using System.ComponentModel.DataAnnotations;

namespace CRUD_APIs_SqlSeverMigrations.ViewModels
{
    public class CreateEeditCategoryView
    {
        [Required(ErrorMessage = "O nome é Obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Esse Texto deve ter entre 3 e 40 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O Slug é Obrigatório")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "Esse Texto deve ter entre 3 e 40 caracteres")]
        public string Slug { get; set; }
    }
}
