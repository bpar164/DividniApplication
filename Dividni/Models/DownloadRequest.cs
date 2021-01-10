using System;
using System.ComponentModel.DataAnnotations;

namespace Dividni.Models
{
    public class DownloadRequest
    {
        public Guid Id { get; set; }
        public int Versions { get; set; }
        public string Type { get; set; }
    }
}