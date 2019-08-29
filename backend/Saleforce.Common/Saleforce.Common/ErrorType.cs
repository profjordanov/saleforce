namespace Saleforce.Common
{
    public enum ErrorType
    {
        Validation = 0,
        NotFound = 1,
        Unauthorized = 2,
        Conflict = 4,
        Critical = 8
    }
}