namespace Auth.Domain
{
    public class MachineIdentity : Identity
    {
        public string HardwareId { get; set; }
        public string LicenseKey { get; set; }
    }


}