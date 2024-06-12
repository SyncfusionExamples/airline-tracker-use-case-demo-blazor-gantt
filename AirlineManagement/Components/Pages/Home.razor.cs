using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http.HttpResults;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Gantt;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.Inputs;
using System;

namespace AirlineManagement.Components.Pages
{
    public partial class Home
    {
        private SfGantt<AirlineInfoModel> ganttInstance { get; set; } = new();
        private SfTextBox TextBoxSearchObj { get; set; } = new();
        private List<AirlineInfoModel> airlineInformations { get; set; } = new();
        private string[] airlines { get; set; } = new[] { "Thai Airways", "AirArabia Airlines", "Emirates Airlines", "Qatar Airways", "South Airlines" };
        private string[] startPlaces { get; set; } = new[] { "BUR", "OMA", "DEN", "LAX", "SHJ", "TFU", "HND", "DXB", "JFK", "MXP", "BKK", "SHJ" };
        private string[] destinationPlaces { get; set; } = new[] { "RNO", "BUR", "OAK", "LAX", "SZX", "FUK", "DXB", "JFK", "EWR", "CNX", "BKK", "HKT", "HDY", "SHJ", "BAH", "RUH" };
        private string _statusStyleColor = string.Empty;
        private string _statusContentstyleColor = string.Empty;
        private string _flightNumber = string.Empty;
        private string _flightAirline = string.Empty;
        private DateTime _flightDepartureDate = DateTime.Now;
        private string _flightOrigin = string.Empty;
        private string _flightDestination = string.Empty;
        private Query _query = new();
        protected override async Task OnInitializedAsync()
        {
            airlineInformations = GetTaskCollection();
            await Task.CompletedTask;
        }

        public async Task OnCreateSearch()
        {
            // Event creation with event handler
            var Click = EventCallback.Factory.Create<MouseEventArgs>(this, SearchClick);
            await TextBoxSearchObj.AddIconAsync("append", "e-search-icon", new Dictionary<string, object>() { { "onclick", Click } });
        }
        public async Task SearchClick(MouseEventArgs args)
        {
            await SearchHandler(TextBoxSearchObj.Value);
        }


        /// <summary>
        /// This class contains properties for maintaining the airline collection.
        /// </summary>
        public class AirlineInfoModel
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
            public int Progress { get; set; }
            public string? FlightId { get; set; } = string.Empty;
            public string Airline { get; set; } = string.Empty;
            public DateOnly? FlightDate { get; set; }
            public DateTime? Departure { get; set; }
            public DateTime? Arrival { get; set; }
            public DateTime? LandingTime { get; set; }
            public string? Duration { get; set; }
            public string Status { get; set; } = string.Empty;
            public string Destination { get; set; } = string.Empty;
            public string Origin { get; set; } = string.Empty;
            public string Aircraft { get; set; } = string.Empty;
            public string DepartureGate { get; set; } = string.Empty;
            public string ArrivalGate { get; set; } = string.Empty;
        }

