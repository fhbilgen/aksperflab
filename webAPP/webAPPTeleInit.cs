using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace webAPP
{
    public class webAPPTeleInit : ITelemetryInitializer
    {
        public void Initialize(ITelemetry telemetry)
        {
            if (string.IsNullOrEmpty(telemetry.Context.Cloud.RoleName))
            {
                //set custom role name here
                telemetry.Context.Cloud.RoleName = "webAPP";
                telemetry.Context.Cloud.RoleInstance = Environment.MachineName;
            }
        }    
    }
}
