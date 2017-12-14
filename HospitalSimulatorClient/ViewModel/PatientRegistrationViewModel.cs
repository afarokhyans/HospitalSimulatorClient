using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HospitalSimulatorClient.Model;
using HospitalSimulatorClient.Properties;
using Newtonsoft.Json;

namespace HospitalSimulatorClient.ViewModel
{
    public class PatientRegistrationViewModel : ViewModelBase
    {

        public PatientRegistrationViewModel()
        {
            ConditionType = new[] { "Flu", "Cancer" };
            Topology = new[] { "", "Head&Neck", "Breast"};
            RegisterPatient = new RelayCommand(ExecutePatientRegistration);
            TopologyEnabled = false;
        }

        public static RelayCommand RegisterPatient { get; set; }
        public string[] Topology { get; set; }

        public string[] ConditionType { get; set; }

        private bool _topologyEnabled;
        public bool TopologyEnabled
        {
            get => _topologyEnabled;
            set => Set(ref _topologyEnabled, value);
        }

        private string _patientName;
        public string PatientName
        {
            get => _patientName;
            set => Set(ref _patientName, value);
        }

        private string _selectedCondition;
        public string SelectedCondition
        {
            get => _selectedCondition;
            set
            {
                TopologyEnabled = !value.Equals("Flu");
                SelectedTopology = value.Equals("Flu") ? Topology[0] : Topology[1];
                Set(ref _selectedCondition, value);
            }
        }

        private string _selectedTopology;
        public string SelectedTopology
        {
            get => _selectedTopology;
            set => Set(ref _selectedTopology, value);
        }

        private string _resultMessage;
        public string ResultMessage
        {
            get => _resultMessage;
            set => Set(ref _resultMessage, value);
        }

        private async void ExecutePatientRegistration()
        {
            // Create a Patient object
            var patient = new Patient
            {
                Name = PatientName,
                Condition = new Condition
                {
                    Type = SelectedCondition,
                    Topology = SelectedTopology
                }
            };

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Resources.BaseServerUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent(JsonConvert.SerializeObject(patient), Encoding.UTF8, "application/json");
                    var response = await client.PostAsync("api/patients", httpContent);

                    ResultMessage = response.StatusCode.Equals(HttpStatusCode.Created) ? $"Patient {PatientName} Created." : $"Failed to register Patient {PatientName}";
                }
            }
            catch (HttpRequestException ex)
            {
                ResultMessage = ex.Message;
            }
        }

    }
}
