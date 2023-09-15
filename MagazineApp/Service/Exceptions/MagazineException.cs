using System;

namespace MagazineApp.Service.Exceptions;
public class MagazineException : Exception
{
    public int Code { get; set; }
    public MagazineException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
