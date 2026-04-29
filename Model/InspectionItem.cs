namespace fleetops_backend.Models
{
    public class InspectionItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsRequired { get; set; }

        public ICollection<InspectionResult> Result { get; set; } = new List<InspectionResult>();
    }
}