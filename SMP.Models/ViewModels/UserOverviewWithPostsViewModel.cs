using System;
using System.Collections.Generic;
using System.Text;

namespace SMP.Models.ViewModels
{
    public class UserOverviewWithPostsViewModel : UserOverviewViewModel
    {
        public IEnumerable<UserPostViewModel> Posts { get; set; }
    }
}
