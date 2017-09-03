using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace curmudgeon.ViewModels
{
    public class TopNavBarViewModel
    {
        public string DisplayName { get; set; }
        public string UserColors { get; set; }

        public TopNavBarViewModel(string displayName, string userColors)
        {
            DisplayName = displayName;
            UserColors = userColors;
        }
    }
}
