namespace Admin.Dtos
{
    public record TransactionStatus
    (
        bool IsSucceed,
        string? Message
    );
}
