using Syncfusion.Blazor.Gantt;

namespace AirlineManagement.Components.Pages
{
    public partial class Airline
    {
        private ViewType viewType { get; set; } = ViewType.ResourceView;
        private SfGantt<AirlineInfoModel> _ganttInstance { get; set; } = new();
        private List<AirlineInfoModel> airlineInformations { get; set; } = new();
        private string[] status { get; set; } = new[] { "InProgress", "Delay", "Scheduled", "Completed", "Canceled" };
        private string[] places { get; set; } = new[] { "Chennai", "Mumbai", "Delhi", "Tokiyo", "Wasingtan" };
        private List<ResourceInfoModel> resourceCollection { get; set; } = new();
        private static List<AssignmentModel> assignmentCollection { get; set; } = new();
        private string color = string.Empty;
        private string progressColor = string.Empty;
        private string statusStyleColor = string.Empty;
        private string statusContentstyleColor = string.Empty;
        private bool check = true;
        protected override async Task OnInitializedAsync()
        {
            airlineInformations = GetTaskCollection();
            resourceCollection = GetResources();
            assignmentCollection = GetAssignmentCollection();
            await Task.CompletedTask;
        }

        /// <summary>
        /// This class contains properties for maintaining the airline collection.
        /// </summary>
        public class AirlineInfoModel
        {
            public int Id { get; set; }
            public int? ParentId { get; set; }
            public int Progress { get; set; }
            public string? Flight { get; set; } = string.Empty;
            public string AirportName { get; set; } = string.Empty;
            public string Airline { get; set; } = string.Empty;
            public DateOnly? JournyDate { get; set; }
            public DateTime? DepartureTime { get; set; }
            public DateTime? ArrivalTime { get; set; }
            public string Duration { get; set; } = string.Empty;
            public string Status { get; set; } = string.Empty;
            public string Destination { get; set; } = string.Empty;
            public string Origin { get; set; } = string.Empty;
            public string? ConnectedFlight { get; set; }
        }

        /// <summary>
        /// This class represents the assignment collection used to connect the task collection and resource collection.
        /// </summary>
        public class AssignmentModel
        {
            public int PrimaryId { get; set; }
            public int TaskId { get; set; }
            public int ResourceId { get; set; }
            public double Unit { get; set; }
        }

        /// <summary>
        /// This class contains properties for resources such as resource ID, name, and unit.
        /// </summary>
        public class ResourceInfoModel
        {
            public int ResourceId { get; set; }
            public string? ResourceName { get; set; }
            public double Unit { get; set; }
        }

