namespace BCEdit180.Core.Utils {
    public class AppProxy {
        public static IApplicationProxy Proxy => ServiceManager.GetService<IApplicationProxy>();
    }
}