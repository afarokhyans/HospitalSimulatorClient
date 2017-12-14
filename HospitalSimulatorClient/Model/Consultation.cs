using System;

namespace HospitalSimulatorClient.Model
{
    public class Consultation
    {
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public TreatmentRoom TreatmentRoom { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime ConsultationDate { get; set; }
    }
}
