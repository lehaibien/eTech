using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTech.Entities.Response
{
  public class ImageBlob
  {
    public byte[] Bytes { get; set; }
    public string ContentType { get; set; }
    public ImageBlob(byte[] bytes, string contentType)
    {
      Bytes = bytes;
      ContentType = contentType;
    }
  }
}