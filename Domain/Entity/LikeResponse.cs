using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    public class LikeResponse
    {

            public bool Success { get; set; }
            public string Message { get; set; }
            public Like Like { get; set; }

            public LikeResponse(bool success, string message, Like like = null)
            {
                Success = success;
                Message = message;
                Like = like;
            }
     
    }
}