        public static List<AirlineInfoModel> GetTaskCollection() => new()
        {
            new AirlineInfoModel { Id = 1, Airline = "South Airlines" },
            new AirlineInfoModel { Id = 2, ParentId = 1, Progress = 25, FlightId = "N200WN", Airline = "Southwest Airlines", Departure = new DateTime(2024, 06, 10, 01, 44, 00), Arrival = new DateTime(2024, 06, 10, 06, 18, 00), Destination = "BUR", Origin = "OMA", Aircraft="Boeing 737-7H4", DepartureGate= "C6", ArrivalGate = "A8", Status = "SCHEDULED" },
            new AirlineInfoModel { Id = 3, ParentId = 1, FlightId = "N425LV", Airline = "Southwest Airlines", Departure = new DateTime(2024, 06, 10, 02, 35, 00), Arrival = new DateTime(2024, 06, 10, 07, 45, 00), Destination = "RNO", Origin = "BUR", Aircraft = "Boeing 737-7H4", DepartureGate= "D85", ArrivalGate = "B7", LandingTime = new DateTime(2024, 06, 10, 07, 59, 00), Progress = 100, Status = "DELAYED" },
            new AirlineInfoModel { Id = 4, ParentId = 1, FlightId = "N240WN", Airline = "Southwest Airlines", Departure = new DateTime(2024, 06, 10, 03, 08, 00), Arrival = new DateTime(2024, 06, 10, 10, 17, 00), Destination = "OAK", Origin = "BUR", Aircraft = "Boeing 737-7H4", DepartureGate= "H26", ArrivalGate = "C8", LandingTime = new DateTime(2024, 06, 10, 10, 12, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 5, ParentId = 1, FlightId = "N205WN", Airline = "Southwest Airlines", Departure = new DateTime(2024, 06, 10, 02, 09, 00), Arrival = new DateTime(2024, 06, 10, 09, 20, 00), Destination = "OAK", Origin = "BUR", Aircraft = "Boeing 737-7H4", DepartureGate= "A6", ArrivalGate = "BH8", LandingTime = new DateTime(2024, 06, 10, 09, 22, 00), Progress=100, Status = "DELAYED" },
            new AirlineInfoModel { Id = 6, ParentId = 1, FlightId = "N8807L", Airline = "Southwest Airlines", Departure = new DateTime(2024, 06, 10, 04, 52, 00), Arrival = new DateTime(2024, 06, 10, 10, 09, 00), Destination = "LAX", Origin = "DEN", Aircraft = "Boeing 737-8MAX", DepartureGate= "C9", ArrivalGate = "AD2", Progress=50, Status = "SCHEDULED" },

            new AirlineInfoModel { Id = 7, Airline = "Qatar Airways" },
            new AirlineInfoModel { Id = 8, ParentId = 7, Progress = 40, FlightId = "CHH7275", Airline = "Qatar Airways", Departure = new DateTime(2024, 06, 11, 02, 15, 00), Arrival = new DateTime(2024, 06, 11, 16, 41, 00), Destination = "SZX", Origin = "TFU", Aircraft="Boeing 789", DepartureGate= "523", ArrivalGate = "42", Status = "SCHEDULED" },
            new AirlineInfoModel { Id = 9, ParentId = 7, FlightId = "ANA258", Airline = "Qatar Airways", Departure = new DateTime(2024, 06, 10, 15, 09, 00), Arrival = new DateTime(2024, 06, 10, 16, 50, 00), Destination = "FUK", Origin = "HND", Aircraft = "Boeing 789", DepartureGate= "9", ArrivalGate = "2", LandingTime = new DateTime(2024, 06, 10, 16, 50, 00), Progress = 100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 10, ParentId = 7, FlightId = "ANA886", Airline = "Qatar Airways", Departure = new DateTime(2024, 06, 10, 14, 11, 00), Arrival = new DateTime(2024, 06, 10, 22, 15, 00), Destination = "OAK", Origin = "BUR", Aircraft = "Boeing 789", DepartureGate= "3", ArrivalGate = "1", LandingTime = new DateTime(2024, 06, 10, 22, 09, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 11, ParentId = 7, FlightId = "QTR1170", Airline = "Qatar Airways", Departure = new DateTime(2024, 06, 10, 08, 52, 00), Arrival = new DateTime(2024, 06, 10, 11, 40, 00), Destination = "OAK", Origin = "BUR", Aircraft = "Boeing 789", DepartureGate= "C44", ArrivalGate = "3", LandingTime = new DateTime(2024, 06, 10, 11, 40, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 12, ParentId = 7, FlightId = "ANA259", Airline = "Qatar Airways", Departure = new DateTime(2024, 06, 10, 15, 03, 00), Arrival = new DateTime(2024, 06, 10, 16, 59, 00), Destination = "LAX", Origin = "DEN", Aircraft = "Boeing 789", DepartureGate= "C9", ArrivalGate = "AD2", Progress=100, Status = "DELAYED", LandingTime = new DateTime(2024, 06, 10, 17, 05, 00), },

            new AirlineInfoModel { Id = 13, Airline = "Emirates Airlines" },
            new AirlineInfoModel { Id = 14, ParentId = 13, FlightId = "EK202", Airline = "Emirates Airlines", Departure = new DateTime(2024, 06, 10, 00, 07, 00), Arrival = new DateTime(2024, 06, 10, 19, 42, 00), Destination = "JFK", Origin = "DXB", Aircraft="Airbus A380-800", DepartureGate= "4", ArrivalGate = "2", Status = "ON TIME", Progress=100 },
            new AirlineInfoModel { Id = 15, ParentId = 13, FlightId = "EK203", Airline = "Emirates Airlines", Departure = new DateTime(2024, 06, 10, 02, 56, 00), Arrival = new DateTime(2024, 06, 10, 08, 19, 00), Destination = "DXB", Origin = "JFK", Aircraft = "Airbus A380-800", DepartureGate= "3", ArrivalGate = "4", LandingTime = new DateTime(2024, 06, 10, 08, 06, 00), Progress = 100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 16, ParentId = 13, FlightId = "EK205", Airline = "Emirates Airlines", Departure = new DateTime(2024, 06, 10, 09, 48, 00), Arrival = new DateTime(2024, 06, 10, 14, 17, 00), Destination = "DXB", Origin = "MXP", Aircraft = "Airbus A380-800", DepartureGate= "3", ArrivalGate = "1", LandingTime = new DateTime(2024, 06, 10, 14, 11, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 17, ParentId = 13, FlightId = "EK210", Airline = "Emirates Airlines", Departure = new DateTime(2024, 06, 10, 01, 11, 00), Arrival = new DateTime(2024, 06, 10, 13, 10, 00), Destination = "JFK", Origin = "MXP", Aircraft = "Airbus A380-800", DepartureGate= "4", ArrivalGate = "1", Progress=50, Status = "SCHEDULED" },
            new AirlineInfoModel { Id = 18, ParentId = 13, FlightId = "EK211", Airline = "Emirates Airlines", Departure = new DateTime(2024, 06, 10, 00, 55, 00), Arrival = new DateTime(2024, 06, 10, 15, 33, 00), Destination = "EWR", Origin = "ATH", Aircraft = "Airbus A380-800", DepartureGate= "62", ArrivalGate = "2", Progress=100, Status = "ON TIME", LandingTime = new DateTime(2024, 06, 10, 15, 23, 00), },

            new AirlineInfoModel { Id = 19, Airline = "AirArabia Airlines" },
            new AirlineInfoModel { Id = 20, ParentId = 19, FlightId = "ABY102", Airline = "AirArabia Airlines", Departure = new DateTime(2024, 06, 10, 09, 13, 00), Arrival = new DateTime(2024, 06, 10, 11, 25, 00), Destination = "BAH", Origin = "SHJ", Aircraft="Airbus A320", DepartureGate= "13A", ArrivalGate = "2", Status = "SCHEDULED", Progress=50 },
            new AirlineInfoModel { Id = 21, ParentId = 19, FlightId = "ABY113", Airline = "AirArabia Airlines", Departure = new DateTime(2024, 06, 10, 10, 00, 00), Arrival = new DateTime(2024, 06, 10, 11, 16, 00), Destination = "SHJ", Origin = "MCT", Aircraft = "Airbus A380-800", DepartureGate= "H22", ArrivalGate = "A33", LandingTime = new DateTime(2024, 06, 10, 11, 07, 00), Progress = 100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 22, ParentId = 19, FlightId = "ABY129", Airline = "AirArabia Airlines", Departure = new DateTime(2024, 06, 10, 09, 48, 00), Arrival = new DateTime(2024, 06, 10, 12, 28, 00), Destination = "SHJ", Origin = "SHJ", Aircraft = "Airbus A380-800", DepartureGate= "3", ArrivalGate = "1", LandingTime = new DateTime(2024, 06, 10, 12, 18, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 23, ParentId = 19, FlightId = "ABY136", Airline = "AirArabia Airlines", Departure = new DateTime(2024, 06, 10, 09, 57, 00), Arrival = new DateTime(2024, 06, 10, 11, 59, 00), Destination = "SHJ", Origin = "DOH", Aircraft = "Airbus A380-800", DepartureGate= "B2", ArrivalGate = "32", Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 24, ParentId = 19, FlightId = "ABY152", Airline = "AirArabia Airlines", Departure = new DateTime(2024, 06, 10, 09, 32, 00), Arrival = new DateTime(2024, 06, 10, 12, 05, 00), Destination = "RUH", Origin = "RUH", Aircraft = "Airbus A380-800", DepartureGate= "1", ArrivalGate = "H2", Progress=20, Status = "SCHEDULED"},

            new AirlineInfoModel { Id = 25, Airline = "Thai Airways" },
            new AirlineInfoModel { Id = 26, ParentId = 25, FlightId = "THA110", Airline = "Thai Airways", Departure = new DateTime(2024, 06, 10, 13, 20, 00), Arrival = new DateTime(2024, 06, 10, 14, 39, 00), Destination = "CNX", Origin = "BKK", Aircraft="Airbus A320", DepartureGate= "H2", ArrivalGate = "1", Status = "ON TIME", Progress=100 },
            new AirlineInfoModel { Id = 27, ParentId = 25, FlightId = "THA133", Airline = "Thai Airways", Departure = new DateTime(2024, 06, 10, 13, 43, 00), Arrival = new DateTime(2024, 06, 10, 16, 50, 00), Destination = "BKK", Origin = "CEI", Aircraft = "Airbus A320", DepartureGate= "2", ArrivalGate = "3", Progress = 50, Status = "SCHEDULED" },
            new AirlineInfoModel { Id = 28, ParentId = 25, FlightId = "THA204", Airline = "Thai Airways", Departure = new DateTime(2024, 06, 10, 13, 48, 00), Arrival = new DateTime(2024, 06, 10, 16, 25, 00), Destination = "BKK", Origin = "HKT", Aircraft = "Airbus A320", DepartureGate= "4", ArrivalGate = "22", LandingTime = new DateTime(2024, 06, 10, 16, 15, 00), Progress=100, Status = "ON TIME" },
            new AirlineInfoModel { Id = 29, ParentId = 25, FlightId = "THA215", Airline = "Thai Airways", Departure = new DateTime(2024, 06, 10, 13, 44, 00), Arrival = new DateTime(2024, 06, 10, 17, 14, 00), Destination = "HKT", Origin = "BKK", Aircraft = "Airbus A320", DepartureGate= "H23", ArrivalGate = "3", Progress=50, Status = "SCHEDULED" },
            new AirlineInfoModel { Id = 30, ParentId = 25, FlightId = "THA263", Airline = "Thai Airways", Departure = new DateTime(2024, 06, 10, 12, 37, 00), Arrival = new DateTime(2024, 06, 10, 14, 02, 00), Destination = "HDY", Origin = "BKK", Aircraft = "Airbus A320", DepartureGate= "2", ArrivalGate = "4", Progress=100, Status = "ON TIME"},
        };


        private IGanttTaskModel<AirlineInfoModel> GetTaskData(AirlineInfoModel data)
        {
            return ganttInstance.GetRowTaskModel(data);
        }

        private string GetStatusContentStyles(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                return string.Empty;
            }
            switch (status)
            {
                case "ON TIME":
                    _statusStyleColor = "#DFFFE2";
                    _statusContentstyleColor = "#00A653";
                    break;
                case "SCHEDULED":
                    _statusStyleColor = "#FFF3CD";
                    _statusContentstyleColor = "#866500";
                    break;
                case "DELAYED":
                    _statusStyleColor = "#F8D7DA";
                    _statusContentstyleColor = "#DC3545";
                    break;
            }
            return $"background:{_statusContentstyleColor};color: #FFFFFF;padding: 5px 8px;border-radius: 5px;font-family: DMSans-regular, DMSans-regular, DMSans-regular, sans-serif !important; font-size: 12px !important; font-weight: bold !important;";
        }

        private async Task SearchHandler(string value)
        {
            await ganttInstance.SearchAsync(value);
        }
        private async Task ValueChangeHandler(ChangedEventArgs args)
        {
            if (args.PreviousValue is null)
            {
                return;
            }
            await SearchHandler(args.Value);
        }
        private void FlightNumberHandler(string value)
        {
            _flightNumber = value;
        }
        private void FlightDateHandler(DateTime value)
        {
            if (value.Ticks == 0)
            {
                _flightDepartureDate = DateTime.Now;
                return;
            }
            _flightDepartureDate = value;
        }
        private void FlightAirlineHandler(string value)
        {
            _flightAirline = value;
        }
        private void FlightOriginHandler(string value)
        {
            _flightOrigin = value;
        }
        private void FlightDestinationHandler(string value)
        {
            _flightDestination = value;
        }

        private async Task FilterHandler(MouseEventArgs args)
        {
            List<WhereFilter> predicateList = new();
            if (!string.IsNullOrEmpty(_flightNumber))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "FlightId",
                    Operator = "contains",
                    Condition = "and",
                    value = _flightNumber,
                    IgnoreCase = true,
                });
            }

            if (_flightDepartureDate.Ticks != 0 && _flightDepartureDate.Date.Ticks != DateTime.Now.Date.Ticks)
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "FlightId",
                    Operator = "contains",
                    Condition = "and",
                    value = _flightNumber,
                    IgnoreCase = true,
                });
            }

            if (!string.IsNullOrEmpty(_flightAirline))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "Airline",
                    Operator = "contains",
                    Condition = "and",
                    value = _flightAirline,
                    IgnoreCase = true,
                });
            }

            if (!string.IsNullOrEmpty(_flightOrigin))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "Origin",
                    Operator = "contains",
                    Condition = "and",
                    value = _flightOrigin,
                    IgnoreCase = true,
                });
            }
            if (!string.IsNullOrEmpty(_flightDestination))
            {
                predicateList.Add(new WhereFilter()
                {
                    Field = "Destination",
                    Operator = "contains",
                    Condition = "and",
                    value = _flightDestination,
                    IgnoreCase = true,
                });
            }
            _query = new Query().Where(WhereFilter.Or(predicateList));
            await Task.CompletedTask;
        }
        private async Task ClearFilterHandler(MouseEventArgs args)
        {
            _flightNumber = string.Empty;
            _flightOrigin = string.Empty;
            _flightDestination = string.Empty;
            _flightAirline = string.Empty;
            _flightDepartureDate = new DateTime();
            _query = new Query();
            await Task.CompletedTask;
        }
    }
}