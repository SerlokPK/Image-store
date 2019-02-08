using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.ViewModel
{
    public class UserViewModel
    {
        public User User { get; set; }
        public UserViewModel()
        {
            using (var context = new ImageStoreEntities()) {
                //context.Images;
            }
        }
    }
}
