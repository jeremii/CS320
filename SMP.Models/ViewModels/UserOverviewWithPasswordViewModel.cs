using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SMP.Models.ViewModels
{
    public class UserOverviewWithPasswordViewModel : UserOverviewViewModel
    {
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
