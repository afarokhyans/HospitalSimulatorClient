using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HospitalSimulatorClient.Model;
using HospitalSimulatorClient.Properties;

namespace HospitalSimulatorClient.ViewModel
{
    public class ConsultationsDisplayViewModel : ViewModelBase
    {
        public ConsultationsDisplayViewModel()
        {
            GetConsultationList = new RelayCommand(ExecuteGetConsultationList);
        }

        public static RelayCommand GetConsultationList { get; set; }

        private List<Consultation> _consultationsList = new List<Consultation>();
        public List<Consultation> ConsultationsList
        {
            get => _consultationsList;
            set => Set(ref _consultationsList, value);
        }

        private string _resultMessage;
        public string ResultMessage
        {
            get => _resultMessage;
            set => Set(ref _resultMessage, value);
        }

        private async void ExecuteGetConsultationList()
        {
            ResultMessage = "";
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Resources.BaseServerUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("api/consultations");
                    if (response.IsSuccessStatusCode)
                    {
                        ConsultationsList = await response.Content.ReadAsAsync<List<Consultation>>();
                    }
                    
                }
            }
            catch (HttpRequestException ex)
            {
                ResultMessage = ex.Message;
                ConsultationsList = new List<Consultation>();
            }
        }
    }
}
