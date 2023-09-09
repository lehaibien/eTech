using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTech.Entities
{
  public class UserImage : FileUpload
  {
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
  }
}