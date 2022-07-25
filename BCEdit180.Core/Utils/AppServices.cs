namespace BCEdit180.Core.Utils {
    public class AppServices {
        public static IAppServices Services => ServiceManager.GetService<IAppServices>();
    }
}