        public static List<AirlineInfoModel> GetTaskCollection() => new()
        {
            new AirlineInfoModel { Id = 1, Flight = "FL456, FL466", Airline = "Jet Airways", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 06), Status = "Scheduled", Destination = "Cochin", Origin = "Delhi", },
            new AirlineInfoModel { Id = 2, ParentId = 1, Progress = 25, Flight = "FL456", Airline = "Jet Airways", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 06), ArrivalTime = new DateTime(2024, 06, 06, 17, 30, 00), DepartureTime = new DateTime(2024, 06, 06, 22, 30, 00), Status = "InProgress", Destination = "Bumbay", Origin = "Delhi"  },
            new AirlineInfoModel { Id = 3, ParentId = 1, Flight = "FL466", Airline = "Jet Airways", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 06, 06), ArrivalTime = new DateTime(2024, 06, 06, 22, 50, 00), DepartureTime = new DateTime(2024, 06, 07, 04, 30, 00), Status = "Scheduled", Destination = "Cochin", Origin = "Bumbay", ConnectedFlight="2FS" },
            new AirlineInfoModel { Id = 5, Progress = 50, Flight = "FL254", Airline = "Air India", AirportName = "Kolkata Airport", JournyDate = new DateOnly(2024, 06, 05), ArrivalTime = new DateTime(2024, 06, 05, 06, 20, 00), DepartureTime = new DateTime(2024, 06, 05, 08, 00, 00), Status = "InProgress", Destination = "Chennai", Origin = "Kolkata", },
            new AirlineInfoModel { Id = 6, Flight = "FL255", Airline = "Air India", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 01, 05), ArrivalTime = new DateTime(2024, 06, 05, 08, 20, 00), DepartureTime = new DateTime(2024, 06, 05, 10, 20, 00), Status = "Scheduled", Destination = "Banglore", Origin = "Chennai"},
            new AirlineInfoModel { Id = 8, Flight = "FL556", Airline = "IndiGo", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 05, 21), ArrivalTime = new DateTime(2024, 06, 06, 21, 00, 00), DepartureTime = new DateTime(2024, 06, 06, 23, 00, 00), Status = "Delay", Destination = "Delhi", Origin = "Banglore"},
            new AirlineInfoModel { Id = 9, Flight = "FL488, FL458", Airline = "Jet Airways", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 06, 06), Status = "Scheduled", Destination = "New Delhi", Origin = "Banglore", },
            new AirlineInfoModel { Id = 10, ParentId = 9, Progress = 25, Flight = "FL488", Airline = "Jet Airways", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 04, 00, 00), DepartureTime = new DateTime(2024, 06, 03, 10, 00, 00), Status = "InProgress", Destination = "Trivandram", Origin = "Banglore" },
            new AirlineInfoModel { Id = 11, ParentId = 9, Flight = "FL458", Airline = "Jet Airways", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 10, 30, 00), Duration="2", Status = "Scheduled", Destination = "New Delhi", Origin = "Trivandram", ConnectedFlight = "10FS" },
            new AirlineInfoModel { Id = 12, Flight = "FL655", Airline = "Air Asia", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 08), Status = "Scheduled", Destination = "Cochin", Origin = "Delhi", },
            new AirlineInfoModel { Id = 14, Flight = "FL655", Airline = "Air Asia", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 06, 08), ArrivalTime = new DateTime(2024, 06, 08, 23, 56, 00), DepartureTime = new DateTime(2024, 06, 09, 04, 25, 00), Status = "Scheduled", Destination = "Cochin", Origin = "Bumbay", },
            new AirlineInfoModel { Id = 15, Flight = "FL555", Airline = "Jet Airways", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 06, 06), Status = "Scheduled", Destination = "New Delhi", Origin = "Banglore", },
            new AirlineInfoModel { Id = 16, ParentId = 15, Progress = 25, Flight = "FL555", Airline = "Jet Airways", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 04, 00, 00), DepartureTime = new DateTime(2024, 06, 03, 10, 00, 00), Status = "InProgress", Destination = "Trivandram", Origin = "Banglore" },
            new AirlineInfoModel { Id = 17, ParentId = 15, Flight = "FL555", Airline = "Jet Airways", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 10, 30, 00), Duration="2", Status = "Scheduled", Destination = "New Delhi", Origin = "Trivandram", ConnectedFlight = "16FS",  },
            new AirlineInfoModel { Id = 18, Flight = "FL895", Airline = "Indigo", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 06), Status = "Scheduled", Destination = "New Delhi", Origin = "Cochin" },
            new AirlineInfoModel { Id = 19, ParentId = 18, Progress = 25, Flight = "FL895", Airline = "Indigo", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 04, 00, 00), DepartureTime = new DateTime(2024, 06, 03, 10, 00, 00), Status = "InProgress", Destination = "Jaipur", Origin = "New Delhi", },
            new AirlineInfoModel { Id = 20, ParentId = 18, Flight = "FL895", Airline = "Indigo", AirportName = "Jaipur Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 10, 30, 00), DepartureTime = new DateTime(2024, 06, 03, 13, 00, 00), Status = "Scheduled", Destination = "Bombay", Origin = "Jaipur", ConnectedFlight = "19FS" },
            new AirlineInfoModel { Id = 21, ParentId = 18, Flight = "FL895", Airline = "Indigo", AirportName = "Bombay Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 13, 30, 00), DepartureTime = new DateTime(2024, 06, 03, 18, 00, 00), Status = "Scheduled", Destination = "Cochin", Origin = "Bombay", ConnectedFlight = "20FS" },
            new AirlineInfoModel { Id = 23,Progress = 25, Flight = "FL845", Airline = "Vistara", AirportName = "Banglore Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 04, 00, 00), DepartureTime = new DateTime(2024, 06, 03, 10, 00, 00), Status = "InProgress", Destination = "Trivandram", Origin = "Banglore", },
            new AirlineInfoModel { Id = 24,Flight = "FL898", Airline = "Vistara", AirportName = "Cochin Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 10, 30, 00), Duration="2", Status = "Scheduled", Destination = "New Delhi", Origin = "Trivandram", },
            new AirlineInfoModel { Id = 25, Flight = "FL875", Airline = "SpiceJet", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 06), Status = "Scheduled", Destination = "New Delhi", Origin = "Cochin",},
            new AirlineInfoModel { Id = 26, ParentId = 25, Progress = 25, Flight = "FL875", Airline = "SpiceJet", AirportName = "Delhi Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 04, 00, 00), DepartureTime = new DateTime(2024, 06, 03, 10, 00, 00), Status = "InProgress", Destination = "Raja Bhoj", Origin = "New Delhi",},
            new AirlineInfoModel { Id = 27, ParentId = 25, Flight = "FL875", Airline = "SpiceJet", AirportName = "Raja Bhoj Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 10, 30, 00), DepartureTime = new DateTime(2024, 06, 03, 15, 00, 00), Status = "Scheduled", Destination = "Bombay", Origin = "Raja Bhoj", ConnectedFlight = "26FS" },
            new AirlineInfoModel { Id = 28, ParentId = 25, Flight = "FL875", Airline = "SpiceJet", AirportName = "Bombay Airport", JournyDate = new DateOnly(2024, 06, 03), ArrivalTime = new DateTime(2024, 06, 03, 15, 30, 00), DepartureTime = new DateTime(2024, 06, 03, 18, 00, 00), Status = "Scheduled", Destination = "Cochin", Origin = "Bombay", ConnectedFlight = "27FS"},
        };

        public static List<ResourceInfoModel> GetResources() => new()
        {
            new ResourceInfoModel() { ResourceId= 1, ResourceName= "Jet Airways",},
            new ResourceInfoModel() { ResourceId= 2, ResourceName= "Air India", },
            new ResourceInfoModel() { ResourceId= 3, ResourceName= "Air Asia", },
            new ResourceInfoModel() { ResourceId= 4, ResourceName= "SpiceJet", },
            new ResourceInfoModel() { ResourceId= 5, ResourceName= "Indigo", },
        };

        public static List<AssignmentModel> GetAssignmentCollection() => new()
        {
            new AssignmentModel(){ PrimaryId=1, TaskId = 2, ResourceId=1},
            new AssignmentModel(){ PrimaryId=2, TaskId = 3, ResourceId=1},
            new AssignmentModel(){ PrimaryId=3, TaskId = 5, ResourceId=2},
            new AssignmentModel(){ PrimaryId=4, TaskId = 6, ResourceId=2},
            new AssignmentModel(){ PrimaryId=5, TaskId = 8, ResourceId=5},
            new AssignmentModel(){ PrimaryId=6, TaskId = 10, ResourceId=1},
            new AssignmentModel(){ PrimaryId=7, TaskId = 11, ResourceId=1},
            new AssignmentModel(){ PrimaryId=8, TaskId = 12, ResourceId=3},
            new AssignmentModel(){ PrimaryId=9, TaskId = 14, ResourceId=3},
            new AssignmentModel(){ PrimaryId=10, TaskId = 16, ResourceId=1},
            new AssignmentModel(){ PrimaryId=11, TaskId = 17, ResourceId=1},
            new AssignmentModel(){ PrimaryId=12, TaskId = 18, ResourceId=5},
            new AssignmentModel(){ PrimaryId=13, TaskId = 19, ResourceId=5},
            new AssignmentModel(){ PrimaryId=14, TaskId = 20, ResourceId=5},
            new AssignmentModel(){ PrimaryId=15, TaskId = 21, ResourceId=5},
            new AssignmentModel(){ PrimaryId=16, TaskId = 25, ResourceId=4},
            new AssignmentModel(){ PrimaryId=17, TaskId = 26, ResourceId=4},
            new AssignmentModel(){ PrimaryId=18, TaskId = 27, ResourceId=4},
            new AssignmentModel(){ PrimaryId=19, TaskId = 28, ResourceId=4}
        };

        private void ChangeViewType()
        {
            if (viewType == ViewType.ResourceView)
            {
                viewType = ViewType.ProjectView;
            }
            else
            {
                viewType = ViewType.ResourceView;
            }
            StateHasChanged();
        }
        private void SetBackgroundColor(AirlineInfoModel task)
        {
            if (task.Status == "InProgress")
            {
                color = "#198754";
                progressColor = "#006400";
            }
            else if (task.Status == "Scheduled")
            {
                color = "#FFC107";
                progressColor = "#D67834";
            }
            else if (task.Status == "Completed")
            {
                color = "#006400";
            }
            else if (task.Status == "Canceled")
            {
                color = "#FF0000";
            }
            else if (task.Status == "Delay")
            {
                color = "#FF0000";
            }
            else if (task.Status == "OnHold")
            {
                color = "#FF4433";
            }
        }
        private IGanttTaskModel<AirlineInfoModel> GetTaskData(AirlineInfoModel data)
        {
            return _ganttInstance.GetRowTaskModel(data);
        }
        private string GetTime(DateTime? date = null)
        {
            if (date is null)
            {
                return string.Empty;
            }
            return $"{date?.Hour}:{date?.Minute}";
        }

        private string GetStatusContentStyles(string status)
        {
            switch (status)
            {
                case "InProgress":
                    statusStyleColor = "#DFECFF";
                    statusContentstyleColor = "#006AA6";
                    break;
                case "Cancel":
                    statusStyleColor = "#FF0000";
                    statusContentstyleColor = "#FFFFFF";
                    break;
                case "Completed":
                    statusStyleColor = "#DFFFE2";
                    statusContentstyleColor = "#00A653";
                    break;
                case "Scheduled":
                    statusStyleColor = "#FFF3CD";
                    statusContentstyleColor = "#866500";
                    break;
                case "Delay":
                    statusStyleColor = "#F8D7DA";
                    statusContentstyleColor = "#DC3545";
                    break;
            }
            return $"background:{statusStyleColor};color:{statusContentstyleColor};padding: 5px 12px; border-radius: 24px; border:1px solid {statusContentstyleColor}";
        }

        private string GetTaskbarContent(AirlineInfoModel task)
        {
            return @GetTime(task.ArrivalTime) + " " + @task.Origin;
        }
    }
}