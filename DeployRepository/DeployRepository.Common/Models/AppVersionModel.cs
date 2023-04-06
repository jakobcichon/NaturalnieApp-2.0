namespace DeployRepository.Common.Models
{
    public record AppVersionModel
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Build { get; set; }

        public override string ToString()
        {
            return $"{Major}.{Minor}.{Build}";
        }
    }
}
