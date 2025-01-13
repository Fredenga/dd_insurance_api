namespace InsuranceAPI.DTOs
{
    public record InsuranceProductDTO
    (
        
        string? ProductName,
        string? Category,
        string? Description,
        decimal BasePremium
    );
}
