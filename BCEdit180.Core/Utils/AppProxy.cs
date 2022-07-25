namespace BCEdit180.Core.Utils {
    public class AppServices {
        public static IApplicationProxy Services => ServiceManager.GetService<IApplicationProxy>();
    }
}